# Why query parametrization matters ?

<!-- Id: sql-why-query-parametrization-matters -->
<!-- Categories: SQL, Performance -->
<!-- Date: 20200403 -->

<!-- #header -->
Vulnerability for SQL injection is quite common knowledge about dynamic SQL and good practice is to replace dynamic SQL statemets with parametrized SQL statements. But there is also another big advantage almost as important as first mentioned. It's about permance.  
<!-- #endheader -->

I developed Data Access Layer application talking with Azure Sql Database. Having no much time I quickly use dynamic SQL statement instead of parametrized one. Quickly after deployment on stage environment I received message from Azure:

![01](01.png)

... also with some details and recommendations:

<table>
<tr>
<td>
Potential causes
</td>
<td>
Defect in application code constructing faulty SQL statements; application code doesn't sanitize user input and may be exploited to inject malicious SQL statements
</td>
</tr>

<tr>
<td>
Investigation steps
</td>
<td>
For details, view the alert in the Azure Security Center.
To investigate further, analyze your audit log. 
</td>
</tr>

<tr>
<td>
Remediation steps
</td>
<td>
Read more about SQL Injection threats, as well as best practices for writing safe application code. Please refer to Security Reference: SQL Injection.
</td>
</tr>
</table>

### What's faulty SQL ?

In this case it means dynamic (literal) SQL:

```csharp
EXEC('SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 10')
```

... instead of parametrized SQL.

```csharp
exec sp_executesql 'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = @BusinessEntityID', '@BusinessEntityID int', @BusinessEntityID=10
```

Application could be vurnelable to SQL injection because when SQL statements are constructed by string concatenation in listing id can be `'10'` but can be also `'10; delete from [Person].[Person]'`

```sql
SELECT * FROM [Person].[Person] WHERE BusinessEntityID = ' + id
```

### What's true but ...

There's no information about performance issues which dynamic SQL could cause. This is what I would like to investigate deeper in this article. 
Dynamic queries forces SQL Server to be constructed and compiled every time whereas for parametrized queries it generates a query execution plan just once. When SQL Server encounter the same SQL parametrized statement it will take execution plan form the cache which will contribute to better performance.

### Inside Query Plans

To take a look at query plan information we can use following features:

- sys.dm_exec_cached_plans 
- sys.dm_exec_sql_text  
- sys.dm_exec_query_plan 


```sql
-- sys.dm_exec_cached_plans is dynamic management view
SELECT top 1 * FROM sys.dm_exec_cached_plans

-- whereas sys.dm_exec_sql_text and sys.dm_exec_query_plan
DECLARE @plan_handle varbinary(max) = (SELECT TOP 1 plan_handle FROM sys.dm_exec_cached_plans)
SELECT * FROM sys.dm_exec_sql_text(@plan_handle) 
SELECT * FROM sys.dm_exec_query_plan(@plan_handle)
-- SELECT * FROM sys.dm_exec_plan_attributes(@plan_handle)
```

![02](02.png)

We can as well combine this items to have all in one once forever.

```sql

SELECT UseCounts, RefCounts, Cacheobjtype, Objtype, ISNULL(DB_NAME(T.dbid),'ResourceDB') AS DatabaseName, TEXT AS sqlText, query_plan 
FROM sys.dm_exec_cached_plans 
CROSS APPLY sys.dm_exec_sql_text(plan_handle) T
CROSS APPLY sys.dm_exec_query_plan(plan_handle) P
WHERE T.dbid = DB_ID()
ORDER BY T.dbid,UseCounts DESC
```

https://docs.microsoft.com/en-us/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql?view=sql-server-ver15

Clearing cache should be performed by database admin and permissions for such operations should very careffully admited, especially 

```sql
-- This command removes all cached plans from memory
DBCC FREEPROCCACHE

-- This command allows you to specify a particular database id, and then clears all plans from that particular database. 
DBCC FLUSHPROCINDB (<dbid>)
```

Statement below should be used between every excercise.

```sql
-- DBCC FLUSHPROCINDB (DB_ID('AdventureWorks2016')) doesn't work !
DECLARE @dbid smallint = (SELECT DB_ID('AdventureWorks2016'))
DBCC FLUSHPROCINDB (@dbid)
```

And here's little modified query to display only what we need.

```sql
SELECT UseCounts, RefCounts, Cacheobjtype, Objtype, ISNULL(DB_NAME(T.dbid),'ResourceDB') AS DatabaseName, TEXT AS sqlText
FROM sys.dm_exec_cached_plans 
CROSS APPLY sys.dm_exec_sql_text(plan_handle) T
WHERE T.dbid = DB_ID() and TEXT not like '%dm_exec_cached_plans%'
ORDER BY T.dbid, UseCounts DESC
```

### Statements 

Let's launch SQL queries below one by another.

```sql
-- (1) statement one by one
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 10
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 20
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 30
```

![06](06.png)

```sql
-- execute this query x3
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 20
```

![07](07.png)


```sql
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 40
```

![08](08.png)

It seems like both *Prepared* and *Adhoc* plans were used.

When values are specified explicitely SQL Server uses feature called `simple paramerization`. With simple parametrization SQL creates two execution plans. The first execution plan is shell plan containing a pointer to the second one. Unfortunatelly, it only applies to a small group of queries.

Last query is the same but i changed uppercased statements into lowercased.

```sql
select FirstName, MiddleName, LastName from [Person].[Person] where BusinessEntityID = 10
select FirstName, MiddleName, LastName from [Person].[Person] where BusinessEntityID = 20
select FirstName, MiddleName, LastName from [Person].[Person] where BusinessEntityID = 30
```

![09](09.png)

Optimizer is treating this queries as different queries but at least it takes advantage of *Prepared* query.


### Quering other way

I observed the same SQL Server behaviour and results when using the same statements in shape as on listing.

```sql 
EXEC('SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 10'
EXEC sp_executesql N'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 10'
``` 

### Parametrized queries

Now let's move to parametrized queries.

```sql
-- (4) sp_executesql with parameters
declare @parametrizedSql nvarchar(max) = 'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = @BusinessEntityID'
declare @parameterDefinition nvarchar(max) = '@BusinessEntityID int'
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=10
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=20
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=30
```
See that there's only one execution plan that was created for this query.

![04](04.png)

Let's make small change in query and in @parametrizedSql rename 'SELECT' to 'SELECt' 
```sql
declare @parametrizedSql nvarchar(max) = 'SELECt FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = @BusinessEntityID'
```
and launch again queries one by another. Now, because of this tiny typo which shouldn't affect our query at all we have two execution plans:

![05](05.png)

SQL Server uses hash value as representation for every query. When query is run, SQL Server calculates hash and checks if there's such hash in cache. The hash value for the query plan is generated directly from the text. If there is even a tiny change in the query text a new hash value will be generated and there will be no match with existing hash values. 







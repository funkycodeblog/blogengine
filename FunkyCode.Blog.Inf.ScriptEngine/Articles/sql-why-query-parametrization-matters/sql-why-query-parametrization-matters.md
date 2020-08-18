# Why query parametrization matters ?

<!-- Id: sql-why-query-parametrization-matters -->
<!-- Categories: SQL, Performance -->
<!-- Date: 20200403 -->

<!-- #header -->
<!-- #endheader -->

```sql
-- (1) statement
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 10
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 20
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 30
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 40
SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 50
```

```sql
-- (2) EXEC
EXEC('SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 10')
EXEC('SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 20')
EXEC('SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 30')
EXEC('SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 40')
EXEC('SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 50')
```

```sql
-- (3) sp_executesql (not parametrized)
EXEC sp_executesql N'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 10'
EXEC sp_executesql N'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 20'
EXEC sp_executesql N'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 30'
EXEC sp_executesql N'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 40'
EXEC sp_executesql N'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = 50'
```

```sql
-- (4) sp_executesql with parameters
declare @parametrizedSql nvarchar(max) = 'SELECT FirstName, MiddleName, LastName FROM [Person].[Person] WHERE BusinessEntityID = @BusinessEntityID'
declare @parameterDefinition nvarchar(max) = '@BusinessEntityID int'
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=10
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=20
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=30
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=40
exec sp_executesql @parametrizedSql, @parameterDefinition, @BusinessEntityID=50
```

```sql
SELECT UseCounts, RefCounts, Cacheobjtype, Objtype, ISNULL(DB_NAME(dbid),'ResourceDB') AS DatabaseName, TEXT AS sqlText
FROM sys.dm_exec_cached_plans 
CROSS APPLY sys.dm_exec_sql_text(plan_handle) 
WHERE dbid = DB_ID() and TEXT like '%@BusinessEntityID%'
ORDER BY dbid,UseCounts DESC;
```









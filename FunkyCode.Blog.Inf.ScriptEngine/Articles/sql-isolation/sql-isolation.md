# SQL Isolation Levels

<!-- Id: sql-isolation  -->
<!-- Categories: SQL -->
<!-- Date: 20200720  -->

<!-- #header -->

<!-- #endheader -->


```sql
DROP TABLE IF EXISTS dbo.Players
go

CREATE TABLE dbo.Players
(
  Id   INT identity (1,1) primary key,
  Name VARCHAR(30),
  Points int
);
GO

insert into dbo.Players values 
('Tytus', 10),
('Romek', 15),
('A`Tomek', 20)
GO

select * from dbo.Players 
```

### Problem: Dirty reads

<table>
<tr>
<th> A </th>
<th> B </th>
</tr>
<tr>
<td>

```sql
-- (1)
select @@SPID
select * from dbo.Players where Id = 2

-- (3)
set transaction isolation level READ COMMITTED

-- (5)
begin transaction A 

-- (7)
update dbo.Players set Points = 16 where Id = 2

-- (9)
rollback transaction

```

</td>
<td>

```sql
-- (2)
Select @@SPID

-- (4)
set transaction isolation level READ UNCOMMITTED


-- (6)
select * from dbo.Players where Id = 2

-- (8)
select * from dbo.Players where Id = 2

-- (10)
select * from dbo.Players where Id = 2
```

</td>
</tr>
</table>


### Solution for: Dirty reads

<table>
<tr>
<th> A </th>
<th> B </th>
</tr>
<tr>
<td>

```sql

-- (1)
select @@SPID
select * from dbo.Players where Id = 2

-- (3)
set transaction isolation level READ COMMITTED

-- (5)
begin transaction A 

-- (7)
update dbo.Players set Points = 16 where Id = 2

-- (9)
rollback transaction

```

</td>
<td>

```sql

-- (2)
select @@SPID

-- (4)
set transaction isolation level Read COMMITTED


-- (6a)
select * from dbo.Players where Id = 1
-- (6b)
select * from dbo.Players where Id = 2

-- (8a)
select * from dbo.Players where Id = 1
-- (8b)
select * from dbo.Players where Id = 2

-- (10a)
select * from dbo.Players where Id = 1
-- (10b)
select * from dbo.Players where Id = 2


```

</td>
</tr>
</table>


### Problem: unrepeatable reads

<table>
<tr>
<th> A </th>
<th> B </th>
</tr>
<tr>
<td>

```sql
-- (1)
set transaction isolation level READ COMMITTED

-- (3)
begin transaction A 

-- (6)
update dbo.Players set Points = 16 where Id = 2

-- (8)
commit transaction
```

</td>
<td>

```sql
-- (2)
set transaction isolation level READ COMMITTED

-- (4)
begin transaction B

-- (5)
select * from dbo.Players where Id = 2 -- 15


-- (7) - will wait for (8) to commit
select * from dbo.Players where Id = 2 -- 16

-- (9)
commit transaction
```

</td>
</tr>
</table>


### Solution: unrepeatable reads (not finished)

<table>
<tr>
<th> A </th>
<th> B </th>
</tr>
<tr>
<td>

```sql
-- (1)
set transaction isolation level READ COMMITTED

-- (3)
begin transaction A 

-- (6a)
update dbo.Players set Points = 6 where Id = 1

-- (6b)
update dbo.Players set Points = 16 where Id = 2

-- (8)
commit transaction
```

</td>
<td>

```sql
-- (2)
set transaction isolation level REPEATABLE READ

-- (4)
begin transaction B

-- (5)
select * from dbo.Players where Id = 2 -- 15


-- (7) - will wait for (8) to commit
select * from dbo.Players where Id = 2 -- 16

-- (9)
commit transaction
```

</td>
</tr>
</table>



Table to copy

<table>
<tr>
<th> A </th>
<th> B </th>
</tr>
<tr>
<td>

```sql

```

</td>
<td>

```sql

```

</td>
</tr>
</table>


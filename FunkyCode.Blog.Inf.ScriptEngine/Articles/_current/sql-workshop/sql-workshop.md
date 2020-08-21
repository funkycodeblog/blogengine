# SQL Workshop

<!-- Id: sql-workshop  -->
<!-- Page -->

## Database

#### Drop and recreate database

It seems that (1) is not necessery when ```WITH REPLACE``` is in (2).

```sql
-- (1)

IF DB_ID(N'AdventureWorks2016') is not null 
BEGIN

ALTER DATABASE AdventureWorks2016 set single_user with rollback immediate
ALTER DATABASE AdventureWorks2016 SET OFFLINE
DROP DATABASE AdventureWorks2016
print('AdventureWorks2016 dropped...')
END

GO

-- (2) 

RESTORE DATABASE [AdventureWorks2016] FROM DISK = 'c:\Projects\Pets\blogengine\_devops\Db-backups\AdventureWorks2016.bak' WITH RECOVERY, REPLACE, STATS = 10,
MOVE 'AdventureWorks2016_Data' TO 'c:\Projects\Pets\blogengine\_devops\Db-backups\AdventureWorks2016_Data.mdf',
MOVE 'AdventureWorks2016_Log' TO 'c:\Projects\Pets\blogengine\_devops\Db-backups\AdventureWorks2016_Log.ldf';
GO

```

#### Check physical location of database files 

```sql
DECLARE @dbname nvarchar(max) = 'BlogEngine'

SELECT 
  DB_NAME([database_id]) [database_name]
, [file_id]
, [type_desc] [file_type]
, [name] [logical_name]
, [physical_name]
FROM sys.[master_files]
WHERE [database_id] IN (DB_ID(@dbname))
ORDER BY [type], DB_NAME([database_id]);
```

## Tables

#### Number of rows of all tables

```sql
SELECT
    ST.name AS Table_Name,
    SUM(DMS.row_count) AS NUMBER_OF_ROWS
FROM
    SYS.TABLES AS ST
    INNER JOIN SYS.DM_DB_PARTITION_STATS AS DMS ON ST.object_id = DMS.object_id
WHERE
    DMS.index_id in (0,1)
GROUP BY ST.name
ORDER BY NUMBER_OF_ROWS DESC
```

## Indexes

#### Check heap tables 

```sql
-- List all heap tables 
SELECT SCH.name + '.' + TBL.name AS TableName 
FROM sys.tables AS TBL 
     INNER JOIN sys.schemas AS SCH 
         ON TBL.schema_id = SCH.schema_id 
     INNER JOIN sys.indexes AS IDX 
         ON TBL.object_id = IDX.object_id 
            AND IDX.type = 0 -- = Heap 
ORDER BY TableName
```

#### Index Usage Statistics

```sql
-- Index Usage Statistics
-- If [UserSeeks] < [UserUpdates] then index is not effective !!!

SELECT
    [DatabaseName] = DB_Name(db_id()),
    [TableName] = OBJECT_NAME(i.object_id),
    [IndexName] = i.name, 
    [IndexType] = i.type_desc,
    [TotalUsage] = IsNull(user_seeks, 0) + IsNull(user_scans, 0) + IsNull(user_lookups, 0),
    [UserSeeks] = IsNull(user_seeks, 0),
    [UserScans] = IsNull(user_scans, 0), 
    [UserLookups] = IsNull(user_lookups, 0),
    [UserUpdates] = IsNull(user_updates, 0)
FROM sys.indexes i 
INNER JOIN sys.objects o
    ON i.object_id = o.object_id
LEFT OUTER JOIN sys.dm_db_index_usage_stats s
    ON s.object_id = i.object_id
    AND s.index_id = i.index_id
WHERE 
    (OBJECTPROPERTY(i.object_id, 'IsMsShipped') = 0)
ORDER BY [TableName], [IndexName];
```

## Connections / sessions

#### Number of connections of all databases

```sql
SELECT 
    DB_NAME(dbid) as DBName, 
	COUNT(dbid) as NumberOfConnections,
	loginame as LoginName
FROM
    sys.sysprocesses
WHERE 
    dbid > 0
GROUP BY 
    dbid, loginame
```

#### List of current sessions

```sql
-- current sessions 
-- http://buildingbettersoftware.blogspot.com/2016/04/using-dmvs-to-find-sql-server.html
SELECT
    database_id,    -- SQL Server 2012 and after only
    session_id,
    status,
    login_time,
    cpu_time, -- [millisec]
    memory_usage, -- [8kB block] 
    reads,
    writes,
    logical_reads,
    host_name,
    program_name,
    host_process_id,
    client_interface_name,
    login_name as database_login_name,
    last_request_start_time
FROM sys.dm_exec_sessions
WHERE is_user_process = 1
ORDER BY cpu_time DESC;
```






#### View to display locks in the current database

```sql
IF EXISTS ( SELECT  1
            FROM    sys.views
            WHERE   name = 'DBlocks' ) 
    DROP VIEW DBlocks ;
GO
CREATE VIEW DBlocks AS
SELECT  request_session_id AS spid ,
        DB_NAME(resource_database_id) AS dbname ,
        CASE WHEN resource_type = 'OBJECT'
             THEN OBJECT_NAME(resource_associated_entity_id)
             WHEN resource_associated_entity_id = 0 THEN 'n/a'
             ELSE OBJECT_NAME(p.object_id)
        END AS entity_name ,
        index_id ,
        resource_type AS resource ,
        resource_description AS description ,
        request_mode AS mode ,
        request_status AS status
FROM    sys.dm_tran_locks t
        LEFT JOIN sys.partitions p
                   ON p.partition_id = t.resource_associated_entity_id
WHERE   resource_database_id = DB_ID()
        AND resource_type <> 'DATABASE';

```







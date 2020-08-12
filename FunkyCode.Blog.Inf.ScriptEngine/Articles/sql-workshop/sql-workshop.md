# SQL Workshop

<!-- Id: sql-workshop  -->
<!-- Page -->


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
# SQL performance 

<!-- Id: performance-sql  -->
<!-- Page -->

* Update statistics

* Check if there are no heaps among your tables

```sql
SELECT 
    o.name, 
    o.object_id, 
    CASE 
      WHEN p.index_id = 0 THEN 'Heap'
      WHEN p.index_id = 1 THEN 'Clustered Index/b-tree'
      WHEN p.index_id > 1 THEN 'Non-clustered Index/b-tree'
    END AS 'Type'
FROM sys.tables o INNER JOIN sys.partitions p ON p.object_id = o.object_id
WHERE p.index_id = 0
```


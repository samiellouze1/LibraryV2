USE VehicleRentDB; -- Replace YourDatabaseName with the name of your database

DECLARE @Sql NVARCHAR(MAX) = '';

-- Drop all foreign key constraints
SELECT @Sql += 'ALTER TABLE ' + OBJECT_SCHEMA_NAME(parent_object_id) + '.' + OBJECT_NAME(parent_object_id) + ' DROP CONSTRAINT ' + name + ';'
FROM sys.foreign_keys
WHERE referenced_object_id IN (SELECT object_id FROM sys.tables WHERE is_ms_shipped = 0);

-- Drop all tables
SELECT @Sql += 'DROP TABLE ' + OBJECT_SCHEMA_NAME(object_id) + '.' + OBJECT_NAME(object_id) + ';'
FROM sys.tables
WHERE is_ms_shipped = 0;

-- Execute the generated SQL
EXEC sp_executesql @Sql;
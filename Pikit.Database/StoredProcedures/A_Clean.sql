-- EF Seed
IF OBJECT_ID('dbo.SP_CleanDatabase', 'p') IS NULL AND DB_NAME() LIKE '%Test'
    EXEC ('create procedure SP_CleanDatabase as select 1')
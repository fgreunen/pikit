-- EF Seed
ALTER PROCEDURE SP_CleanDatabase
AS
	SET NOCOUNT ON

	IF DB_NAME() NOT LIKE '%Test'
		RAISERROR ('This SP can only be executed on a Test database.', 20, 1)  WITH LOG

	DELETE FROM AuditRecords
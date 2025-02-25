USE [Esign]
GO
ALTER TABLE customers ADD CONSTRAINT DF_impersonateattempt DEFAULT 0 FOR impersonateattempt;

GO
ALTER TABLE customers ALTER COLUMN impersonateattempt BIT NULL

GO
CREATE TRIGGER ReplaceNullTrigger ON customers
AFTER INSERT, UPDATE
AS
    BEGIN
    SET NOCOUNT ON;
        
        UPDATE C
        SET impersonateattempt = 0
        FROM customers C
             JOIN inserted i ON C.ID = i.ID
        WHERE i.impersonateattempt = '' OR i.impersonateattempt IS NULL;
END;
GO


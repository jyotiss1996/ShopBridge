
--Create Table For the Items
GO
CREATE TABLE [dbo].[ShopBridgeItem] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (100)   NOT NULL,
    [Description]    VARCHAR (500)   NULL,
    [Price]          DECIMAL (18, 2) NULL,
    [Actv_Ind]       TINYINT         NOT NULL,
    [Created_Dt]     DATETIME        NOT NULL,
    [LastUpdated_Dt] DATETIME        NOT NULL
);

--Procedure For the CRUD Operation
GO
CREATE PROCEDURE [dbo].[ShopBridgeItemCRUD]
(	
@Mode Char(1),
@Id int= Null OUT,
@Name Varchar(100)= Null,
@Description Varchar(500)= Null,
@Price Decimal(18,2)= Null,
@Actv_Ind Tinyint=Null,
@Created_Dt DateTime=Null
)
AS
BEGIN

IF(@Mode='R')
BEGIN
	Select * from ShopBridgeItem
	Where (Id = @Id OR ISNULL(@Id,0)=0) AND ([Name] = @Name OR ISNULL(@Name,'')='') AND Actv_Ind = 1
END
IF(@Mode='C')
BEGIN

Insert Into ShopBridgeItem([Name],[Description],Price,Actv_Ind,Created_Dt,LastUpdated_Dt)
Values                    (@Name,@Description,@Price,@Actv_Ind,@Created_Dt,@Created_Dt)

SET @Id = SCOPE_IDENTITY()

END

IF(@Mode='U')
BEGIN

UPDATE ShopBridgeItem SET [Name]=@Name,[Description]=@Description,Price= @Price,LastUpdated_Dt=@Created_Dt
WHERE Id = @Id

END
IF(@Mode='D')
BEGIN

DELETE FROM ShopBridgeItem WHERE Id= @Id

END


END



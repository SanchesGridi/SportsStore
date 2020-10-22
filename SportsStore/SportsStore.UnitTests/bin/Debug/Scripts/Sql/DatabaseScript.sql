﻿CREATE DATABASE "SportsStore_Test"
GO

USE "SportsStore_Test"
GO

CREATE TABLE [dbo].[Products] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX)  NOT NULL,
    [Description]   NVARCHAR (MAX)  NOT NULL,
    [Category]      NVARCHAR (MAX)  NOT NULL,
    [Price]         DECIMAL (18, 2) NOT NULL,
    [ImageData]     VARBINARY (MAX) NULL,
    [ImageMimeType] NVARCHAR (50)   NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

BEGIN TRY
	BEGIN TRANSACTION
		DECLARE @data1 VARBINARY(MAX) = 0xFFD8FFDB0084000503040404030504040405050506070C08070707070F0B0B090C110F1212110F111113161C1713141A1511111821181A1D1D1F1F1F13172224221E241C1E1F1E010505050706070E08080E1E1411141E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E;
		DECLARE @data2 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010100000100010000FFDB0043000B08080808080B08080B100B090B10130E0B0B0E1316121213121216151113121213111515191A1B1A1915212124242121302F2F2F3036363636363636363636FFDB0043010C0B0B0C0D0C0F0D0D0F130E0E0E13140E0F0F0E141A12121412121A221815151515;
		DECLARE @data3 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010100000100010000FFDB0084000906071213121513131316161517181718181818171A1A1B1A1D18171717171817191A1D2820181B251D181A21312125292B2E2E2E181F3338332C37282D2E2B010A0A0A0E0D0E1B10101A2F251F252D2D2D322D2D2D2D2D2D2D2D2F2D2D2D2D2D2D2D2D2D2D2D;
		DECLARE @data4 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010100000100010000FFDB0043000302020302020303030304030304050805050404050A070706080C0A0C0C0B0A0B0B0D0E12100D0E110E0B0B1016101113141515150C0F171816141812141514FFDB00430103040405040509050509140D0B0D1414141414141414141414141414141414141414;
		DECLARE @data5 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010100000100010000FFDB00430006040506050406060506070706080A100A0A09090A140E0F0C1017141818171416161A1D251F1A1B231C1616202C20232627292A29191F2D302D283025282928FFDB0043010707070A080A130A0A13281A161A2828282828282828282828282828282828282828;
		DECLARE @data6 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010101006000600000FFDB0043000402030303020403030304040404050906050505050B080806090D0B0D0D0D0B0C0C0E1014110E0F130F0C0C1218121315161717170E11191B19161A14161716FFDB0043010404040505050A06060A160F0C0F1616161616161616161616161616161616161616;
		DECLARE @data7 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010100000100010000FFFE003B43524541544F523A2067642D6A7065672076312E3020287573696E6720494A47204A50454720763632292C207175616C697479203D2036300AFFDB0043000D090A0B0A080D0B0A0B0E0E0D0F13201513121213271C1E17202E2931302E292D2C333A4A3E33364637;
		DECLARE @data8 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010101004800480000FFE120A045786966000049492A000800000011000001030001000000401400000101030001000000800D00000201030003000000DA0000000601030001000000020000000F01020006000000E0000000100102000F000000E600000012010300010000000100000015010300;
		DECLARE @data9 VARBINARY(MAX) = 0xFFD8FFDB0084000503040404030504040405050506070C08070707070F0B0B090C110F1212110F111113161C1713141A1511111821181A1D1D1F1F1F13172224221E241C1E1F1E010505050706070E08080E1E1411141E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E;
		DECLARE @data10 VARBINARY(MAX) = 0xFFD8FFE000104A46494600010100000100010000FFDB0043000302020302020303030304030304050805050404050A070706080C0A0C0C0B0A0B0B0D0E12100D0E110E0B0B1016101113141515150C0F171816141812141514FFDB00430103040405040509050509140D0B0D1414141414141414141414141414141414141414;

		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Kayak', N'A boat for one person.', N'Watersports', CAST(275.00 AS Decimal(18, 2)), @data1, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Lifejacket', N'Protective and fashionable. Increases swimming speed!', N'Watersports', CAST(48.95 AS Decimal(18, 2)), @data2, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Soccer Ball', N'FIFA-approved size and weight.', N'Soccer', CAST(19.50 AS Decimal(18, 2)), @data3, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Corner Flags', N'Give your palying field a professional touch!', N'Soccer', CAST(34.95 AS Decimal(18, 2)), @data4, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Stadium', N'Flat-packed 35,00-seat stadium.', N'Soccer', CAST(79500.00 AS Decimal(18, 2)), @data5, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Thinking Cap', N'Improve your brain efficiency by 75%!', N'Chess', CAST(16.00 AS Decimal(18, 2)), @data6, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Unsteady Chair', N'Secretly give your opponent a disadvantage.', N'Chess', CAST(29.95 AS Decimal(18, 2)), @data7, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Human Chess Board', N'A fun game for the family.', N'Chess', CAST(75.00 AS Decimal(18, 2)), @data8, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Bling-Bling King', N'Gold-plated, diamond-studded King.', N'Chess', CAST(1200.00 AS Decimal(18, 2)), @data9, N'image/jpeg')
		INSERT INTO [dbo].[Products] ([Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (N'Fake Sneakers', N'Good price and pretty passable quality.', N'Fake', CAST(15.00 AS Decimal(18, 2)), @data10, N'image/jpeg')
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	if	ERROR_MESSAGE() <> NULL	
	BEGIN
		SELECT ERROR_MESSAGE() as 'Message', ERROR_LINE() as 'Line'
		ROLLBACK TRANSACTION
	END		
END CATCH
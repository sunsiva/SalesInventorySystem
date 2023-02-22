USE [master]
GO
/****** Object:  Database [SIM]    Script Date: 22-02-2023 21:48:45 ******/
CREATE DATABASE [SIM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SI_DB', FILENAME = N'E:\SIM-DB\SI_DB.mdf' , SIZE = 3328KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SI_DB_log', FILENAME = N'E:\SIM-DB\SI_DB_log.ldf' , SIZE = 504KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SIM] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SIM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SIM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SIM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SIM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SIM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SIM] SET ARITHABORT OFF 
GO
ALTER DATABASE [SIM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SIM] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [SIM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SIM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SIM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SIM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SIM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SIM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SIM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SIM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SIM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SIM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SIM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SIM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SIM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SIM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SIM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SIM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SIM] SET  MULTI_USER 
GO
ALTER DATABASE [SIM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SIM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SIM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SIM] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SIM] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SIM', N'ON'
GO
USE [SIM]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[InvoiceNo] [nchar](20) NOT NULL,
	[BillingDate] [nchar](30) NULL,
	[CustomerNo] [nchar](20) NULL,
	[CustomerName] [nchar](100) NULL,
	[subTotal] [int] NULL,
	[TaxPercentage] [float] NULL,
	[TaxAmount] [int] NULL,
	[GrandTotal] [int] NULL,
	[TotalPayment] [int] NULL,
	[PaymentDue] [int] NULL,
 CONSTRAINT [PK_BillInfo] PRIMARY KEY CLUSTERED 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[B_Name] [nchar](100) NULL,
	[B_Address] [varchar](250) NULL,
	[B_Landmark] [varchar](250) NULL,
	[B_city] [nchar](50) NULL,
	[B_state] [nchar](50) NULL,
	[B_zipcode] [nchar](10) NULL,
	[S_Name] [nchar](100) NULL,
	[S_address] [varchar](250) NULL,
	[S_landmark] [varchar](250) NULL,
	[S_city] [nchar](50) NULL,
	[S_state] [nchar](50) NULL,
	[S_zipcode] [nchar](10) NULL,
	[CustomerNo] [nchar](20) NOT NULL,
	[Phone] [nchar](15) NULL,
	[Email] [varchar](150) NULL,
	[MobileNo] [nchar](15) NULL,
	[FaxNo] [nchar](15) NULL,
	[Notes] [varchar](250) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Daily]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Daily](
	[Id] [bigint] NOT NULL,
	[CustId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Item] [nvarchar](50) NOT NULL,
	[ItemDesc] [nvarchar](150) NULL,
	[Amount] [money] NULL,
	[Mobile] [nchar](10) NULL,
	[Desc] [nvarchar](150) NULL,
	[CreatedBy] [nchar](10) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedBy] [nchar](10) NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryCategory]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventoryCategory](
	[CategoryID] [nchar](10) NOT NULL,
	[CategoryName] [varchar](150) NULL,
 CONSTRAINT [PK_InventoryCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderedProduct]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[OrderedProduct](
	[OrderNo] [bigint] NOT NULL,
	[ProductCode] [nchar](20) NOT NULL,
	[ProductName] [varchar](250) NULL,
	[Weight] [nchar](10) NULL,
	[Price] [float] NULL,
	[Cartons] [int] NULL,
	[TotalPackets] [int] NULL,
	[TotalAmount] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderInfo]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInfo](
	[OrderNo] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderDate] [nchar](30) NULL,
	[OrderStatus] [nchar](20) NULL,
	[CustomerNo] [nchar](20) NULL,
	[CustomerName] [nchar](100) NULL,
	[SubTotal] [int] NULL,
	[TaxPercentage] [float] NULL,
	[TaxAmount] [int] NULL,
	[TotalAmount] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nchar](10) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nchar](10) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_OrderInfo] PRIMARY KEY CLUSTERED 
(
	[OrderNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ProductCode] [nchar](20) NOT NULL,
	[ProductName] [varchar](250) NULL,
	[Category] [varchar](150) NULL,
	[Weight] [nchar](10) NULL,
	[Price] [float] NULL,
	[MRP] [float] NULL,
	[BPrice] [float] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nchar](10) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductSold]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductSold](
	[InvoiceNo] [nchar](20) NULL,
	[ProductCode] [nchar](20) NULL,
	[ProductName] [varchar](250) NULL,
	[Weight] [nchar](10) NULL,
	[Price] [float] NULL,
	[Cartons] [int] NULL,
	[TotalPackets] [int] NULL,
	[TotalAmount] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registration](
	[Username] [nchar](30) NOT NULL,
	[Password] [nchar](30) NULL,
	[Name] [nchar](30) NULL,
	[ContactNo] [nchar](15) NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stock]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock](
	[StockID] [nchar](20) NOT NULL,
	[ProductCode] [nchar](20) NULL,
	[ProductName] [varchar](250) NULL,
	[Category] [varchar](150) NULL,
	[Weight] [nchar](10) NULL,
	[Stockdate] [nchar](30) NULL,
	[Cartons] [int] NULL,
	[Packets] [int] NULL,
	[TotalPackets] [int] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserName] [nchar](30) NOT NULL,
	[Password] [nchar](30) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 22-02-2023 21:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vendor](
	[VendorID] [nchar](20) NOT NULL,
	[Name] [nchar](100) NULL,
	[Address] [varchar](250) NULL,
	[Landmark] [varchar](250) NULL,
	[City] [nchar](50) NULL,
	[State] [nchar](50) NULL,
	[ZipCode] [nchar](10) NULL,
	[Phone] [nchar](15) NULL,
	[Email] [varchar](150) NULL,
	[MobileNo] [nchar](15) NULL,
	[FaxNo] [nchar](15) NULL,
	[Notes] [varchar](250) NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[BillInfo] ([InvoiceNo], [BillingDate], [CustomerNo], [CustomerName], [subTotal], [TaxPercentage], [TaxAmount], [GrandTotal], [TotalPayment], [PaymentDue]) VALUES (N'INV-54314881        ', N'18/Feb/2023                   ', N'SD-642569           ', N'Annachi Kadai                                                                                       ', 400, 18, 72, 472, 400, 72)
INSERT [dbo].[BillInfo] ([InvoiceNo], [BillingDate], [CustomerNo], [CustomerName], [subTotal], [TaxPercentage], [TaxAmount], [GrandTotal], [TotalPayment], [PaymentDue]) VALUES (N'INV-71811986        ', N'18/Feb/2023                   ', N'SD-642569           ', N'Annachi Kadai                                                                                       ', 400, 18, 72, 472, 472, 0)
INSERT [dbo].[Customer] ([B_Name], [B_Address], [B_Landmark], [B_city], [B_state], [B_zipcode], [S_Name], [S_address], [S_landmark], [S_city], [S_state], [S_zipcode], [CustomerNo], [Phone], [Email], [MobileNo], [FaxNo], [Notes]) VALUES (N'Annachi Kadai                                                                                       ', N'mettala', N'mettala', N'mettala                                           ', N'Tamil Nadu                                        ', N'636202    ', N'Annachi Kadai                                                                                       ', N'mettala', N'mettala', N'mettala                                           ', N'Tamil Nadu                                        ', N'636202    ', N'SD-642569           ', N'               ', N'', N'7897978979     ', N'               ', N'')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'1         ', N'Cement')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'10        ', N'Matt')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'11        ', N'ThrustiBommai')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'12        ', N'WeldingRod')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'13        ', N'Kadasal')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'14        ', N'JapanSheet')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'15        ', N'SquarePipe')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'16        ', N'RoundPipe')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'17        ', N'LAngle')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'18        ', N'Sponge')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'19        ', N'Aani')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'2         ', N'Steel')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'20        ', N'KattuKambi')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'3         ', N'Paint')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'4         ', N'Brush')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'5         ', N'PVCPipe')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'6         ', N'Brush')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'7         ', N'WhitePowder')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'8         ', N'PattyPowder')
INSERT [dbo].[InventoryCategory] ([CategoryID], [CategoryName]) VALUES (N'9         ', N'WhiteCement')
INSERT [dbo].[Product] ([ProductCode], [ProductName], [Category], [Weight], [Price], [MRP], [BPrice], [CreatedOn], [CreatedBy], [IsActive]) VALUES (N'P-1229              ', N'UltraTech', N'Cement', N'50        ', 400, 400, 380, CAST(N'2023-02-17 22:48:34.477' AS DateTime), NULL, NULL)
INSERT [dbo].[Product] ([ProductCode], [ProductName], [Category], [Weight], [Price], [MRP], [BPrice], [CreatedOn], [CreatedBy], [IsActive]) VALUES (N'P-1257              ', N'Abba', N'Brush', N'4         ', 200, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Product] ([ProductCode], [ProductName], [Category], [Weight], [Price], [MRP], [BPrice], [CreatedOn], [CreatedBy], [IsActive]) VALUES (N'P-1676              ', N'Arasu', N'Cement', N'400       ', 340, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Product] ([ProductCode], [ProductName], [Category], [Weight], [Price], [MRP], [BPrice], [CreatedOn], [CreatedBy], [IsActive]) VALUES (N'P-2918              ', N'Abba', N'Brush', N'5         ', 400, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Product] ([ProductCode], [ProductName], [Category], [Weight], [Price], [MRP], [BPrice], [CreatedOn], [CreatedBy], [IsActive]) VALUES (N'P-9124              ', N'Abba', N'Brush', N'2         ', 100, 120, 80, NULL, NULL, NULL)
INSERT [dbo].[ProductSold] ([InvoiceNo], [ProductCode], [ProductName], [Weight], [Price], [Cartons], [TotalPackets], [TotalAmount]) VALUES (N'INV-71811986        ', N'P-2918              ', N'Abba', N'5         ', 400, 1, 1, 400)
INSERT [dbo].[ProductSold] ([InvoiceNo], [ProductCode], [ProductName], [Weight], [Price], [Cartons], [TotalPackets], [TotalAmount]) VALUES (N'INV-54314881        ', N'P-2918              ', N'Abba', N'5         ', 400, 1, 1, 400)
INSERT [dbo].[Registration] ([Username], [Password], [Name], [ContactNo]) VALUES (N'Siva                          ', N'12345                         ', N'Siva                          ', N'9742966666     ')
INSERT [dbo].[Stock] ([StockID], [ProductCode], [ProductName], [Category], [Weight], [Stockdate], [Cartons], [Packets], [TotalPackets]) VALUES (N'ST-529915           ', N'P-1229              ', N'UltraTech', N'Cement', N'50        ', N'18/Feb/2023                   ', 50, 50, 2500)
INSERT [dbo].[Stock] ([StockID], [ProductCode], [ProductName], [Category], [Weight], [Stockdate], [Cartons], [Packets], [TotalPackets]) VALUES (N'ST-538329           ', N'P-2918              ', N'Abba', N'Brush', N'5         ', N'17/Feb/2023                   ', 3, 1, 3)
INSERT [dbo].[Users] ([UserName], [Password]) VALUES (N'admin                         ', N'12345                         ')
INSERT [dbo].[Users] ([UserName], [Password]) VALUES (N'Siva                          ', N'12345                         ')
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD  CONSTRAINT [FK_BillInfo_Customer] FOREIGN KEY([CustomerNo])
REFERENCES [dbo].[Customer] ([CustomerNo])
GO
ALTER TABLE [dbo].[BillInfo] CHECK CONSTRAINT [FK_BillInfo_Customer]
GO
ALTER TABLE [dbo].[OrderedProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderedProduct_OrderInfo] FOREIGN KEY([OrderNo])
REFERENCES [dbo].[OrderInfo] ([OrderNo])
GO
ALTER TABLE [dbo].[OrderedProduct] CHECK CONSTRAINT [FK_OrderedProduct_OrderInfo]
GO
ALTER TABLE [dbo].[OrderedProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderedProduct_Product] FOREIGN KEY([ProductCode])
REFERENCES [dbo].[Product] ([ProductCode])
GO
ALTER TABLE [dbo].[OrderedProduct] CHECK CONSTRAINT [FK_OrderedProduct_Product]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK_OrderInfo_Customer] FOREIGN KEY([CustomerNo])
REFERENCES [dbo].[Customer] ([CustomerNo])
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK_OrderInfo_Customer]
GO
ALTER TABLE [dbo].[ProductSold]  WITH CHECK ADD  CONSTRAINT [FK_ProductSold_BillInfo] FOREIGN KEY([InvoiceNo])
REFERENCES [dbo].[BillInfo] ([InvoiceNo])
GO
ALTER TABLE [dbo].[ProductSold] CHECK CONSTRAINT [FK_ProductSold_BillInfo]
GO
ALTER TABLE [dbo].[ProductSold]  WITH CHECK ADD  CONSTRAINT [FK_ProductSold_Product] FOREIGN KEY([ProductCode])
REFERENCES [dbo].[Product] ([ProductCode])
GO
ALTER TABLE [dbo].[ProductSold] CHECK CONSTRAINT [FK_ProductSold_Product]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Product] FOREIGN KEY([ProductCode])
REFERENCES [dbo].[Product] ([ProductCode])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Product]
GO
USE [master]
GO
ALTER DATABASE [SIM] SET  READ_WRITE 
GO

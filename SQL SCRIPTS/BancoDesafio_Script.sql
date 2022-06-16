USE [master]
GO
/****** Object:  Database [BANCODESAFIO]    Script Date: 6/16/2022 6:37:42 PM ******/
CREATE DATABASE [BANCODESAFIO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BANCODESAFIO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BANCODESAFIO.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BANCODESAFIO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BANCODESAFIO_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BANCODESAFIO] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BANCODESAFIO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BANCODESAFIO] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET ARITHABORT OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BANCODESAFIO] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BANCODESAFIO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BANCODESAFIO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BANCODESAFIO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BANCODESAFIO] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BANCODESAFIO] SET  MULTI_USER 
GO
ALTER DATABASE [BANCODESAFIO] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BANCODESAFIO] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BANCODESAFIO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BANCODESAFIO] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BANCODESAFIO] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BANCODESAFIO] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BANCODESAFIO] SET QUERY_STORE = OFF
GO
USE [BANCODESAFIO]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 6/16/2022 6:37:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Uf] [varchar](2) NOT NULL,
	[Celular] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Financiamento]    Script Date: 6/16/2022 6:37:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Financiamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoFinanciamento] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
	[ValorTotal] [decimal](18, 0) NOT NULL,
	[DataVencimento] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parcela]    Script Date: 6/16/2022 6:37:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parcela](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdFinanciamento] [int] NOT NULL,
	[NumeroParcela] [int] NOT NULL,
	[ValorParcela] [decimal](18, 0) NOT NULL,
	[DataVencimento] [datetime] NOT NULL,
	[DataPagamento] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoFinanciamento]    Script Date: 6/16/2022 6:37:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoFinanciamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Taxa] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Uf], [Celular]) VALUES (1, N'Paulo Zuchini', N'SP', N'1999999999')
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Uf], [Celular]) VALUES (2, N'Paula Gomes', N'SP', N'1999999999')
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Uf], [Celular]) VALUES (3, N'Yoshi Charles', N'SP', N'1999999999')
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Uf], [Celular]) VALUES (4, N'Yuki Tanuki', N'SP', N'1999999999')
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Uf], [Celular]) VALUES (5, N'Johnny Bravo', N'Sp', N'1999999999')
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Uf], [Celular]) VALUES (6, N'Capitao Caverna', N'SP', N'1999999999')
GO
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Financiamento] ON 
GO
INSERT [dbo].[Financiamento] ([Id], [TipoFinanciamento], [IdCliente], [ValorTotal], [DataVencimento]) VALUES (1, 1, 1, CAST(50000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Financiamento] ([Id], [TipoFinanciamento], [IdCliente], [ValorTotal], [DataVencimento]) VALUES (2, 1, 2, CAST(20000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Financiamento] ([Id], [TipoFinanciamento], [IdCliente], [ValorTotal], [DataVencimento]) VALUES (3, 4, 3, CAST(500000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Financiamento] ([Id], [TipoFinanciamento], [IdCliente], [ValorTotal], [DataVencimento]) VALUES (4, 2, 4, CAST(100000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Financiamento] ([Id], [TipoFinanciamento], [IdCliente], [ValorTotal], [DataVencimento]) VALUES (5, 1, 5, CAST(50000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Financiamento] ([Id], [TipoFinanciamento], [IdCliente], [ValorTotal], [DataVencimento]) VALUES (6, 1, 6, CAST(50000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Financiamento] OFF
GO
SET IDENTITY_INSERT [dbo].[Parcela] ON 
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (1, 1, 1, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (2, 1, 2, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-08-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (3, 1, 3, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-09-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (4, 1, 4, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-10-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (5, 1, 5, CAST(11000 AS Decimal(18, 0)), CAST(N'2022-11-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (7, 2, 1, CAST(4000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (8, 2, 2, CAST(4000 AS Decimal(18, 0)), CAST(N'2022-08-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (9, 2, 3, CAST(4000 AS Decimal(18, 0)), CAST(N'2022-09-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (10, 2, 4, CAST(4000 AS Decimal(18, 0)), CAST(N'2022-10-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (11, 2, 5, CAST(4400 AS Decimal(18, 0)), CAST(N'2022-11-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (12, 3, 1, CAST(100000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (13, 3, 2, CAST(100000 AS Decimal(18, 0)), CAST(N'2022-06-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (14, 3, 3, CAST(100000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (15, 3, 4, CAST(100000 AS Decimal(18, 0)), CAST(N'2022-08-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (16, 3, 5, CAST(100000 AS Decimal(18, 0)), CAST(N'2022-09-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (17, 4, 1, CAST(20000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime), CAST(N'2022-07-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (18, 4, 2, CAST(20000 AS Decimal(18, 0)), CAST(N'2022-06-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (19, 4, 3, CAST(20000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (20, 4, 4, CAST(20000 AS Decimal(18, 0)), CAST(N'2022-08-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (21, 4, 5, CAST(20200 AS Decimal(18, 0)), CAST(N'2022-09-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (22, 5, 1, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime), CAST(N'2022-05-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (23, 5, 2, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-06-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (24, 5, 3, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (25, 5, 4, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-08-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (26, 5, 5, CAST(11000 AS Decimal(18, 0)), CAST(N'2022-09-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (27, 6, 1, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-05-15T00:00:00.000' AS DateTime), CAST(N'2022-05-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (28, 6, 2, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-06-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (29, 6, 3, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-07-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (30, 6, 4, CAST(10000 AS Decimal(18, 0)), CAST(N'2022-08-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Parcela] ([Id], [IdFinanciamento], [NumeroParcela], [ValorParcela], [DataVencimento], [DataPagamento]) VALUES (31, 6, 5, CAST(11000 AS Decimal(18, 0)), CAST(N'2022-09-15T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Parcela] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoFinanciamento] ON 
GO
INSERT [dbo].[TipoFinanciamento] ([Id], [Nome], [Taxa]) VALUES (1, N'Direto', CAST(2 AS Decimal(18, 0)))
GO
INSERT [dbo].[TipoFinanciamento] ([Id], [Nome], [Taxa]) VALUES (2, N'Consignado', CAST(1 AS Decimal(18, 0)))
GO
INSERT [dbo].[TipoFinanciamento] ([Id], [Nome], [Taxa]) VALUES (3, N'Pessoa Juridica', CAST(5 AS Decimal(18, 0)))
GO
INSERT [dbo].[TipoFinanciamento] ([Id], [Nome], [Taxa]) VALUES (4, N'Pessoa Fisica', CAST(3 AS Decimal(18, 0)))
GO
INSERT [dbo].[TipoFinanciamento] ([Id], [Nome], [Taxa]) VALUES (5, N'Imobiliario', CAST(9 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[TipoFinanciamento] OFF
GO
ALTER TABLE [dbo].[Financiamento]  WITH CHECK ADD  CONSTRAINT [fk_IdCliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([Id])
GO
ALTER TABLE [dbo].[Financiamento] CHECK CONSTRAINT [fk_IdCliente]
GO
ALTER TABLE [dbo].[Financiamento]  WITH CHECK ADD  CONSTRAINT [fk_TipoFinanciamento] FOREIGN KEY([TipoFinanciamento])
REFERENCES [dbo].[TipoFinanciamento] ([Id])
GO
ALTER TABLE [dbo].[Financiamento] CHECK CONSTRAINT [fk_TipoFinanciamento]
GO
ALTER TABLE [dbo].[Parcela]  WITH CHECK ADD  CONSTRAINT [fk_IdFinanciamento] FOREIGN KEY([IdFinanciamento])
REFERENCES [dbo].[Financiamento] ([Id])
GO
ALTER TABLE [dbo].[Parcela] CHECK CONSTRAINT [fk_IdFinanciamento]
GO
USE [master]
GO
ALTER DATABASE [BANCODESAFIO] SET  READ_WRITE 
GO

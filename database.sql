USE [master]
GO
/****** Object:  Database [ecommerce]    Script Date: 02-10-2020 03:19:59 AM ******/
CREATE DATABASE [ecommerce]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ecommerce', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ecommerce.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ecommerce_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ecommerce_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ecommerce] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ecommerce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ecommerce] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ecommerce] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ecommerce] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ecommerce] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ecommerce] SET ARITHABORT OFF 
GO
ALTER DATABASE [ecommerce] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ecommerce] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ecommerce] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ecommerce] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ecommerce] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ecommerce] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ecommerce] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ecommerce] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ecommerce] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ecommerce] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ecommerce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ecommerce] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ecommerce] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ecommerce] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ecommerce] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ecommerce] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ecommerce] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ecommerce] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ecommerce] SET  MULTI_USER 
GO
ALTER DATABASE [ecommerce] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ecommerce] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ecommerce] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ecommerce] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ecommerce] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ecommerce] SET QUERY_STORE = OFF
GO
USE [ecommerce]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 02-10-2020 03:19:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[admin_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](256) NOT NULL,
	[email] [varchar](256) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[phone] [varchar](256) NOT NULL,
 CONSTRAINT [PK_admin] PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 02-10-2020 03:19:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[cart_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[prod_id] [int] NOT NULL,
 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED 
(
	[cart_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 02-10-2020 03:19:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[cat_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](256) NOT NULL,
	[isactive] [bit] NOT NULL,
 CONSTRAINT [PK_categories] PRIMARY KEY CLUSTERED 
(
	[cat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 02-10-2020 03:19:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[prod_id] [int] NOT NULL,
	[status] [int] NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 02-10-2020 03:19:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[prod_id] [int] IDENTITY(1,1) NOT NULL,
	[cat_id] [int] NOT NULL,
	[title] [varchar](256) NOT NULL,
	[description] [varchar](max) NOT NULL,
	[image] [varchar](256) NOT NULL,
	[isactive] [bit] NOT NULL,
	[isfeatured] [bit] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [float] NOT NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[prod_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slider]    Script Date: 02-10-2020 03:19:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slider](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](256) NOT NULL,
	[image] [varchar](256) NOT NULL,
	[redirect] [varchar](256) NOT NULL,
 CONSTRAINT [PK_slider] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 02-10-2020 03:19:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](256) NOT NULL,
	[email] [varchar](256) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[phone] [varchar](256) NOT NULL,
	[address] [varchar](max) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[admin] ON 

INSERT [dbo].[admin] ([admin_id], [name], [email], [password], [phone]) VALUES (1, N'Admin', N'admin@mail.com', N'Admin123', N'123456789')
SET IDENTITY_INSERT [dbo].[admin] OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([cat_id], [name], [isactive]) VALUES (1, N'Mobiles', 1)
INSERT [dbo].[categories] ([cat_id], [name], [isactive]) VALUES (3, N'Watches', 1)
SET IDENTITY_INSERT [dbo].[categories] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (1, 1, 1, 3, CAST(N'2020-10-02T00:52:01.237' AS DateTime), CAST(N'2020-10-02T02:45:44.810' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (2, 1, 1, 1, CAST(N'2020-10-02T00:52:45.897' AS DateTime), CAST(N'2020-10-02T00:52:45.897' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (3, 1, 1, 1, CAST(N'2020-10-02T00:53:53.117' AS DateTime), CAST(N'2020-10-02T00:53:53.117' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (4, 1, 2, 1, CAST(N'2020-10-02T01:00:40.880' AS DateTime), CAST(N'2020-10-02T01:00:40.880' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (5, 1, 1, 2, CAST(N'2020-10-02T01:16:34.797' AS DateTime), CAST(N'2020-10-02T02:45:59.097' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (6, 1, 2, 3, CAST(N'2020-10-02T01:16:34.807' AS DateTime), CAST(N'2020-10-02T02:45:40.360' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (7, 1, 1, 4, CAST(N'2020-10-02T02:15:26.130' AS DateTime), CAST(N'2020-10-02T02:15:26.130' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (8, 1, 3, 4, CAST(N'2020-10-02T02:15:26.190' AS DateTime), CAST(N'2020-10-02T02:15:26.190' AS DateTime))
INSERT [dbo].[orders] ([order_id], [user_id], [prod_id], [status], [created_at], [updated_at]) VALUES (9, 1, 1, 4, CAST(N'2020-10-02T02:15:26.197' AS DateTime), CAST(N'2020-10-02T02:15:26.197' AS DateTime))
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([prod_id], [cat_id], [title], [description], [image], [isactive], [isfeatured], [quantity], [price]) VALUES (1, 3, N'Watch', N'Add New Products', N'f9259d3e-93e7-4e71-b781-9a02985e045b_1170-bl-br-fogg-original-imafdv994mjj8z69.jpeg', 1, 1, 28, 12)
INSERT [dbo].[products] ([prod_id], [cat_id], [title], [description], [image], [isactive], [isfeatured], [quantity], [price]) VALUES (2, 1, N'Realme Narzo 10A (So White, 32 GB)  (3 GB RAM)', N'Realme Narzo 10A 
(So White, 32 GB)  
(3 GB RAM)', N'ec30085b-744c-476d-8c69-7e8c738b3e5f_realme-narzo-10a-rmx2020-original-imafrvptd3nd8yqe.jpeg', 1, 1, 8, 8999)
INSERT [dbo].[products] ([prod_id], [cat_id], [title], [description], [image], [isactive], [isfeatured], [quantity], [price]) VALUES (3, 1, N'Samsung Galaxy M11 (Black, 32 GB)  (3 GB RAM)', N'Samsung Galaxy M11 (Black, 32 GB)  (3 GB RAM)', N'842beadd-24fc-461a-9a36-301106e27e52_samsung-galaxy-m11-sm-m115fzkeins-original-imafscjyhxwghgwf.jpeg', 1, 0, 10, 10858)
INSERT [dbo].[products] ([prod_id], [cat_id], [title], [description], [image], [isactive], [isfeatured], [quantity], [price]) VALUES (4, 3, N'LimeStone LS2803 All Black Mesh Strap Day and Date Functioning Quartz Analog Watch - For Men', N'LimeStone LS2803 All Black Mesh Strap Day and Date Functioning Quartz Analog Watch - For Men', N'fee96785-bde8-4861-b2ab-5087bd2e7f7d_skm-1155-blue-skmei-original-imafsvz4jx3zvqd9.jpeg', 1, 1, 10, 450)
SET IDENTITY_INSERT [dbo].[products] OFF
GO
SET IDENTITY_INSERT [dbo].[slider] ON 

INSERT [dbo].[slider] ([id], [title], [image], [redirect]) VALUES (1, N'Test Title', N'1b06f782-eac1-4126-a110-c0e1bda4973a_download (3).jfif', N'/products')
INSERT [dbo].[slider] ([id], [title], [image], [redirect]) VALUES (2, N'Slider 2', N'697b67f9-f240-48f9-aa5e-ac1b3f7dd1ec_download (2).jfif', N'/products')
SET IDENTITY_INSERT [dbo].[slider] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [name], [email], [password], [phone], [address]) VALUES (1, N'Mohammed Saad', N'saad.13.personal@gmail.com', N'Saad123', N'8884291607', N'BTM layout, EWS colony')
SET IDENTITY_INSERT [dbo].[users] OFF
GO
USE [master]
GO
ALTER DATABASE [ecommerce] SET  READ_WRITE 
GO

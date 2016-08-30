/****** Object:  Table [dbo].[Account]    Script Date: 06/08/2016 11:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](255) NULL,
	[DisplayName] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [dbo].[ExternalAccount](
	[ExternalAccountId] [int] IDENTITY(1,1) NOT NULL,
	[LoginProvider] [nvarchar](255) NULL,
	[ProviderKey] [nvarchar](255) NULL,
	[AccountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExternalAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Client]    Script Date: 06/08/2016 11:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Secret] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[CanProvideSecret] [bit] NULL,
	[Active] [bit] NULL,
	[RefreshTokenLifeTime] [int] NULL,
	[AllowedOrigin] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 06/08/2016 11:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[RefreshTokenId] [uniqueidentifier] NOT NULL,
	[HashedTokenId] [nvarchar](255) NULL,
	[Subject] [nvarchar](255) NULL,
	[ProtectedTicket] [nvarchar](2000) NULL,
	[IssuedDateUtc] [datetime] NULL,
	[ExpectedExpiryDateUtc] [datetime] NULL,
	[ClientId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Venue]    Script Date: 06/08/2016 11:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venue](
	[VenueId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[OwnerAccountId] [int] NULL,
	[VenuePlaceId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VenueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[VenuePlace]    Script Date: 06/08/2016 11:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VenuePlace](
	[VenuePlaceId] [int] IDENTITY(1,1) NOT NULL,
	[PlaceId] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[VenuePlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
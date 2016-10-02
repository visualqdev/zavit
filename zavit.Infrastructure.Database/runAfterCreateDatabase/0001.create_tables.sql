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
	[AccountType] [int] DEFAULT 0
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[IsDefault] [bit] DEFAULT 0,
PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venue](
	[VenueId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Address] [nvarchar](500) NULL,
	[OwnerAccountId] [int] NULL,
	[VenuePlaceId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VenueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VenuePlace](
	[VenuePlaceId] [int] IDENTITY(1,1) NOT NULL,
	[PlaceId] [nvarchar](255) NULL,
	[Address] [nvarchar](500) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[VenuePlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ActivityToVenue](
	[VenueId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL
)

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VenueMembership](
	[VenueMembershipId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[VenueId] [int] NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[VenueMembershipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityToVenueMembership](
	[VenueMembershipId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL
)

GO
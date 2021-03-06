USE [master]
GO

DECLARE @FLG_LIMPA_BASE BIT = 0
IF @FLG_LIMPA_BASE = 1
BEGIN

	DROP TABLE [Pedido]
	DROP TABLE [User]
	DROP TABLE [Cliente]
	DROP TABLE [Drone]
	DROP TABLE [DroneItinerario]

END

IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.DATABASES WHERE NAME = 'DroneDelivery')
BEGIN
	CREATE DATABASE [DroneDelivery]
END
GO

USE [DroneDelivery]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.TABLES WHERE NAME = 'Cliente' )
BEGIN

	CREATE TABLE [dbo].[Cliente](
		[Id] [uniqueidentifier] NOT NULL,
		[Nome] [varchar](100) NULL,
		[Latitude] [float] NULL,
		[Longitude] [float] NULL,
	 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END


IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.TABLES WHERE NAME = 'Drone' )
BEGIN

	CREATE TABLE [dbo].[Drone](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Capacidade] [int] NOT NULL,
		[Velocidade] [int] NOT NULL,
		[Autonomia] [int] NOT NULL,
		[Carga] [int] NOT NULL,
		[AutonomiaRestante] [int] NULL,
	 CONSTRAINT [PK_Drone] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.TABLES WHERE NAME = 'DroneItinerario' )
BEGIN

	CREATE TABLE [dbo].[DroneItinerario](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[DataHora] [datetime] NULL,
		[DroneId] [int] NOT NULL,
		[StatusDrone] [int] NOT NULL,
	 CONSTRAINT [PK_DroneItinerario] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END


IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.TABLES WHERE NAME = 'Pedido' )
BEGIN

	CREATE TABLE [dbo].[Pedido](
		[Id] [uniqueidentifier] NOT NULL,
		[Peso] [int] NOT NULL,
		[Valor] [float] NOT NULL,
		[DataHora] [datetime] NOT NULL,
		[DroneId] [int] NULL,
		[Status] [int] NULL,
		[ClienteId] [uniqueidentifier] NULL,
	 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.TABLES WHERE NAME = 'User' )
BEGIN

	CREATE TABLE [dbo].[User](
		[Id] [uniqueidentifier] NOT NULL,
		[UserName] [varchar](50) NOT NULL,
		[Password] [varchar](250) NOT NULL,
		[Role] [varchar](50) NULL,
		[ClienteId] [uniqueidentifier] NULL,
	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.OBJECTS WHERE NAME = 'FK_DroneItinerario_Drone') 
BEGIN 
	ALTER TABLE [dbo].[DroneItinerario]  WITH CHECK ADD  CONSTRAINT [FK_DroneItinerario_Drone] FOREIGN KEY([DroneId])
	REFERENCES [dbo].[Drone] ([Id])

	ALTER TABLE [dbo].[DroneItinerario] CHECK CONSTRAINT [FK_DroneItinerario_Drone]

END
GO

IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.OBJECTS WHERE NAME = 'FK_Pedido_Client') 
BEGIN 
	ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD CONSTRAINT [FK_Pedido_Client] FOREIGN KEY([ClienteId])
	REFERENCES [dbo].[Cliente] ([Id])

	ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Client]
END
GO

IF NOT EXISTS ( SELECT TOP 1 1 FROM SYS.OBJECTS WHERE NAME = 'FK_User_Client') 
BEGIN 
	ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Client] FOREIGN KEY([ClienteId])
	REFERENCES [dbo].[Cliente] ([Id])

	ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Client]
END
GO



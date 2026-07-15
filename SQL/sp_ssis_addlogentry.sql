/*

If you are using the SSIS logging feature, you can use this stored procedure to add log entries to the sysssislog table. 
This procedure is typically called by the SSIS runtime when logging events occur during package execution.

If you are running SSIS packages from AutoSys make sure you set permissions on the table and stored procedure for the AutoSys user.
*/

CREATE TABLE [dbo].[sysssislog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[event] [sysname] NOT NULL,
	[computer] [nvarchar](128) NOT NULL,
	[operator] [nvarchar](128) NOT NULL,
	[source] [nvarchar](1024) NOT NULL,
	[sourceid] [uniqueidentifier] NOT NULL,
	[executionid] [uniqueidentifier] NOT NULL,
	[starttime] [datetime] NOT NULL,
	[endtime] [datetime] NOT NULL,
	[datacode] [int] NOT NULL,
	[databytes] [image] NULL,
	[message] [nvarchar](2048) NOT NULL,
 CONSTRAINT [PK_sysssislog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[sp_ssis_addlogentry]
    @event sysname,
    @computer nvarchar(128),
    @operator nvarchar(128),
    @source nvarchar(1024),
    @sourceid uniqueidentifier,
    @executionid uniqueidentifier,
    @starttime datetime,
    @endtime datetime,
    @datacode int,
    @databytes image,
    @message nvarchar(2048)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[sysssislog] (
        [event],
        [computer],
        [operator],
        [source],
        [sourceid],
        [executionid],
        [starttime],
        [endtime],
        [datacode],
        [databytes],
        [message]
    )
    VALUES (
        @event,
        @computer,
        @operator,
        @source,
        @sourceid,
        @executionid,
        @starttime,
        @endtime,
        @datacode,
        @databytes,
        @message
    );
    
    RETURN 0;
END;
GO
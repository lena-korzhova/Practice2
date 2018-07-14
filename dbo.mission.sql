CREATE TABLE [dbo].[mission] (
    [Id]          INT      NOT NULL,
    [ns]          INT      NULL,
    [epoch_start] DATETIME NULL,
    [epoch_fin]   DATETIME NULL,
    [health]      INT      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
	
);


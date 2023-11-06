CREATE TABLE [Matches] (
    [Id] nvarchar(64) NOT NULL,
    [Description] nvarchar(max) NULL,
    [MatchDate] datetime2 NOT NULL,
    [MatchTime] time NOT NULL,
    [TeamA] nvarchar(max) NULL,
    [TeamB] nvarchar(max) NULL,
    [Sport] int NOT NULL,
    CONSTRAINT [PK_Matches] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [MatchOdds] (
    [Id] nvarchar(450) NOT NULL,
    [MatchId] nvarchar(max) NOT NULL,
    [Specifier] nvarchar(max) NOT NULL,
    [Odd] float NOT NULL,
    CONSTRAINT [PK_MatchOdds] PRIMARY KEY ([Id])
);
GO



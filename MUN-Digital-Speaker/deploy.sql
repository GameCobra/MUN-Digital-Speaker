IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Delegation] (
    [Id] int NOT NULL IDENTITY,
    [Country] nvarchar(max) NULL,
    [TimesSpoken] int NULL,
    [RequestedToSpeak] bit NULL,
    CONSTRAINT [PK_Delegation] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250806032118_InitialCreate', N'9.0.0');

ALTER TABLE [Delegation] ADD [Key] int NOT NULL DEFAULT 0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250807030018_AddNotesToDelegation', N'9.0.0');

ALTER TABLE [Delegation] ADD [Login] int NOT NULL DEFAULT 0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250808020324_AddLoginToDelegation', N'9.0.0');

CREATE TABLE [Amendment] (
    [DelegationId] int NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [ResolutionID] int NOT NULL,
    [Metadata] nvarchar(max) NULL,
    [Change] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Amendment] PRIMARY KEY ([DelegationId], [Id]),
    CONSTRAINT [FK_Amendment_Delegation_DelegationId] FOREIGN KEY ([DelegationId]) REFERENCES [Delegation] ([Id]) ON DELETE CASCADE
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251006025323_AddAmendmentOverride', N'9.0.0');

ALTER TABLE [Amendment] ADD [ClauseNumber] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260104012508_UpdatedAmendments', N'9.0.0');

ALTER TABLE [Delegation] ADD [AmendmentPoints] int NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260113025444_AddedAmendmentPoints', N'9.0.0');

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Delegation]') AND [c].[name] = N'RequestedToSpeak');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Delegation] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Delegation] DROP COLUMN [RequestedToSpeak];

ALTER TABLE [Delegation] ADD [RequestedToSpeakECO] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Delegation] ADD [RequestedToSpeakENV] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Delegation] ADD [RequestedToSpeakGEN] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Delegation] ADD [RequestedToSpeakHE] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Delegation] ADD [RequestedToSpeakSEC] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260213163305_councels', N'9.0.0');

COMMIT;
GO


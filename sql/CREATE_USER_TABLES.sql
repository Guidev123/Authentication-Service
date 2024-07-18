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
GO

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [EmailVerificationCode] nvarchar(255) NOT NULL,
    [EmailExpiresAt] datetime2 NULL,
    [EmailVerifiedAt] datetime2 NULL,
    [PasswordHash] nvarchar(255) NOT NULL,
    [PasswordResetCode] nvarchar(255) NOT NULL,
    [Image] VARCHAR(255) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240718161952_Initial', N'8.0.7');
GO

COMMIT;
GO


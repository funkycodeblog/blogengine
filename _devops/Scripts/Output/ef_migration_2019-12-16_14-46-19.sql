IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    CREATE TABLE [Hobbies] (
        [Name] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_Hobbies] PRIMARY KEY ([Name])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    CREATE TABLE [Skills] (
        [Name] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_Skills] PRIMARY KEY ([Name])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    CREATE TABLE [Users] (
        [Id] nvarchar(450) NOT NULL,
        [Email] nvarchar(50) NOT NULL,
        [DisplayName] nvarchar(50) NOT NULL,
        [Role] nvarchar(50) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    CREATE TABLE [UserHobby] (
        [UserId] nvarchar(450) NOT NULL,
        [HobbyId] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_UserHobby] PRIMARY KEY ([UserId], [HobbyId]),
        CONSTRAINT [FK_UserHobby_Hobbies_HobbyId] FOREIGN KEY ([HobbyId]) REFERENCES [Hobbies] ([Name]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserHobby_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    CREATE TABLE [UserSkill] (
        [UserId] nvarchar(450) NOT NULL,
        [SkillId] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_UserSkill] PRIMARY KEY ([UserId], [SkillId]),
        CONSTRAINT [FK_UserSkill_Skills_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skills] ([Name]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserSkill_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    CREATE INDEX [IX_UserHobby_HobbyId] ON [UserHobby] ([HobbyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    CREATE INDEX [IX_UserSkill_SkillId] ON [UserSkill] ([SkillId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191126123928_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191126123928_Init', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191210125343_AddUserAvatarsAndSkype')
BEGIN
    ALTER TABLE [Users] ADD [Skype] nvarchar(50) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191210125343_AddUserAvatarsAndSkype')
BEGIN
    CREATE TABLE [UserAvatars] (
        [Id] nvarchar(450) NOT NULL,
        [Avatar] nvarchar(max) NULL,
        CONSTRAINT [PK_UserAvatars] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191210125343_AddUserAvatarsAndSkype')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191210125343_AddUserAvatarsAndSkype', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191214174853_AddContactTable')
BEGIN
    ALTER TABLE [Users] ADD [Status] nvarchar(max) NOT NULL DEFAULT N'New';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191214174853_AddContactTable')
BEGIN
    CREATE TABLE [Contacts] (
        [InitiatorId] nvarchar(450) NOT NULL,
        [ReceiverId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Contacts] PRIMARY KEY ([InitiatorId], [ReceiverId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191214174853_AddContactTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191214174853_AddContactTable', N'2.2.6-servicing-10079');
END;

GO


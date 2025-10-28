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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251023033111_Inicial'
)
BEGIN
    CREATE TABLE [Produtos] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(100) NOT NULL,
        [Categoria] nvarchar(50) NOT NULL,
        [Preco] decimal(18,2) NOT NULL,
        [Quantidade] int NOT NULL,
        CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251023033111_Inicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251023033111_Inicial', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251024024608_CriarTabelaUsuarios'
)
BEGIN
    CREATE TABLE [Usuarios] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(100) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [SenhaHash] nvarchar(255) NOT NULL,
        [TipoUsuario] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251024024608_CriarTabelaUsuarios'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251024024608_CriarTabelaUsuarios', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026221614_CriarTabelaVendas'
)
BEGIN
    CREATE TABLE [Vendas] (
        [Id] int NOT NULL IDENTITY,
        [DataVenda] datetime2 NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        [UsuarioNome] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Vendas] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026221614_CriarTabelaVendas'
)
BEGIN
    CREATE TABLE [ItensVenda] (
        [Id] int NOT NULL IDENTITY,
        [ProdutoId] int NOT NULL,
        [Quantidade] int NOT NULL,
        [Subtotal] decimal(18,2) NOT NULL,
        [VendaId] int NOT NULL,
        CONSTRAINT [PK_ItensVenda] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ItensVenda_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ItensVenda_Vendas_VendaId] FOREIGN KEY ([VendaId]) REFERENCES [Vendas] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026221614_CriarTabelaVendas'
)
BEGIN
    CREATE INDEX [IX_ItensVenda_ProdutoId] ON [ItensVenda] ([ProdutoId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026221614_CriarTabelaVendas'
)
BEGIN
    CREATE INDEX [IX_ItensVenda_VendaId] ON [ItensVenda] ([VendaId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026221614_CriarTabelaVendas'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251026221614_CriarTabelaVendas', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026222024_CriarTabelaItensVenda'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251026222024_CriarTabelaItensVenda', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251027000026_AddPrecoUnitarioToItemVenda'
)
BEGIN
    EXEC sp_rename N'[Produtos].[Quantidade]', N'Estoque', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251027000026_AddPrecoUnitarioToItemVenda'
)
BEGIN
    ALTER TABLE [ItensVenda] ADD [PrecoUnitario] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251027000026_AddPrecoUnitarioToItemVenda'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251027000026_AddPrecoUnitarioToItemVenda', N'8.0.11');
END;
GO

COMMIT;
GO


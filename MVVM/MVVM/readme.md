1. Создать БД с именем Lesson7 

2. Создать таблицы Department:

CREATE TABLE [dbo].[Department] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

и Employee:

CREATE TABLE [dbo].[Employee] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [Age]          INT           NOT NULL,
    [DepartmentId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_ToDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id])
	ON DELETE CASCADE
);


3. Для заполнения тестовыми данными выполнить:

INSERT INTO Department (Title) VALUES (N'Департамент_1')
INSERT INTO Department (Title) VALUES (N'Департамент_2')
INSERT INTO Department (Title) VALUES (N'Департамент_3')
INSERT INTO Department (Title) VALUES (N'Департамент_4')
INSERT INTO Department (Title) VALUES (N'Департамент_5')

INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_1', N'Фамилия_1', '25', '1')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_2', N'Фамилия_2', '24', '2')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_3', N'Фамилия_3', '27', '3')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_4', N'Фамилия_4', '23', '4')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_5', N'Фамилия_5', '26', '5')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_6', N'Фамилия_6', '28', '1')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_7', N'Фамилия_7', '29', '2')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_8', N'Фамилия_8', '30', '3')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_9', N'Фамилия_9', '22', '4')
INSERT INTO Employee (FirstName, LastName, Age, DepartmentId) VALUES (N'Имя_10', N'Фамилия_10', '31', '5')
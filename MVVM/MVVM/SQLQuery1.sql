select * from Employee

select * from Department
SELECT MAX(id) FROM Employee
select * from Employee
left join Department on Employee.DepartmentId=Department.Id;

delete  from Employee where Id>0
delete  from Department where Id>0

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

INSERT INTO Department (Title) VALUES (N'Департамент_1')
INSERT INTO Department (Title) VALUES (N'Департамент_2')
INSERT INTO Department (Title) VALUES (N'Департамент_3')
INSERT INTO Department (Title) VALUES (N'Департамент_4')
INSERT INTO Department (Title) VALUES (N'Департамент_5')

DBCC CHECKIDENT ('Employee', RESEED, 0)
DBCC CHECKIDENT ('Department', RESEED, 0)
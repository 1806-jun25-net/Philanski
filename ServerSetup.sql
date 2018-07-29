

Insert Departments 
Values ('Engineering')

Insert Worksites
Values ('123 Thunderbluff St.', null, 'Thunderbluff', 'AZ', '42696')

Insert Worksites
Values ('123 Undercity St.', null, 'Undercity', 'AZ', '42696')

Insert Worksites
Values ('123 Orgimmar St.', null, 'Orgimmar', 'AZ', '42696')

Insert Employees
Values ('Sylvanas', 'Windrunner', 'swindrunner@gmail.com', 'Queen', 2, 60000.00, '20180618 10:34:09 AM', null)

Insert Employees
Values ('Garrosh', 'Hellscream', 'warchief@gmail.com', 'Warchief', 3, 80000.00, '20180615 10:34:09 AM', null)

Insert EmployeeDepartments
Values (1,1)

Insert EmployeeDepartments
Values (2,1)

Insert Managers
Values (2)

Insert ManagerDepartments
Values (1,1)

Insert TimeSheets
Values (1, '2018-07-23', 8.00)
Insert TimeSheets
Values (1, '2018-07-24', 8.00)
Insert TimeSheets
Values (1, '2018-07-25', 8.00)
Insert TimeSheets
Values (1, '2018-07-26', 8.00)
Insert TimeSheets
Values (1, '2018-07-27', 8.00)


Insert TimeSheetApprovals
Values ('2018-07-22', '2018-07-28', 40.00, 0, null, '20180724 10:34:09 AM',1)

Drop table TimeSheetApprovals

Create Table TimeSheetApprovals
(ID int identity(1,1) not null, WeekStart date not null, WeekEnd date not null, WeekTotalRegular decimal(5,2) not null, Status nvarchar(1) not null, ApprovingManagerID int null, TimeSubmitted datetime not null, EmployeeID int not null)


select * from Employees
select * from Managers
select * from Worksites
select * from TimeSheets
select * from TimeSheetApprovals
select * from EmployeeDepartments
select * from ManagerDepartments
select * from Departments
select * from AspNetRoleClaims
select * from AspNetUsers
select * from AspNetUserLogins
select * from AspNetRoles

Delete from Departments
Where id = 3

ALTER TABLE Employee
ADD FOREIGN KEY (WorksiteID) REFERENCES Worksite(ID);

Alter TABLE EmployeeDepartments
ADD FOREIGN KEY (EmployeeID) REFERENCES Employees(ID)

Alter TABLE EmployeeDepartments
ADD FOREIGN KEY (DepartmentID) REFERENCES Departments(ID)

Alter TABLE Manager
ADD FOREIGN KEY (EmployeeID) REFERENCES Employee(ID)

Alter TABLE ManagerDepartments
ADD FOREIGN KEY (ManagerID) REFERENCES Managers(ID)

Alter TABLE ManagerDepartments
ADD FOREIGN KEY (DepartmentID) REFERENCES Departments(ID)

Alter TABLE TimeSheets
ADD FOREIGN KEY (EmployeeID) REFERENCES Employees(ID)

Alter TABLE TimeSheetApprovals
ADD FOREIGN KEY (EmployeeID) REFERENCES Employees(ID)

Alter TABLE TimeSheetApprovals
ADD FOREIGN KEY (ApprovingManagerID) REFERENCES Managers(ID)


Alter Table TimeSheetApprovals
Add Primary Key (ID)

Alter TABLE TimeSheetApprovals
ADD FOREIGN KEY (TimeSheetID) REFERENCES TimeSheets(ID)

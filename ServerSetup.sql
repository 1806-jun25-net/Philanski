ALTER TABLE Employee
ADD FOREIGN KEY (WorksiteID) REFERENCES Worksite(ID);

Alter TABLE EmployeeDepartment
ADD FOREIGN KEY (EmployeeID) REFERENCES Employee(ID)

Alter TABLE EmployeeDepartment
ADD FOREIGN KEY (DepartmentID) REFERENCES Department(ID)

Alter TABLE Manager
ADD FOREIGN KEY (EmployeeID) REFERENCES Employee(ID)

Alter TABLE ManagerDepartment
ADD FOREIGN KEY (ManagerID) REFERENCES Manager(ID)

Alter TABLE ManagerDepartment
ADD FOREIGN KEY (DepartmentID) REFERENCES Department(ID)

Alter TABLE TimeSheet
ADD FOREIGN KEY (EmployeeID) REFERENCES Employee(ID)

Alter TABLE TimeSheetApproval
ADD FOREIGN KEY (EmployeeID) REFERENCES Employee(ID)

Alter TABLE TimeSheetApproval
ADD FOREIGN KEY (ApprovingManagerID) REFERENCES Manager(ID)


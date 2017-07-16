create database Employees;

use Employees;

create table Employees
(
	EmpId int primary key auto_increment ,
    Emp_Name varchar(50) not null,
    Address varchar(100) not null,
    Contact varchar(10) not null,
    PAN varchar(50) not null,
    Start_Date date not null,
    End_Date date not null,
    Date_Of_Birth date not null,
    Salary int not null,
    Emp_Status bool not null,
    Last_Updated date not null
)







select * from employees;


DELIMITER $$

create procedure AddNewEmpDetails
(
	E_Emp_Id int ,
    E_Emp_Name varchar(50) ,
    E_Address varchar(100) ,
    E_Contact varchar(10) ,
    E_PAN varchar(50) ,
    E_Start_Date date ,
    E_End_Date date ,
    E_Date_Of_Birth date ,
    E_Salary int ,
    E_Emp_Status bool ,
    E_Last_Updated date  
)
begin
insert into Employees values (E_Emp_Id,E_Emp_Name,E_Address,E_Contact,E_PAN,E_Start_Date,E_End_Date,E_Date_Of_Birth,E_Salary,E_Emp_Status,now());
End$$

DELIMITER $$

Create Procedure GetEmployees()
begin  
   select * from Employees where Emp_Status=1;
End$$


DELIMITER $$

Create procedure UpdateEmpDetails  
(  
	E_Emp_ID int,
    E_Emp_Name varchar(50) ,
    E_Address varchar(100) ,
    E_Contact varchar(10) ,
    E_PAN varchar(50) ,
    E_Start_Date date ,
    E_End_Date date ,
    E_Date_Of_Birth date ,
    E_Salary int ,
    E_Emp_Status bool
)  
begin  
   Update Employees   
   set                                  
    Emp_Name = E_Emp_Name ,  
    Address = E_Address ,
    Contact = E_Contact  ,  
    PAN = E_PAN ,
    Start_Date = E_Start_Date ,  
    End_Date =   E_End_Date ,
    DatOf_Birth = E_Date_Of_Birth , 
    Salary = E_Salary ,
    Emp_Status = E_Emp_Status ,
    Last_Updated = NOW() 
    WHERE Emp_ID = E_Emp_ID  ; 
End$$

select * from Employees;

DELIMITER $$

Create procedure DeleteEmpById  
(  
   E_Emp_ID int  
)  
begin  
   Update Employees set Emp_Status=0 where EmpId=E_Emp_ID;  
End$$

SELECT  EmpId, Emp_Name, Salary , Start_date , End_date ,
    CASE
        WHEN (Employees.End_Date is null) 
    THEN DATEDIFF(DATE_ADD(Start_Date, INTERVAL 30 DAY), Start_Date) * Salary/30
    ELSE DATEDIFF(End_Date, Start_Date) * Salary/30  END AS Total_Salary

From Employees;


Truncate Employees;

select * from Employees;
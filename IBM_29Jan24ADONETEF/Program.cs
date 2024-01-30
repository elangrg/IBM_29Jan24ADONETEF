using IBM_29Jan24ADONETEF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM_29Jan24ADONETEF
{
    internal class Program
    {
        static void Main()
        {
         

            Models.IBM25Jan24DbEntities _db= new Models.IBM25Jan24DbEntities ();



            string choice = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Employee Details through ADO.NET EF \n\n\n1. List Employees\n\n2. Add New Employee\n\n3. Edit Employee\n\n4. Delete Employee\n\n5. Stored Proc  \n\n0. Exit \n\n\n\n");
                Console.Write("Enter Choice:");
                choice = Console.ReadLine();



                if (choice == "1")
                {

                    ListEmployees(_db);


                }
                if (choice == "2")
                {
                    AddNewEmployee(_db);


                }

                if (choice == "3")
                {
                   EditEmployee(_db);


                }
                if (choice == "4")
                {
                    DeleteEmployee(_db);

                }
                if (choice == "5")
                {
                    CallStoredProcEg(_db);

                }
                if (choice == "6")
                {
                    //DatasetDataTableDisconnectedArcDataAdap(_cn);

                }

            } while (choice != "0");



        }

        private static void CallStoredProcEg(IBM25Jan24DbEntities _db)
        {
            Console.WriteLine("\n\nStored Proc ");
            Console.Write("Emp ID to Seek:");
            string empID = Console.ReadLine();


            ObjectParameter prm = new ObjectParameter("EmpName", "");
            _db.sp_GetEmpNameByID( int.Parse( empID),prm );
            Console.WriteLine($"EmpName is {prm.Value} for Emp Id {empID}");
            Console.Write("Press any key to Continue...");
            Console.ReadKey();
            Console.WriteLine($"EmpID          |EmpName          |Salary");
            foreach (var employee in _db.GetAllEmployees())
            { Console.WriteLine($"{employee.EmployeeID}|{employee.EmpName}|{employee.Salary}"); }
            Console.Write("Press any key to Continue...");
            Console.ReadKey();


        }

        private static void DeleteEmployee(IBM25Jan24DbEntities _db)
        {
            ListEmployees(_db);


            Console.WriteLine("\n\nEnter Employee Details to Seek for Edit");
            Console.Write("Emp ID to Seek:");


            Employee emp = _db.Employees.Find(Console.ReadLine());


           _db.Employees.Remove(emp);
           _db.SaveChanges();


            Console.WriteLine("Deleted Successfully!!!\n Press any key to Continue...");


            Console.ReadKey();
        }

        private static void EditEmployee(IBM25Jan24DbEntities _db)
        {
            // Display Employees 
            ListEmployees(_db);


            Console.WriteLine("\n\nEnter Employee Details to Seek for Edit");
            Console.Write("Emp ID to Seek:");
         

            Employee emp=    _db.Employees.Find(Console.ReadLine());


            Console.WriteLine("Enter Employee Details to Update");
            Console.Write("New Emp Name  :");
            emp.EmpName = Console.ReadLine();
            Console.Write("New Emp Salary:");
            emp.Salary = decimal.Parse(Console.ReadLine());

            _db.SaveChanges();


                Console.WriteLine("Updated Successfully!!!\n Press any key to Continue...");
           
           
            Console.ReadKey();

        }

        private static void AddNewEmployee(IBM25Jan24DbEntities _db)
        {
            Employee employee = new Employee();
            Console.Clear();
            Console.WriteLine("Enter Employee Details to Insert");
            Console.Write("Emp Name  :");
           employee.EmpName= Console.ReadLine();
            Console.Write("Emp Salary:");
            employee.Salary = decimal.Parse(Console.ReadLine());

            _db.Employees.Add(employee);
            _db.SaveChanges();

        }

        private static void ListEmployees(IBM25Jan24DbEntities _db)
        {
            Console.Clear();



            var qry = from emp in _db.Employees
                      where emp.EmpName.Contains("a")
                      select emp;
            var expr = _db.Employees.Where(e => e.EmpName.Contains("a"));


            Console.WriteLine($"EmpID          |EmpName          |Salary");
            foreach (var employee in qry)
            {Console.WriteLine($"{employee.EmployeeID}|{employee.EmpName}|{employee.Salary}"); }
            Console.Write("Press any key to Continue...");
            Console.ReadKey();

        }
    }
}

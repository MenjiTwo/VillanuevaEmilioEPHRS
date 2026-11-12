using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
    public string JobTitle { get; set; }
    public decimal MonthlySalary { get; set; }
    public double TaxRate { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
}

class Program
{
    static List<Employee> employees = new List<Employee>();

    static int nextId = 1001;

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== EPHRS MAIN ===");
            Console.WriteLine("1. Create (Add Employee)");
            Console.WriteLine("2. Read (View All Employees)");
            Console.WriteLine("3. Search / Filter Employees");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateEmployee();
                    break;
                case "2":
                    ReadEmployees();
                    break;
                case "3":
                    SearchEmployees();
                    break;
                case "4":
                    UpdateEmployee();
                    break;
                case "5":
                    DeleteEmployee();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    static void CreateEmployee()
    {
        Console.WriteLine("--- Add New Employee ---");

        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter Department: ");
        string department = Console.ReadLine();

        Console.Write("Enter Job Title: ");
        string jobTitle = Console.ReadLine();

        Console.Write("Enter Monthly Salary: ");
        decimal.TryParse(Console.ReadLine(), out decimal salary);

        Console.Write("Enter Tax Rate (e.g. 0.12): ");
        double.TryParse(Console.ReadLine(), out double taxRate);

        Console.Write("Enter Hire Date (yyyy-mm-dd): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime hireDate);

        Console.Write("Is the employee active? (y/n): ");
        string active = Console.ReadLine();

        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        Employee emp = new Employee();
        emp.Id = nextId;
        emp.FirstName = firstName;
        emp.LastName = lastName;
        emp.Department = department;
        emp.JobTitle = jobTitle;
        emp.MonthlySalary = salary;
        emp.TaxRate = taxRate;
        emp.HireDate = hireDate;
        emp.IsActive = active.ToLower() == "y";
        emp.Email = email;

        employees.Add(emp);
        nextId++;

        Console.WriteLine("\nEmployee added successfully!");
    }

    static void ShowEmployee(Employee e)
    {
        decimal netSalary = e.MonthlySalary - (e.MonthlySalary * (decimal)e.TaxRate);

        Console.WriteLine("ID: " + e.Id);
        Console.WriteLine("Name: " + e.FirstName + " " + e.LastName);
        Console.WriteLine("Department: " + e.Department);
        Console.WriteLine("Job Title: " + e.JobTitle);
        Console.WriteLine("Monthly Salary: " + e.MonthlySalary);
        Console.WriteLine("Tax Rate: " + e.TaxRate);
        Console.WriteLine("Net Salary: " + netSalary);
        Console.WriteLine("Hire Date: " + e.HireDate.ToShortDateString());
        Console.WriteLine("Active: " + e.IsActive);
        Console.WriteLine("Email: " + e.Email);
        Console.WriteLine("-----------------------------");
    }

    static void ReadEmployees()
    {
        Console.WriteLine("--- Employee Records ---");

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        foreach (Employee e in employees)
        {
            ShowEmployee(e);
        }
    }

    static void SearchEmployees()
    {
        Console.WriteLine("--- Search Employees ---");
        Console.Write("Enter name, department, or job title to search: ");
        string keyword = Console.ReadLine().ToLower();

        var results = employees.Where(e =>
            e.FirstName.ToLower().Contains(keyword) ||
            e.LastName.ToLower().Contains(keyword) ||
            e.Department.ToLower().Contains(keyword) ||
            e.JobTitle.ToLower().Contains(keyword)).ToList();

        Console.WriteLine();

        if (results.Count == 0)
        {
            Console.WriteLine("No matching employees found.");
            return;
        }

        foreach (Employee e in results)
        {
            ShowEmployee(e);
        }
    }

    static void UpdateEmployee()
    {
        ReadEmployees();

        Console.Write("\nEnter the ID of the employee to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee emp = employees.FirstOrDefault(e => e.Id == id);

            if (emp != null)
            {
                Console.Write($"Enter new first name (current: {emp.FirstName}): ");
                string newFirstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newFirstName)) emp.FirstName = newFirstName;

                Console.Write($"Enter new last name (current: {emp.LastName}): ");
                string newLastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newLastName)) emp.LastName = newLastName;

                Console.Write($"Enter new department (current: {emp.Department}): ");
                string newDepartment = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDepartment)) emp.Department = newDepartment;

                Console.Write($"Enter new job title (current: {emp.JobTitle}): ");
                string newJobTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newJobTitle)) emp.JobTitle = newJobTitle;

                Console.Write($"Enter new monthly salary (current: {emp.MonthlySalary}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newSalary))
                {
                    emp.MonthlySalary = newSalary;
                }

                Console.Write($"Enter new tax rate (current: {emp.TaxRate}): ");
                if (double.TryParse(Console.ReadLine(), out double newTaxRate))
                {
                    emp.TaxRate = newTaxRate;
                }

                Console.Write($"Enter new hire date (current: {emp.HireDate.ToShortDateString()}): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newHireDate))
                {
                    emp.HireDate = newHireDate;
                }

                Console.Write($"Is the employee active? y/n (current: {emp.IsActive}): ");
                string newActive = Console.ReadLine();
                if (newActive.ToLower() == "y") emp.IsActive = true;
                else if (newActive.ToLower() == "n") emp.IsActive = false;

                Console.Write($"Enter new email (current: {emp.Email}): ");
                string newEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEmail)) emp.Email = newEmail;

                Console.WriteLine("\nEmployee updated successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }

    static void DeleteEmployee()
    {
        ReadEmployees();

        Console.Write("\nEnter the ID of the employee to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            int removed = employees.RemoveAll(e => e.Id == id);

            if (removed > 0)
            {
                Console.WriteLine("\nEmployee deleted successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }
}
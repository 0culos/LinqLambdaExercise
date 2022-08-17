using System.Globalization;
using LambdaEmployee.Entities;

Console.Write("Enter full file path: ");
string path = Console.ReadLine();

List<Employee> list = new List<Employee>();
try
{
    using (StreamReader sr = File.OpenText(path))
    {
        while (!sr.EndOfStream)
        {
            string[] data = sr.ReadLine().Split(',');
            string name = data[0];
            string email = data[1];
            double salary = double.Parse(data[2], CultureInfo.InvariantCulture);
            list.Add(new Employee(name, email, salary));
        }
    }

    Console.Write("Enter salary: ");
    double baseSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

    Console.WriteLine("Email of people whose salary is more than " + baseSalary.ToString("F2", CultureInfo.InvariantCulture) + ": ");

    var emails = list.Where(p => p.Salary > baseSalary).OrderBy(p => p.Name).Select(p => p.Email);
    foreach (string email in emails)
    {
        Console.WriteLine(email);
    }

    Console.Write("Sum of salary of people whose name starts with 'M': ");

    var names = list.Where(p => p.Name[0] == 'M').Sum(p => p.Salary);
    Console.WriteLine(names.ToString("F2", CultureInfo.InvariantCulture));
}
catch (IOException e)
{
    Console.WriteLine("An error has occured:");
    Console.WriteLine(e.Message);
}
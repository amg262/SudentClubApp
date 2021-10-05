using System;
using System.Collections.Generic;
using NLog;
using NLog.Web;
using StudentClubApp;

namespace SudentClubApp
{
    internal class Program
    {
        private string fileName = "students.csv";
        private readonly Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();


        public Program()
        {
            Students = new List<Student>();
        }

        private List<Student> Students { get; set; }

        public void AddStudentOption()
        {
            try
            {
                //StudentFile csv = new StudentFile("students.csv");

                Console.Write("First Name: ");
                var fName = Console.ReadLine();

                Console.Write("Last Name: ");
                var lName = Console.ReadLine();

                Console.Write("Email: ");
                var email = Console.ReadLine();

                var newStudent = new Student(fName, lName, email);
                Students.Add(newStudent);

                RefreshPrompt();
            }
            catch (Exception e)
            {
                logger.Warn(e);
                Console.WriteLine(e);
                throw;
            }
        }


        public void RefreshPrompt()
        {
            Console.WriteLine("1). Add   2). Delete  3). Edit  4). List  5). Exit");
        }

        public void DeleteStudentOption()
        {
            Console.Write("ID or Index of Student to Remove: ");
            int id, idRemove;
            int.TryParse(Console.ReadLine(), out idRemove);
            id = idRemove;

            //Students.RemoveAt(--idRemove);
            Students.RemoveAt(idRemove);


            Console.WriteLine("---------------");
            foreach (var s in Students) Console.WriteLine(s);

            Console.WriteLine($"--- ID #{id} Successfully Removed ---");
            RefreshPrompt();
        }


        public void EditOptionStudent()
        {
            Console.WriteLine("ID or Index of Student to Edit: ");
            var idEdit = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("New First Name: ");
            Students[idEdit].FirstName = Console.ReadLine();
            Console.WriteLine("New Last Name: ");
            Students[idEdit].LastName = Console.ReadLine();

            Console.WriteLine("New Email: ");
            Students[idEdit].Email = Console.ReadLine();

            Console.WriteLine($"--- Index #{Students[idEdit].Id} Successfully Editted ---");
            RefreshPrompt();
        }

        public void ListStudentsOption()
        {
            try
            {
                if (Students.Count > 0)
                {
                    foreach (var s in Students) Console.WriteLine(s);

                    Console.WriteLine($"------{Students.Count} Students in List-------");
                    RefreshPrompt();
                }
            }
            catch (Exception e)
            {
                logger.Warn(e);
                Console.WriteLine(e);
                throw;
            }
        }


        private static void Main(string[] args)
        {
            var p = new Program();
            //Students = new List<Student>();
            p.Students.Add(new Student("Andrew", "Gunn", "agunn1@my.wctc.edu"));
            p.Students.Add(new Student("Aaron", "Rodgers", "arod@packers.com"));
            p.Students.Add(new Student("Aaron A-Jizzle", "Jones", "ajizzle@gmail.com"));
            p.Students.Add(new Student("Gilbert", "Brown", "gilbertogmail.com"));
            p.Students.Add(new Student("Jordon", "Love", "noloveforjordanlove@lulz.com"));

            //int choice = p.HomeList();

            var option = 0;
            Console.WriteLine($"{p.Students.Count} records pre-added to club");

            Console.WriteLine("--------------------------");
            Console.WriteLine("Student Club Management");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Delete Student");
            Console.WriteLine("3. Edit Student");
            Console.WriteLine("4. List Students");
            Console.WriteLine("5. Exit");
            do
            {
                int.TryParse(Console.ReadLine(), out option);

                //return option;
                switch (option)
                {
                    case 1:
                        p.AddStudentOption();
                        p.ListStudentsOption();
                        break;
                    case 2:
                        p.ListStudentsOption();
                        p.DeleteStudentOption();

                        break;
                    case 3:
                        p.ListStudentsOption();
                        p.EditOptionStudent();
                        p.ListStudentsOption();
                        break;
                    case 4:
                        p.ListStudentsOption();
                        break;
                    case 5:
                        break;
                }
            } while (option != 5 && option != 0);


            Console.WriteLine("Enter Name of CSV file to generate: ");
            var filePath = Console.ReadLine() + ".csv";
            var csv = new StudentFile(filePath);
            csv.WriteToFile(p.Students);

            Console.WriteLine("Writing....");
            Console.WriteLine("Writing...");
            Console.WriteLine("Writing..");
            Console.WriteLine(
                $"Writing Complete! {filePath} is definitely saved somewhere in this directory..depending on what version of .NET is running. Check /bin/debug/net5.0/{filePath}");
        }
    }
}
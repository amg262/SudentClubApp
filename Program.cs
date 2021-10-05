using System;
using System.IO;
using System.Collections.Generic;
using NLog;
using NLog.Web;
using StudentClubApp;

namespace SudentClubApp
{
    class Program
    {
        private string fileName = "students.csv";
        private List<Student> Students { get; set; }
        private NLog.Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();


        public Program()
        {
            Students = new List<Student>();
        }

        public void StudentsList()
        {
            int id = 0;

            try
            {
                if (this.Students.Count > 0)
                {
                    foreach (Student s in this.Students)
                    {
                        id++;
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.logger.Error(e.Message);
                //throw;
            }
        }

        public void DeleteStudent(string id)
        {
            Student s = Students.Find(student => student.FirstName == id);

            Console.WriteLine(Students.Remove(s));

            //Console.WriteLine(s);

            foreach (var ss in Students)
            {
                Console.WriteLine(ss);
            }
        }


        public void AddStudentOption()
        {
            try
            {
                //StudentFile csv = new StudentFile("students.csv");

                Console.Write("First Name: ");
                string fName = Console.ReadLine();

                Console.Write("Last Name: ");
                string lName = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Student newStudent = new Student(fName, lName, email);
                Students.Add(newStudent);

                //csv.WriteToFile(Students, true);
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
            Console.WriteLine($"1). Add   2). Delete  3). Edit  4). List  5). Exit");
        }

        public void DeleteStudentOption()
        {
            Console.Write("ID or Index of Student to Remove: ");
            int id, idRemove;
            Int32.TryParse(Console.ReadLine(), out idRemove);
            id = idRemove;

            //Students.RemoveAt(--idRemove);
            Students.RemoveAt(idRemove);


            Console.WriteLine("---------------");
            foreach (var s in Students)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine($"--- ID #{id} Successfully Removed ---");
            RefreshPrompt();
        }


        public void EditStudent()
        {
            Console.WriteLine("ID or Index of Student to Edit: ");
            int idEdit = Convert.ToInt32(Console.ReadLine());

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
                    foreach (var s in Students)
                    {
                        Console.WriteLine(s);
                    }

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

        public int HomeList()
        {
            Students = new List<Student>();
            Students.Add(new Student("Andy", "Gunn", "andrewgun31@gmail.com"));
            Students.Add(new Student("Boby", "Gunn", "bobby@gmail.com"));
            Students.Add(new Student("mom", "mom", "@gmail.com"));
            Students.Add(new Student("dad", "dad", "gmail.com"));

            Console.WriteLine($"Student Club Management");
            Console.WriteLine($"1. Add Student");
            Console.WriteLine($"2. Delete Student");
            Console.WriteLine($"3. Edit Student");
            Console.WriteLine($"4. List Students");
            Console.WriteLine($"5. Read From File");
            Console.WriteLine($"6. Write List to File");
            Console.WriteLine($"7. Exit");

            Int32.TryParse(Console.ReadLine(), out int option);

            return option;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            //Students = new List<Student>();
            p.Students.Add(new Student("Andy", "Gunn", "andrewgun31@gmail.com"));
            p.Students.Add(new Student("Boby", "Gunn", "bobby@gmail.com"));
            p.Students.Add(new Student("mom", "mom", "@gmail.com"));
            p.Students.Add(new Student("dad", "dad", "gmail.com"));

            //int choice = p.HomeList();

            int option = 0;
            Console.WriteLine("--------------------------");
            Console.WriteLine($"Student Club Management");
            Console.WriteLine($"1. Add Student");
            Console.WriteLine($"2. Delete Student");
            Console.WriteLine($"3. Edit Student");
            Console.WriteLine($"4. List Students");
            Console.WriteLine($"5. Exit");
            do
            {
                Int32.TryParse(Console.ReadLine(), out option);

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
                        p.EditStudent();
                        p.ListStudentsOption();
                        break;
                    case 4:
                        p.ListStudentsOption();
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            } while (option != 5 && option != 0);


            Console.WriteLine("Enter Name of CSV file to generate: ");
            string filePath = Console.ReadLine() + ".csv";
            StudentFile csv = new StudentFile(filePath);
            csv.WriteToFile(p.Students);

            Console.WriteLine($"Writing....");
            Console.WriteLine($"Writing...");
            Console.WriteLine($"Writing..");
            Console.WriteLine(
                $"Writing Complete! {filePath} is definitely saved somewhere in this directory..depending on what version of .NET is running. Check /bin/debug/net5.0/{filePath}");
        }
    }
}
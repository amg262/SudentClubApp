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
            }
            catch (Exception e)
            {
                logger.Warn(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteStudentOption()
        {
            Console.Write("ID of Student to Remove: ");
            Int32.TryParse(Console.ReadLine(), out int idRemove);


            Students.RemoveAt(--idRemove);

            foreach (var s in Students)
            {
                Console.WriteLine(s);
            }
        }


        public void EditStudent()
        {
            int idEdit2 = 2;
            Console.WriteLine("ID of Student to Edit: ");
            int idEdit = Convert.ToInt32(Console.ReadLine());
            // int id, idEdit;
            // Int32.TryParse(Console.ReadLine(), out id);
            // Int32.TryParse(Console.ReadLine(), out idEdit);
            // --idEdit;
            idEdit--;

            //Students[idEdit].Id = id;

            Console.WriteLine("New First Name: ");
            Students[idEdit].FirstName = Console.ReadLine();
            Console.WriteLine("New Last Name: ");
            Students[idEdit].LastName = Console.ReadLine();

            Console.WriteLine("New Email: ");
            Students[idEdit].Email = Console.ReadLine();
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


            int choice = p.HomeList();

            switch (choice)
            {
                case 1:
                    p.AddStudentOption();
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
                case 6:
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
    }
}
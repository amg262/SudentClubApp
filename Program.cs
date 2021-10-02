using System;
using System.IO;
using System.Collections.Generic;
using NLog;
using NLog.Web;

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


        public void DefaultPopulate()
        {
            Students = new List<Student>();
            Students.Add(new Student("Andy", "Gunn", "andrewgun31@gmail.com"));
            Students.Add(new Student("Boby", "Gunn", "bobby@gmail.com"));
            Students.Add(new Student("Bobdsdy", "Gfsdfunn", "@gmail.com"));
            Students.Add(new Student("gsadgs", "Gundsn", "gmail.com"));

            try
            {
                StreamWriter writer = new StreamWriter(this.fileName, true);

                foreach (Student s in Students)
                {
                    Student.QueueId++;
                    writer.WriteLine($"{Student.QueueId}, {s.FirstName}, {s.LastName}, {s.Email}");
                }

                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.logger.Error(e);
                throw;
            }
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

        public void StudentsList2()
        {
            var id = 0;


            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    DefaultPopulate();
                }

                StreamReader reader = new StreamReader(fileName);

                while (!reader.EndOfStream)
                {
                    string[] lineData = reader.ReadLine().Split(",");
                    Students.Add(new Student(Convert.ToInt32(lineData[0]), lineData[1], lineData[2], lineData[3]));
                }

                reader.Close();

                foreach (Student s in Students)
                {
                    Console.WriteLine(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger.Error(e);
                throw;
            }
        }

        public void AddStudentOption()
        {
            try
            {
                Console.Write("First Name: ");
                string fName = Console.ReadLine();

                Console.Write("Last Name: ");
                string lName = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Student newStudent = new Student(fName, lName, email);
                Students.Add(newStudent);
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
            Console.Write("ID of Student to remove: ");
            Int32.TryParse(Console.ReadLine(), out int idRemove);

            
            Students.RemoveAt(idRemove);

            foreach (var s in Students)
            {
                Console.WriteLine(s);
            }
            // try
            // {
            //     foreach (Student s in Students)
            //     {
            //         if (s.Id == idRemove)
            //         {
            //             //Students.Remove(s);
            //             Students.RemoveAt(--idRemove);
            //         }
            //     }
            // }
            // catch (Exception e)
            // {
            //     logger.Error(e);
            //     Console.WriteLine(e);
            //     throw;
            // }
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
            
            
            //p.logger.Error("error");

            // p.DefaultPopulate();
            // p.StudentsList2();
            //
            // p.DeleteStudent("Andy");

            int choice = p.HomeList();
            
            switch (choice)
            {
                case 1:
                    p.AddStudentOption();
                    break;
                case 2:
                    p.DeleteStudentOption();
                    p.ListStudentsOption();
                    break;
                case 3:
                    
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
using System;
using System.Collections.Generic;
using NLog;
using NLog.Web;

namespace SudentClubApp
{
    class Program
    {
        private List<Student> students;
        private NLog.Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();


        public Program()
        {
            this.students = new List<Student>();
        }

        public Program(List<Student> students = null)
        {
            this.students = students;
        }

        public List<Student> Students
        {
            get => students;
            set => students = value;
        }

        public void DefaultPopulate()
        {
            this.students = new List<Student>();
            this.students.Add(new Student("Andy","Gunn","andrewgun31@gmail.com"));
            this.students.Add(new Student("Boby","Gunn","bobby@gmail.com"));
            this.students.Add(new Student("Bobdsdy","Gfsdfunn","@gmail.com"));
            this.students.Add(new Student("gsadgs","Gundsn","gmail.com"));
            
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
                        Console.WriteLine($"{id}. | " +
                                          $"{s.Id} -- " +
                                          $"{s.FirstName} " +
                                          $"{s.LastName} | " +
                                          $"{s.Email}");
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

        public void HomeList()
        {
            Console.WriteLine($"Student Club Management");
            Console.WriteLine($"1. Add Student");
            Console.WriteLine($"2. Delete Student");
            Console.WriteLine($"3. Edit Student");
            Console.WriteLine($"4. List Students");
            Console.WriteLine($"5. Exit");
        }

        static void Main(string[] args)
        {
            
            Program p = new Program();
            //p.logger.Error("error");

            p.DefaultPopulate();
            p.StudentsList();
        }
    }
}
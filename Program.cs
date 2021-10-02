﻿using System;
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


        public void GetInput()
        {
            try
            {
                Console.Write("Enter Student First Name:");
                string fName = Console.ReadLine();

                Console.Write("Enter Student Last Name: ");
                string lName = Console.ReadLine();

                Console.Write("Enter Student Email: ");
                string email = Console.ReadLine();

                int id = Student.QueueId;

                Student student = new Student(id, fName, lName, email);

                Students.Add(student);
            }
            catch (Exception e)
            {
                logger.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public void ReadFromFile()
        {
            var id = 0;

            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    this.WriteToFile();
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


        public void WriteToFile()
        {
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


        public void FindStudent(string keyword)
        {
            try
            {
                Student s = Students.Find(student => student.FirstName.Equals(keyword));

                Console.WriteLine(s);
            }
            catch (Exception e)
            {
                logger.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteFoundStudent(string keyword)
        {
            try
            {
                List<Student> results;
                Student student = null;
                results = new List<Student>();
                results = Students.FindAll(student => student.FirstName.Equals(keyword));

                foreach (Student v in results)
                {
                    Students.Remove(v);
                }
                
            }
            catch (Exception e)
            {
                logger.Warn(e);
                Console.WriteLine(e);
                throw;
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
            //p.GetInput();
            //p.StudentsList();
            //p.WriteToFile();
            p.ReadFromFile();

            //Console.WriteLine("Find: ");
            //string key = Console.ReadLine();
            //p.FindStudent(key);
            p.DeleteFoundStudent("andy");

            p.StudentsList();
            //p.WriteToFile();
        }
    }
}
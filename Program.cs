using System;
using System.Collections.Generic;

namespace SudentClubApp
{
    class Program
    {

        private List<Student> students;


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

        public void StudentsList()
        {
            int id = 0;

            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            
            
            
        }
    }
}
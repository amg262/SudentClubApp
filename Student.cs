using System;

namespace SudentClubApp
{
    public class Student
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private static int queueId;

        public Student(string firstName = null, string lastName = null, string email = null)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }

        public Student(int id = default, string firstName = null, string lastName = null, string email = null)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public static int QueueId
        {
            get => queueId;
            set => queueId = value;
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Print()
        {
            return $"{FirstName} {LastName} | {Email}";
        }
    }
}
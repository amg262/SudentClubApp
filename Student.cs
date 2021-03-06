namespace SudentClubApp
{
    public class Student
    {
        public Student()
        {
        }

        public Student(string firstName = null, string lastName = null, string email = null)
        {
            //QueueId++;
            Id = QueueId++;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public Student(int id = default, string firstName = null, string lastName = null, string email = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }


        public int Id { get; set; }

        public static int QueueId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }


        public override string ToString()
        {
            return $"{Id},{FirstName}, {LastName}, {Email}";
        }

        public string Print()
        {
            return $"{Id},{FirstName}, {LastName}, {Email}";
        }
    }
}
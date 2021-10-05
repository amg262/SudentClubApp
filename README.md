# Sudent Club App #

This application allows you to manage the members of a student club. By selecting different menu options, you can add, delete, edit, and list the students in the club.

The program returns to the main menu after each operation until the user selects to Exit the application.



### I utilize NLog packages inside of Try - Catch blocks and write those thrown exceptions into a log file using:
```config
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd ">
    <targets>
        <target name="error_log" xsi:type="File" fileName="error.log"/>
        <target name="error_console" xsi:type="Console"/>
    </targets>

    <rules>
        <logger name="*" minlevel="Trace" writeTo="error_console"/>
        <logger name="*" minlevel="Fatal" writeTo="error_log" />
        <logger name="*" minlevel="Warn" writeTo="error_log" />
        <logger name="*" minlevel="Info" writeTo="error_log"/>
    </rules>
</nlog>

```


Some of the error messages found in that log are here:
```
2021-09-29 15:48:10.9066|ERROR|SudentClubApp.Program|System.IO.FileNotFoundException: Could not find file 'C:\Users\Andy\RiderProjects\SudentClubApp\SudentClubApp\bin\Debug\net5.0\students.csv'.
File name: 'C:\Users\Andy\RiderProjects\SudentClubApp\SudentClubApp\bin\Debug\net5.0\students.csv'
   at System.IO.FileStream.ValidateFileHandle(SafeFileHandle fileHandle)
   at System.IO.FileStream.CreateFileOpenHandle(FileMode mode, FileShare share, FileOptions options)
   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options)
   at System.IO.StreamReader.ValidateArgsAndOpenPath(String path, Encoding encoding, Int32 bufferSize)
   at System.IO.StreamReader..ctor(String path)
   at SudentClubApp.Program.StudentsList2() in C:\Users\Andy\RiderProjects\SudentClubApp\SudentClubApp\Program.cs:line 81
2021-10-02 18:04:32.3341|ERROR|SudentClubApp.Program|System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
   at System.Collections.Generic.List`1.Enumerator.MoveNextRare()
   at System.Collections.Generic.List`1.Enumerator.MoveNext()
   at SudentClubApp.Program.DeleteStudentOption() in C:\Users\Andy\RiderProjects\SudentClubApp\SudentClubApp\Program.cs:line 153```


```
5 records pre-added to club
--------------------------
Student Club Management
1. Add Student
2. Delete Student
3. Edit Student
4. List Students
5. Exit
1
First Name: Brett
Last Name: Favre
Email: bfarve@yahoo.net
1). Add   2). Delete  3). Edit  4). List  5). Exit
0,Andrew, Gunn, agunn1@my.wctc.edu
1,Aaron, Rodgers, arod@packers.com
2,Aaron A-Jizzle, Jones, ajizzle@gmail.com
3,Gilbert, Brown, gilbertogmail.com
4,Jordon, Love, noloveforjordanlove@lulz.com
5,Brett, Favre, bfarve@yahoo.net
------6 Students in List-------
1). Add   2). Delete  3). Edit  4). List  5). Exit
3
0,Andrew, Gunn, agunn1@my.wctc.edu
1,Aaron, Rodgers, arod@packers.com
2,Aaron A-Jizzle, Jones, ajizzle@gmail.com
3,Gilbert, Brown, gilbertogmail.com
4,Jordon, Love, noloveforjordanlove@lulz.com
5,Brett, Favre, bfarve@yahoo.net
------6 Students in List-------
1). Add   2). Delete  3). Edit  4). List  5). Exit
ID or Index of Student to Edit:
5
New First Name:
Brett
New Last Name:
Im retiring again Favre
New Email:
retired@favre.io
--- Index #5 Successfully Editted ---
1). Add   2). Delete  3). Edit  4). List  5). Exit
0,Andrew, Gunn, agunn1@my.wctc.edu
1,Aaron, Rodgers, arod@packers.com
2,Aaron A-Jizzle, Jones, ajizzle@gmail.com
3,Gilbert, Brown, gilbertogmail.com
4,Jordon, Love, noloveforjordanlove@lulz.com
5,Brett, Im retiring again Favre, retired@favre.io
------6 Students in List-------
1). Add   2). Delete  3). Edit  4). List  5). Exit
2
0,Andrew, Gunn, agunn1@my.wctc.edu
1,Aaron, Rodgers, arod@packers.com
2,Aaron A-Jizzle, Jones, ajizzle@gmail.com
3,Gilbert, Brown, gilbertogmail.com
4,Jordon, Love, noloveforjordanlove@lulz.com
5,Brett, Im retiring again Favre, retired@favre.io
------6 Students in List-------
1). Add   2). Delete  3). Edit  4). List  5). Exit
ID or Index of Student to Remove: 4
---------------
0,Andrew, Gunn, agunn1@my.wctc.edu
1,Aaron, Rodgers, arod@packers.com
2,Aaron A-Jizzle, Jones, ajizzle@gmail.com
3,Gilbert, Brown, gilbertogmail.com
5,Brett, Im retiring again Favre, retired@favre.io
--- ID #4 Successfully Removed ---
1). Add   2). Delete  3). Edit  4). List  5). Exit
5
Enter Name of CSV file to generate:
packers
Writing....
Writing...
Writing..
Writing Complete! packers.csv is definitely saved somewhere in this directory..depending on what version of .NET is running. Check /bin/debug/net5.0/packers.csv
```

#### Packers.csv file has this data written to it:
```csv
0,Andrew, Gunn, agunn1@my.wctc.edu
1,Aaron, Rodgers, arod@packers.com
2,Aaron A-Jizzle, Jones, ajizzle@gmail.com
3,Gilbert, Brown, gilbertogmail.com
5,Brett, Im retiring again Favre, retired@favre.io
```

### This class handles the writing of the file with a list of Student objects
```c#
public class StudentFile : IDisposable
{
    private static Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

    public StudentFile(string filePath, List<Student> studentList = null)
    {
        FilePath = filePath;
        StudentList = studentList;
    }

    public string FilePath { get; set; }
    public List<Student> StudentList { get; set; }
    public StreamReader Reader { get; set; }
    public StreamWriter Writer { get; set; }
    public bool IsCreated { get; set; }

    public void Dispose()
    {
        Reader?.Dispose();
        Writer?.Dispose();
    }

    public void WriteToFile(List<Student> students, bool append = false)
    {
        Writer = new StreamWriter(FilePath, append);

        foreach (var s in students) Writer.WriteLine(s.ToString());

        Writer.Close();
    }
    public void ReadFromFile()
    {
        Reader = new StreamReader(FilePath);

        while (!Reader.EndOfStream) Console.WriteLine(Reader.ReadLine());

        Reader.Close();
    }
}
```

### Student object is outlined with this class
```c#
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
```


### And the main program is executed here:
```c#
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
```
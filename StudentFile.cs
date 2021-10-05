using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using NLog.Web;
using SudentClubApp;

namespace StudentClubApp
{
    public class StudentFile : IDisposable
    {
        private static Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public List<string> Preset = new()
        {
            "TicketID,Summary,Status,Priority,Submitter,Assigned,Watching",
            "1,This is a bug student,Open,High,Drew Kjell,Jane Doe,Drew Kjell|John Smith|Bill Jones"
        };

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

        public void WriteToFile(List<Student> students, bool append = true)
        {
            
            Writer = new StreamWriter(FilePath, append);

            
            if (!File.Exists(FilePath))
            {
                //Writer.WriteLine(Preset[0]);
                //Writer.WriteLine(Preset[1]);
            }

            foreach (var s in students)
            {
                Writer.WriteLine(s.ToString());

            }

            Writer.Close();
        }
        
        
        public void ReadFromFile()
        {
            Reader = new StreamReader(FilePath);

            while (!Reader.EndOfStream) Console.WriteLine(Reader.ReadLine());

            Reader.Close();
        }
    }
}
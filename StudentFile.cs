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
}
using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using NLog.Web;

namespace SudentClubApp
{
    public class File
    {
        private string filePath;
        private Logger logger;
        private StreamReader reader;
        private List<Student> students;
        private StreamWriter writer;

        public File(List<Student> students = null, Logger logger = null, StreamReader reader = null,
            StreamWriter writer = null)
        {
            this.students = students;
            this.logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            ;
            this.reader = reader;
            this.writer = writer;
        }

        public File()
        {
            this.filePath = "student.csv";
            this.logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        public File(string filePath = null)
        {
            this.filePath = filePath;
            this.logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            // this.Reader = new StreamReader(this.filePath);
        }

        public List<Student> Students
        {
            get => students;
            set => students = value;
        }

        public Logger Logger
        {
            get => logger;
            set => logger = value;
        }

        public StreamReader Reader
        {
            get => reader;
            set => reader = value;
        }

        public StreamWriter Writer
        {
            get => writer;
            set => writer = value;
        }

        public void HandleFile()
        {
            //students = new List<Student>();

            // try
            // {
            // this.Reader = new StreamReader(this.filePath);
            // this.Reader.ReadLine();
            //
            // while (!this.Reader.EndOfStream)
            // {
            //     Student student = new Student();
            //     string line = this.Reader.ReadLine();
            //     int index = line.IndexOf('"');
            //     if (index == -1)
            //     {
            //         string[] movieData = line.Split(',');
            //         student.MovieID = Int32.Parse(movieData[0]);
            //         movie.Title = movieData[1];
            //         movie.Genres = movieData[2].Split('|').ToList();
            //     }
            //     else
            //     {
            //         movie.MovieID = Int32.Parse(line.Substring(0, index - 1));
            //         line = line.Substring(index + 1);
            //         index = line.IndexOf('"');
            //         movie.Title = line.Substring(0, index);
            //         line = line.Substring(index + 2);
            //         movie.Genres = line.Split('|').ToList();
            //     }
            //
            //     movies.Add(movie);
            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e);
            //     this.logger.Error(e.Message);
            //     throw;
            // }
        }
    }
}
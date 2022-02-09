using ASPCoreLesson4AdditionalTask;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ASPCoreLesson4AdditionalProject
{
    public static class LibraryManager
    {
         public static IEnumerable<Book> GetBooks(IConfiguration appConfiguration)
        {
            IEnumerable<IConfigurationSection> booksSection = appConfiguration.GetSection("Books").GetChildren();
            foreach (IConfigurationSection item in booksSection)
                yield return new Book() { BookName = item.Value };
        }

        public static Student GetStudentByID(int id, IConfiguration appConfiguration)
        {
            IConfigurationSection studentSection = appConfiguration.GetSection("Students");

            if (studentSection[id.ToString()] != null)
                return new Student() { StudentID = id, Name = studentSection[id.ToString()] };
            return null;
        }

        public static IEnumerable<Student> GetAllStudents(IConfiguration appConfiguration)
        {
            IEnumerable<IConfigurationSection> studentsSection = appConfiguration.GetSection("Students")
                .GetChildren();

            foreach (IConfigurationSection item in studentsSection)
                yield return new Student() { StudentID = int.Parse(item.Key), Name = item.Value };
        }

    }
}

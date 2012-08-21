using System;
using EmailMaker.Infrastructure;

namespace EmailMaker.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose from the following options:");
            Console.WriteLine("1 Generate database schema sql file");
            var line = Console.ReadLine();
            if (line == "1")
            {
                new EmailMakerDatabaseSchemaGenerator(@"..\..\..\EmailMaker.Database\EmailMaker_generated_database_schema.sql").Generate();
                Console.WriteLine("Database schema sql file has been generated");
            }
        }
    }
}

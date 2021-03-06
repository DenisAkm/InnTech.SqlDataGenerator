﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace InnTech.SqlDataGenerator.Executor
{
    //Build as Single File: dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true
    class Program
    {
        static string ProgramName = "SqlDataGenerator";
        static string SqlFileName = "Create_{0}_Records_In_{1}_Entities_{2}.sql";
        static string OptionsFileName = "AppSettings.json";
        static string Description = "{0}-- The sql-script is generated by program {1} at {2} and creates following entities: \n\n{3}\n" +
                              "-- In total {4} records in {5} entites.\n{6}\n";
        static string Line = "--------------------------------------------------------------------------------------------------------------\n";

        static void Main(string[] args)
        {
            try
            {
                var appSettings = ReadAppSettings();

                var client = new MssqlConnectionUtils(appSettings.ConnectionString);
                var dataGenerator = new DataGeneratorCore(client, appSettings);
                var sqlStringBuilder = dataGenerator.BuildSqlScript(appSettings.CreateEntities);

                string description = BuildDescription(appSettings);
                sqlStringBuilder.Insert(0, description);

                SqlFileName = string.Format(SqlFileName, 
                    appSettings.CreateEntities.Sum(x => x.Value), 
                    appSettings.CreateEntities.Count, 
                    DateTime.UtcNow.ToString("yyyy-MM-dd"));

                string sqlFilePath = Path.Combine(Environment.CurrentDirectory, SqlFileName);
                using (StreamWriter sw = new StreamWriter(sqlFilePath, append: false))
                {
                    sw.Write(sqlStringBuilder);
                }

                Console.WriteLine(sqlStringBuilder);
            }
            catch (Exception e)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.StackTrace);

                Console.ForegroundColor = color;
            }

            Console.ReadLine();
        }

        private static string BuildDescription(GeneratorSettings appSettings)
        {
            var entities = appSettings.CreateEntities.Select(x => $"-- Name: {x.Key}, Count: {x.Value}\n");

            return string.Format(Description,
                    Line,
                    ProgramName,
                    DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("de-DE")),
                    string.Join("", entities),
                    appSettings.CreateEntities.Sum(x => x.Value),
                    appSettings.CreateEntities.Count,
                    Line);
        }

        private static GeneratorSettings ReadAppSettings()
        {
            string optionsFilePath = Path.Combine(Environment.CurrentDirectory, OptionsFileName);
            using var streamReader = new StreamReader(optionsFilePath);
            var jsonTextReader = streamReader.ReadToEnd();

            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<GeneratorSettings>(jsonTextReader, options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace SecondLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("Use one of commands:");
            Console.WriteLine("'check' to check is year leap");
            Console.WriteLine("'calc' to calc interval lenght");
            Console.WriteLine("'day' to get the name of day of week");
            Console.WriteLine("'quit' to exit");
            Console.WriteLine("'save' to save interval");
            Console.WriteLine("'load' to load interval");
            Console.WriteLine("----");

            string command;
            do
            {
                Console.WriteLine("Input the command:");
                command = Console.ReadLine();

                if (command == "check")
                {
                    Console.WriteLine("Input the year:");
                    int year = int.Parse(Console.ReadLine());

                    Year yea = new Year
                    {
                        year = year
                    };
                    if (yea.isLeapYear())
                    {
                        Console.WriteLine("Is year " + year + " leap?" + "True");
                    }
                    else
                    {
                        Console.WriteLine("Is year" + year + "leap?" + "False");
                    }
                }

                else if (command == "calc")
                {
                    DateTime date1 = AskDateTime();
                    //Console.WriteLine(date1);
                    DateTime date2 = AskDateTime();
                    //Console.WriteLine(date2);

                    Interval interval = new Interval()
                    {
                        from = date1,
                        to = date2
                    };
                    Console.WriteLine("interval lenght between " + date1 + " and " + date2 + " " + interval.FindInterval());
                }

                else if (command == "day")
                {
                    DateTime date = AskDateTime();
                    Day nameday = new Day(date);
                    Console.WriteLine(date);
                    Console.WriteLine(nameday.IndexOfDay());
                }
                else if (command == "quit")
                {
                    break;
                }
                else if (command == "save")
                {
                    Interval interval = new Interval
                    {
                        from = AskDateTime(),
                        to = AskDateTime()
                    };
                    Console.WriteLine("Select method:");
                    Console.WriteLine("1. json");
                    Console.WriteLine("2. xml");
                    Console.WriteLine("3. sqlite");
                    int method = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter path where to save:");
                    string path = Console.ReadLine();
                    switch (method) {
                        case 1:
                            File.WriteAllText(path, JsonSerializer.Serialize(interval));
                            break;
                        case 2:
                            StreamWriter writer = new StreamWriter(path);
                            (new XmlSerializer(typeof(Interval))).Serialize(writer, interval);
                            break;
                        case 3:
                            AppDbContext context = new AppDbContext(path);
                            context.Database.EnsureCreated();
                            context.Intervals.ExecuteDelete();
                            context.Intervals.Add(interval);
                            context.SaveChanges();
                            break;
                    }
                }
                else if (command == "load")
                {
                    Interval interval;
                    Console.WriteLine("Select method:");
                    Console.WriteLine("1. json");
                    Console.WriteLine("2. xml");
                    Console.WriteLine("3. sqlite");
                    int method = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter path where to load:");
                    string path = Console.ReadLine();
                    if (method == 1)
                    {
                        interval = JsonSerializer.Deserialize<Interval>(File.ReadAllText(path));
                    }
                    else if (method == 2)
                    {
                        StreamReader reader = new StreamReader(path);
                        interval = (Interval)new XmlSerializer(typeof(Interval)).Deserialize(reader);
                    }
                    else
                    {
                        AppDbContext context = new AppDbContext(path);
                        interval = context.Intervals.First();
                    }
                    Console.WriteLine("interval lenght = " + interval.FindInterval());
                }
                else
                {
                    throw new Exception("False");
                }
            
            }
    
            while (command != "quit");
            
            }

        private static DateTime AskDateTime()
        {
            Console.WriteLine("Input the year:");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Input the month:");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Input the day:");
            int day = int.Parse(Console.ReadLine());

            DateTime date = new DateTime(year, month, day);
            return date;
        }
    }
            
    }
    
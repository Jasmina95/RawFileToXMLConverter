﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class FileReader
    {
        public static XMLNode GetXMLNode ()
        {
            XMLNode People = new XMLNode { Name = "people" };

            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                using (var sr = new StreamReader(new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TextFile1.txt")).LocalPath))
#pragma warning restore CS8604 // Possible null reference argument.
                {
                    bool fileIsValid = false;

                    while (!sr.EndOfStream)
                    {
                        var nextLine = sr.ReadLine();
                        var strings = nextLine.Split('|');

                        //if (nextLine.StartsWith("P") || nextLine.StartsWith("p"))
                        if (strings[0].ToUpper() == "P")
                        {
                            if (!fileIsValid)
                            {
                                fileIsValid = true;
                            }

                            XMLNode Person = new XMLNode { Name = "person" };
                            Person.ChildNodes.AddRange(new List<XMLNode>
                            {
                                new XMLNode { Name = "firstname", Value = strings.ElementAtOrDefault(1) },
                                new XMLNode { Name = "lastname", Value = strings.ElementAtOrDefault(2) }
                            });
                            People.ChildNodes.Add(Person);
                        }
                        else if (fileIsValid)
                        {
                            XMLNode lastPerson = People.ChildNodes.Last();

                            switch (strings[0].ToUpper())
                            {
                                case "A":
                                    XMLNode Address = new XMLNode { Name = "address" };

                                    Address.ChildNodes.AddRange(new List<XMLNode>
                                    {
                                        new XMLNode { Name = "street", Value = strings.ElementAtOrDefault(1)},
                                        new XMLNode { Name = "city", Value = strings.ElementAtOrDefault(2) },
                                        new XMLNode { Name = "zipcode", Value = strings.ElementAtOrDefault(3) }
                                    });

                                    lastPerson.ChildNodes.Add(Address);
                                    break;
                                case "T":
                                    XMLNode Phone = new XMLNode { Name = "phone" };

                                    Phone.ChildNodes.AddRange(new List<XMLNode>
                                    {
                                        new XMLNode { Name = "mobile", Value = strings.ElementAtOrDefault(1) },
                                        new XMLNode { Name = "landline", Value = strings.ElementAtOrDefault(2) }
                                    });

                                    lastPerson.ChildNodes.Add(Phone);
                                    break;
                                case "F":
                                    XMLNode Family = new XMLNode { Name = "family" };

                                    Family.ChildNodes.AddRange(new List<XMLNode>
                                    {
                                        new XMLNode { Name = "name", Value = strings.ElementAtOrDefault(1) },
                                        new XMLNode { Name = "born", Value = strings.ElementAtOrDefault(2) }
                                    });

                                    lastPerson.ChildNodes.Add(Family);
                                    break;
                                default:
                                    throw new Exception("File Invalid!");
                            }
                        }
                        else
                        {
                            throw new Exception("File invalid!");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }

            return People;

        }
    }
}

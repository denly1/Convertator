using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Convertator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Figure> figuris = new List<Figure>();
            ConsoleKeyInfo key;
            do
            {



                Console.WriteLine("Введите путь до файла");
                Console.WriteLine("---------------------");
                string path = Console.ReadLine();
                Console.Clear();


                if (path.EndsWith(".txt"))
                {

                    Console.WriteLine("Вот что внутри файла. Нажмите F1 чтобы сохранить");
                    Console.WriteLine("------------------------------------------------");

                    string[] row = File.ReadAllLines(path);

                    for (int i = 0; i < row.Length; i++)
                    {
                        Console.WriteLine(row[i]);
                    }
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите путь, куда вы бы хотели сохранить");
                        Console.WriteLine("-----------------------------------------");
                        string NextPuty = Console.ReadLine();
                        Console.Clear();

                        if (NextPuty.EndsWith(".json"))
                        {
                            row = File.ReadAllLines(path);

                            for (int i = 0; i < row.Length; i += 3)
                            {
                                Figure figure = new Figure();
                                {
                                    figure.Name = row[i];
                                    figure.Size = row[i + 1];
                                    figure.Size2 = row[i + 2];

                                };
                                figuris.Add(figure);

                            }

                            string jsonn = JsonConvert.SerializeObject(figuris);
                            File.WriteAllText(NextPuty, jsonn);
                        }

                        if (NextPuty.EndsWith(".xml"))
                        {
                            row = File.ReadAllLines(path);


                            for (int i = 0; i < row.Length; i += 3)
                            {
                                Figure figure = new Figure();
                                {
                                    figure.Name = row[i];
                                    figure.Size = row[i + 1];
                                    figure.Size2 = row[i + 2];
                                };
                                figuris.Add(figure);
                            }

                            XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                            using (FileStream fileS = new FileStream(NextPuty, FileMode.Create))
                            {
                                xml.Serialize(fileS, figuris);
                            }


                        }



                        Console.Clear();
                        string json = JsonConvert.SerializeObject(figuris);
                        File.WriteAllText(NextPuty, json);
                    }
                }

                if (path.EndsWith(".json"))
                {
                    Console.WriteLine("Вот что внутри файла");
                    Console.WriteLine("--------------------");
                    if (File.Exists(path))
                    {
                        string json = File.ReadAllText(path);
                        figuris = JsonConvert.DeserializeObject<List<Figure>>(json);
                        foreach (var figure in figuris)
                        {
                            Console.WriteLine(figure.Name);
                            Console.WriteLine(figure.Size);
                            Console.WriteLine(figure.Size2);
                            Console.WriteLine("----------------");
                        }

                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.Clear();

                        Console.WriteLine("Введите путь, куда вы бы хотели сохранить");
                        Console.WriteLine("-----------------------------------------");
                        string NextPuty = Console.ReadLine();
                        Console.Clear();

                        if (NextPuty.EndsWith(".txt"))
                        {
                            string Text = File.ReadAllText(path);
                            figuris = JsonConvert.DeserializeObject<List<Figure>>(Text);
                            foreach (var figure in figuris)
                            {

                                File.AppendAllText(NextPuty, figure.Name);
                                File.AppendAllText(NextPuty, "\n");
                                File.AppendAllText(NextPuty, figure.Size);
                                File.AppendAllText(NextPuty, "\n");
                                File.AppendAllText(NextPuty, figure.Size2);
                                File.AppendAllText(NextPuty, "\n");
                            }
                        }

                        if (NextPuty.EndsWith(".xml"))
                        {
                            string Text = File.ReadAllText(path);
                            figuris = JsonConvert.DeserializeObject<List<Figure>>(Text);
                            XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                            using (FileStream fileS = new FileStream(NextPuty, FileMode.Create))
                            {
                                xml.Serialize(fileS, figuris);
                            }
                        }
                    }
                }

                if (path.EndsWith(".xml"))
                {
                    Console.WriteLine("Вот что внутри файла");
                    Console.WriteLine("--------------------");
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Figure>));
                    using (FileStream FilePokaz = new FileStream(path, FileMode.Open))
                    {
                        figuris = (List<Figure>)xmlSerializer.Deserialize(FilePokaz);
                        foreach (var figure in figuris)
                        {
                            Console.WriteLine(figure.Name);
                            Console.WriteLine(figure.Size);
                            Console.WriteLine(figure.Size2);
                            Console.WriteLine("---------------");
                        }
                    }
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.Clear();
                        Console.Clear();
                        Console.WriteLine("Введите путь, куда вы бы хотели сохранить");
                        Console.WriteLine("-----------------------------------------");
                        string NextPuty = Console.ReadLine();
                        Console.Clear();

                        if (NextPuty.EndsWith(".txt"))
                        {

                            XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                            using (FileStream fileS = new FileStream(path, FileMode.Open))
                            {
                                figuris = (List<Figure>)xml.Deserialize(fileS);
                            }

                            foreach (Figure figure in figuris)
                            {

                                File.AppendAllText(NextPuty, figure.Name);
                                File.AppendAllText(NextPuty, "\n");
                                File.AppendAllText(NextPuty, figure.Size);
                                File.AppendAllText(NextPuty, "\n");
                                File.AppendAllText(NextPuty, figure.Size2);
                                File.AppendAllText(NextPuty, "\n");
                            }
                        }
                        if (NextPuty.EndsWith(".json"))
                        {
                            XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                            using (FileStream fileS = new FileStream(path, FileMode.Open))
                            {
                                figuris = (List<Figure>)xml.Deserialize(fileS);
                            }
                            Console.WriteLine(figuris);
                            string json = JsonConvert.SerializeObject(figuris);
                            File.WriteAllText(NextPuty, json);
                        }

                    }
                }
                Console.WriteLine("Конвертация выполнилась");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
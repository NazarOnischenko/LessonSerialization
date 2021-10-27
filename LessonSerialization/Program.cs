using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace LessonSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Group> groups = new List<Group>();
            List<Student> students = new List<Student>();

            for (int i = 1;i<11;i++)
            {
                groups.Add(new Group(i, "Group " + i));
            }

            for (int i = 1;i < 301; i++)
            {
                var student = new Student(Guid.NewGuid().ToString().Substring(0, 5), i % 100 + 1)
                {
                    Group = groups[i % 10]
                };
                students.Add(student);
            }

            SerializableBin<Group>(groups, "binary/groups.bin");
            DeserializableBin<Group>("binary/groups.bin");


            SerializableSoap<Group>(groups, "binary/students.soap");
            DeserializableSoap<Group>("binary/students.soap");

            SerializableXml<Group>(groups, "binary/groups.xml");
            DeserializableXml<Group>("binary/groups.xml");

            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));
            using (var file = new FileStream("binary/students.json",FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(file, students);
            }
            using (var file = new FileStream("binary/students.json",FileMode.Open))
            {
                var newLists = jsonFormatter.ReadObject(file) as List<Student>;
                if (newLists != null)
                {
                    foreach (var newList in newLists)
                    {
                        Console.WriteLine(newList);
                    }
                }
            }
        }
        static void SerializableBin<T>(List<T> lists,string fileName)
        {
            var binaryFormater = new BinaryFormatter();
            using (var file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                binaryFormater.Serialize(file, lists);
            }
            Console.WriteLine("Serializable is success :)");
        }
        static void DeserializableBin<T>(string fileName)
        {
            var binaryFormater = new BinaryFormatter();
            using (var file = new FileStream(fileName, FileMode.Open))
            {
                var newLists = binaryFormater.Deserialize(file) as List<T>;
                if (newLists != null)
                {
                    foreach (var newList in newLists)
                    {
                        Console.WriteLine(newList);
                    }
                }
            }
        }
        static void SerializableSoap<T>(List<T>lists, string fileName)
        {
            var soapFormatter = new SoapFormatter();
            
            using (var file = new FileStream(fileName,FileMode.OpenOrCreate))
            {
                soapFormatter.Serialize(file,lists.ToArray());
            }

            Console.WriteLine("Serialize if success :)");
        }
        static void DeserializableSoap<T>(string fileName)
        {
            var soapFormatter = new SoapFormatter();
            using (var file = new FileStream(fileName,FileMode.Open))
            {
                var newLists = soapFormatter.Deserialize(file) as T[];
                if (newLists != null)
                {
                    foreach (var newList in newLists)
                    {
                        Console.WriteLine(newList);
                    }
                }
            }
        }

        static void SerializableXml<T>(List<T> lists,string fileName)
        {
            var xmlFormatter = new XmlSerializer(typeof(List<T>));
            using (var file = new FileStream(fileName,FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(file,lists);
            }
            Console.WriteLine("Serilizable is success :)");
        }

        static void DeserializableXml<T>(string fileName)
        {
            var xmlFormatter = new XmlSerializer(typeof(List<T>));
            using (var file = new FileStream(fileName,FileMode.Open))
            {
                var newLists = xmlFormatter.Deserialize(file) as List<T>;
                if (newLists != null)
                {
                    foreach (var newList in newLists)
                    {
                        Console.WriteLine(newList);
                    }
                }
            }
        }
    }
}

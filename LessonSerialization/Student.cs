using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LessonSerialization
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        
        public Group Group { get; set; }
        
        public Student(string name, int age) 
        {
            if (name == null && name == string.Empty)
            {
                throw new ArgumentNullException("Name must be not empty...");
            }
            if (age <= 0)
            {
                throw new ArgumentNullException("Age must be bigger than 0...");
            }
            Name = name;
            Age = age;
        }
        public override string ToString()
        {
            return $"{Name} have {Age} years old";
        }

    }
}

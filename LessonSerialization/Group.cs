using System;
using System.Collections.Generic;
using System.Text;

namespace LessonSerialization
{
    [Serializable]
    public class Group
    {
        [NonSerialized]
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        public int Number { get; set; }
        public string Name { get; set; }
        
        public Group() 
        {
            Number = rnd.Next(1, 11);
            Name = "Group " + Number;
        }
        public Group(int number, string name)
        {
            if (number <= 0)
            {
                throw new ArgumentNullException("Number group must be bigger than 0...");
            }
            if (name == null && name == string.Empty)
            {
                throw new ArgumentNullException("Name must be not empty...");
            }
            Number = number;
            Name = name;
        }
        public override string ToString()
        {
            return $"{Name} number {Number}";
        }
    }
}

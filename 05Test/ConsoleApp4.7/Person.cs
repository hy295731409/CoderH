using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._7
{
    //public class Person
    //{
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //    public Person Partner { get; set; }
    //    public decimal Salary { get; set; }
    //}
    public struct Person : IEquatable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        //public Person Partner { get; set; }
        public decimal Salary { get; set; }

        public bool Equals(Person obj)
        {
            Console.WriteLine("自定义");
            return this.Name == obj.Name;
        }
        public override bool Equals(object obj)
        {
            Console.WriteLine("通用");
            var other = (Person)obj;
            return this.Name == other.Name;
        }

        //public bool IsEqual(Person p)
        //{
        //    var dft = EqualityComparer<Person>.Default;
        //    return dft.Equals(this, p);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._7.test
{
    public class Test
    {
        public void GetTest()
        {
            List<Student> studentList = new List<Student>
            {
                new Student {ClassName = "软工一班",sex = 0, StudentName = "康巴一", StuId = 1},
                new Student {ClassName = "软工一班",sex = 1, StudentName = "康巴二", StuId = 2},
                new Student {ClassName = "软工一班",sex = 0, StudentName = "康巴三", StuId = 3},
                new Student {ClassName = "软工二班",sex = 1, StudentName = "康定4", StuId = 4},
                new Student {ClassName = "软工二班",sex = 0, StudentName = "康定5", StuId = 5},
                new Student {ClassName = "软工二班",sex = 1, StudentName = "康定6", StuId = 6},
            };


            var studentGroup = studentList.GroupBy(s => new { s.ClassName, s.sex }).ToList();
            var t = studentList.GroupBy(s => new { s.ClassName, s.sex }).Select(s => new { s.Key, count = s.Count() });

        }
        public class Student
        {
            public int StuId { get; set; }

            public string ClassName { get; set; }

            public string StudentName { get; set; }

            public int sex { get; set; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Student
    {
        /// <summary>
        /// ID 属性成为此类对应的数据库表的主键列。 默认情况下，EF Core 将名为 ID 或 classnameID 的属性视为主键。 
        /// 因此，Student 类主键的另一种自动识别的名称是 StudentID。
        /// </summary>
        public int ID { get; set; }//主键
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }//导航属性
    }
}

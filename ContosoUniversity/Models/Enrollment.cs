using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }//主键
        public int CourseID { get; set; }//外键
        public int StudentID { get; set; }//外键
        public Grade? Grade { get; set; }

        public Course Course { get; set; }//CourseID对应的导航属性
        public Student Student { get; set; }//StudentID对应的导航属性
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    //Student 和 Course 实体之间存在多对多关系。 Enrollment 实体充当数据库中“具有有效负载”的多对多联接表 。 
    //“具有有效负载”表示 Enrollment 表除了联接表的 FK 外还包含其他数据（本教程中为 PK 和 Grade）
    //如果 Enrollment 表不包含年级信息，则它只需包含两个 FK（CourseID 和 StudentID）。 无有效负载的多对多联接表有时称为纯联接表(PJT)
    public class Enrollment
    {
        public int EnrollmentID { get; set; }//主键
        public int CourseID { get; set; }//外键
        public int StudentID { get; set; }//外键
        [DisplayFormat(NullDisplayText ="NO Grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; }//CourseID对应的导航属性
        public Student Student { get; set; }//StudentID对应的导航属性
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    //“讲师-课程”的多对多关系要求使用联接表，而该联接表的实体则为 CourseAssignment
    //CourseAssignment 中的两个 FK（InstructorID 和 CourseID）共同唯一标识 CourseAssignment 表的每一行。 
    //CourseAssignment 不需要专用的 PK。 InstructorID 和 CourseID 属性充当组合 PK。 
    //使用 Fluent API 是向 EF Core 指定组合 PK 的唯一方法

    //组合键可确保：
    //允许一门课程对应多行。
    //允许一名讲师对应多行。
    //不允许同一讲师和课程对应多行。
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}

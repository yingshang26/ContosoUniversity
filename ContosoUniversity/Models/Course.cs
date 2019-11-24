using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Course
    {
        //应用可以通过 DatabaseGenerated 特性指定主键，指定 PK 由应用程序提供而不是由数据库生成。。
        //默认情况下，EF Core 假定 PK 值由数据库生成。 数据库生成通常是最佳方法。 
        //Course 实体的 PK 由用户指定。 
        //例如，对于课程编号，数学系可以使用 1000 系列的编号，英语系可以使用 2000 系列的编号。
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Number")]
        public int CourseID { get; set; }//主键

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0,5)]
        public int Credits { get; set; }

        //Course 实体具有外键 (FK) 属性 DepartmentID
        //DepartmentID 指向相关的 Department 实体
        public int DepartmentID { get; set; }
        //当数据模型具有相关实体的导航属性时，EF Core 不要求此模型具有外键属性。
        //EF Core 可在数据库中的任何所需位置自动创建 FK。 EF Core 为自动创建的 FK 创建阴影属性。 
        //然而，在数据模型中显式包含 FK 可使更新更简单和更高效。 例如，假设某个模型中不包含 FK 属性 DepartmentID 。

        //课程将分配到一个系
        //Course 实体具有 Department 导航属性
        public Department Department { get; set; }//todo:这些对象实体在数据库中是如何保存的？数据库中有Department的一张数据表

        //参与一门课程的学生数量不定，因此 Enrollments 导航属性是一个集合
        public ICollection<Enrollment> Enrollments { get; set; }//导航属性

        //一门课程可能由多位讲师讲授，因此 CourseAssignments 导航属性是一个集合
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}

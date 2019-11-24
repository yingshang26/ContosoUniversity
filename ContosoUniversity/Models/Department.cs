using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        //Column 特性用于更改 SQL 数据类型映射。 
        //Budget 列通过数据库中的 SQL Server 货币类型进行定义
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        //一个系可能有也可能没有管理员
        //管理员始终由讲师担任。 因此，InstructorID 属性作为到 Instructor 实体的 FK 包含在其中
        //按照约定，EF Core 能针对不可为 NULL 的 FK 和多对多关系启用级联删除。 
        //此默认行为可能导致形成循环级联删除规则。 循环级联删除规则会在添加迁移时引发异常。
        //例如，如果将 Department.InstructorID 属性定义为不可为 null，EF Core 将配置级联删除规则。 
        //在这种情况下，指定为管理员的讲师被删除时，该学院将被删除。 在这种情况下，限制规则将更有意义。 以下 fluent API 将设置限制规则并禁用级联规则。
        public int? InstructorID { get; set; }

        public Instructor Administrator { get; set; }

        //一个系可以有多门课程，因此存在 Course 导航属性
        public ICollection<Course> Courses { get; set; }
    }
}

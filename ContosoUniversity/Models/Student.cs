using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="First name cannot be longer than 50 characters")]
        //创建数据库后，模型上的属性名将用作列名（使用 Column 特性时除外）。 
        //Student 模型使用 FirstMidName 作为名字字段，因为该字段也可能包含中间名。
        //Column 特性用于将 FirstMidName 属性的名称映射到数据库中的“FirstName”
        [Column("FirstName")]//todo:什么意思?
        [Display(Name ="First Name")]
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="0:yyyy-MM-dd",ApplyFormatInEditMode =true)]
        [Display(Name ="Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name ="Full Name")]
        public string FullName//无法设置 FullName，因此它只有一个 get 访问器。 数据库中不会创建任何 FullName 列。
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
        public ICollection<Enrollment> Enrollments { get; set; }//导航属性
    }
}

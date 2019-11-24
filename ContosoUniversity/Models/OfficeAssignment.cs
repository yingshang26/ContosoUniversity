using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        //[Key] 特性用于在属性名不是 classnameID 或 ID 时将属性标识为主键 (PK)。
        //OfficeAssignment PK 也是其到 Instructor 实体的外键 (FK)
        [Key]
        public int InstructorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        //导航属性
        public Instructor Instructor { get; set; }
    }
}

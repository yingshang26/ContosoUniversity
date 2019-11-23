﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Course
    {
        //应用可以通过 DatabaseGenerated 特性指定主键，而无需靠数据库生成。
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }//主键
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }//导航属性
    }
}

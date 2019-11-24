using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
    public class SchoolContext : DbContext//数据库上下文类是为给定数据模型协调 EF Core 功能的主类
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        //为每个实体集创建 DbSet<TEntity> 属性
        //在 EF Core 术语中：
        //实体集通常对应数据库表。
        //实体对应表中的行。
        public DbSet<ContosoUniversity.Models.Student> Students { get; set; }//todo:这些数据哪来的？DbInitializer中的种子数据
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        //todo:这个函数何时调用，有啥作用
        //上面代码中的 OnModelCreating 方法使用 Fluent API 配置 EF Core 行为 。 
        //API 称为“Fluent”，因为它通常在将一系列方法调用连接成单个语句后才能使用。

        //在本教程中，fluent API 仅用于不能通过特性完成的数据库映射。 
        //但是，Fluent API 可以指定可通过特性完成的大多数格式设置、验证和映射规则。

        //某些开发者倾向于仅使用 Fluent API 以保持实体类的“纯净”。 特性和 Fluent API 可以相互混合。 
        //某些配置只能通过 Fluent API 完成（指定组合 PK）。 有些配置只能通过特性完成 (MinimumLength)。 
        //使用 Fluent API 或特性的建议做法：
        //选择以下两种方法之一。
        //尽可能以前后一致的方法使用所选的方法。
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            //指定组合主键
            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });
            //禁用级联删除规则
            //todo:这种表达式什么意思？
            //modelBuilder.Entity<Department>().HasOne(d => d.Administrator).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}

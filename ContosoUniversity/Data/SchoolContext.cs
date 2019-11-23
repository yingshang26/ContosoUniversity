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
        public DbSet<ContosoUniversity.Models.Student> Students { get; set; }//todo:这些数据哪来的？
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        //todo:这个函数何时调用，有啥作用
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}

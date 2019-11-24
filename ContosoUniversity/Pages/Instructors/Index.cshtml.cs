using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData InstructorData { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        //public IList<Instructor> Instructor { get;set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            //Instructor = await _context.Instructors.ToListAsync();
            InstructorData = new InstructorIndexData();
            //代码指定以下导航属性的预先加载：
            //* Instructor.OfficeAssignment
            //* Instructor.CourseAssignments
            //      *CourseAssignments.Course
            //          *Course.Department
            //          *Course.Enrollments
            //              *Enrollment.Student
            InstructorData.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Enrollments)
                            .ThenInclude(i => i.Student)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)//选择讲师时
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.ID == id.Value).Single();//Where 方法返回一个集合。 但在本例中，筛选器将选择单个实体。 因此，调用 Single 方法将集合转换为单个 Instructor 实体。
                //当集合仅包含一个项时，集合使用 Single 方法。 如果集合为空或包含多个项，Single 方法会引发异常。 
                //还可使用 SingleOrDefault，该方式在集合为空时返回默认值（本例中为 null）。
                InstructorData.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)//选择课程时
            {
                CourseID = courseID.Value;
                var selectedCourse = InstructorData.Courses
                    .Where(x => x.CourseID == courseID).Single();
                //假设用户几乎不希望课程中显示注册情况（预先加载）。 在此情况下，可仅在请求时加载注册数据进行优化。 
                //在本部分中，会更新 OnGetAsync 以使用 Enrollments 和 Students 的显式加载。

                //await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                //foreach (Enrollment enrollment in selectedCourse.Enrollments)
                //{
                //    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                //}
                InstructorData.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}

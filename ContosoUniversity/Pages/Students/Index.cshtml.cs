using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public PaginatedList<Student> Students { get;set; }

        public async Task OnGetAsync(string sortOrder,string currentFilter,string searchString,int? pageIndex)//todo:sortOrder是谁在赋值，怎么赋值的？就是通过下面两句实现的
        {//todo:searchString是如何传递进来的，form、input标记的机制吧！
            CurrentSort = sortOrder;

            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if(searchString!=null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;//解决选中列标题排序链接时，“搜索”框中的筛选值会丢失的问题，即排序和筛选不能同时进行的问题
            }

            CurrentFilter = searchString;//否则，点击Search后，输入的筛选值会被清空
            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;
            //该方法使用 LINQ to Entities 指定要作为排序依据的列

            if(!string.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;

            Students = await PaginatedList<Student>.CreateAsync(studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}

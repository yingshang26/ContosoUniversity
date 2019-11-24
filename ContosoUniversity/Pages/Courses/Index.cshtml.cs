using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public IList<Course> Courses { get;set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                .Include(c => c.Department)//预先将相关数据加载到实体的导航属性中
                .AsNoTracking()//todo:AsNoTracking什么意思？=>由于未跟踪返回的实体，因此 AsNoTracking 提升了性能。 无需跟踪实体，因为未在当前的上下文中更新这些实体。
                .ToListAsync();
        }
    }
}

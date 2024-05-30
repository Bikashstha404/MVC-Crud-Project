using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsPortal.Web.Data;
using StudentsPortal.Web.Models;
using StudentsPortal.Web.Models.Entities;

namespace StudentsPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbcontext=dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };
            await dbcontext.Students.AddAsync(student);
            await dbcontext.SaveChangesAsync();
            return View();  
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbcontext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var Student = await dbcontext.Students.FindAsync(id);

            return View(Student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbcontext.Students.FindAsync(viewModel.Id);

            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbcontext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (student is not null)
            {
                dbcontext.Students.Remove(viewModel);
                await dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using TaskList.Database;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class TaskListController : Controller
    {
        private readonly DataContext _context;

        public TaskListController(DataContext context)
        {
            _context = context;
        }

        //GET method        
        public async Task<ActionResult> Index()
        {
            IQueryable<ToDo> items = from i in _context.taskLists orderby i.Id select i;

            List<ToDo> list = await items.ToListAsync();

            return View(list);
        }

        //GET create        
        public IActionResult Create ()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDo item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();              

                return RedirectToAction("Index");
            }

            return View(item);
        }

        //GET method        
        public async Task<ActionResult> Edit(int id)
        {
            ToDo? item = await _context.taskLists.FindAsync(id);
            if (item== null)
            {
                return NotFound();
            }

            return View(item);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ToDo item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();                

                return RedirectToAction("Index");
            }

            return View(item);
        }
        
        //GET
        public async Task<ActionResult> Delete(int id)
        {
            ToDo? item = await _context.taskLists.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            else
            {
                _context.taskLists.Remove(item);
                await _context.SaveChangesAsync();
            }            

            return RedirectToAction("Index");
        }
    }
}

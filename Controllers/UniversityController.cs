using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UniversityApp.Data;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApp.Controllers;

[Route("studs")]
public class UniversityController : Controller
{
    private readonly UniversityContext _context;

    public UniversityController(ILogger<UniversityController> logger, UniversityContext context)
    {
       
        _context = context;

    }

    //возвращает список студентов
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        try{
            var students = await _context.Students.Select(s => new
            {
                s.Id,
                s.Name,
                s.Score,
                s.Age
            }).ToListAsync();
            Console.WriteLine("Data is succeed!" + $"{students.Count}");
            return View(students);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return View();
        }

    }

    //открытие формы для создания нового пользователя
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    //создает студента
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Age,Score")] Student student)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                Console.WriteLine("Student add" + $"{student.Id}");
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("Model is not valid" + $"{student.Id}");
            return View(student);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return View();
        }
       
    }

    //переход на форму редактирования
    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    //редактирование студента, который есть в БД
    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Score,Age")] Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }

    //переход на форму редактирования
    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    //удаляет пользователя
    [HttpPost("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, [Bind("Id,Name,Score,Age")] Student student)
    {
         _context.Students.Remove(student);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
    }

}

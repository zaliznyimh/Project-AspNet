using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers;

public class FieldOfStudiesController : Controller
{
    private readonly StudentsContext _context;
    private readonly IDatabaseService _databaseService;

    public FieldOfStudiesController(StudentsContext context, IDatabaseService databaseService)
    {
        _context = context;
        _databaseService = databaseService;
    }

    // GET: FieldOfStudies
    public async Task<IActionResult> Index()
    {
        var listOfFields = await _databaseService.GetFieldOfStudyListAsync();
        return View(listOfFields);
    }

    // GET: FieldOfStudies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fieldOfStudy = await _databaseService.GetFieldOfStudyInfoAsync(id);
        if (fieldOfStudy == null)
        {
            return NotFound();
        }

        return View(fieldOfStudy);
    }

    // GET: FieldOfStudies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FieldOfStudies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,DurationOfStudies,NumberOfStudents")] FieldOfStudy fieldOfStudy)
    {
        if (ModelState.IsValid)
        {
            var result = await _databaseService.CreateFieldOfStudyAsync(fieldOfStudy);
            return RedirectToAction(nameof(Index));
        }
        return View(fieldOfStudy);
    }

    // GET: FieldOfStudies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fieldOfStudy = await _databaseService.GetFieldOfStudyInfoAsync(id);
        if (fieldOfStudy == null)
        {
            return NotFound();
        }
        return View(fieldOfStudy);
    }

    // POST: FieldOfStudies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DurationOfStudies,NumberOfStudents")] FieldOfStudy fieldOfStudy)
    {
        if (id != fieldOfStudy.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var result = await _databaseService.EditFieldOfStudyAsync(fieldOfStudy);
            return RedirectToAction(nameof(Index));
        }
        return View(fieldOfStudy);
    }

    // GET: FieldOfStudies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fieldOfStudy = await _databaseService.GetFieldOfStudyInfoAsync(id);
        if (fieldOfStudy == null)
        {
            return NotFound();
        }

        return View(fieldOfStudy);
    }

    // POST: FieldOfStudies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _databaseService.DeleteFieldOfStudyAsync(id);
        return RedirectToAction(nameof(Index));
    }

}

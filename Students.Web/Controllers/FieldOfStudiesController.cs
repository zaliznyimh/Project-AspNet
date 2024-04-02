using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;

namespace Students.Web.Controllers;

public class FieldOfStudiesController : Controller
{
    private readonly StudentsContext _context;

    public FieldOfStudiesController(StudentsContext context)
    {
        _context = context;
    }

    // GET: FieldOfStudies
    public async Task<IActionResult> Index()
    {
        return View(await _context.FieldOfStudies.ToListAsync());
    }

    // GET: FieldOfStudies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fieldOfStudy = await _context.FieldOfStudies
            .FirstOrDefaultAsync(m => m.Id == id);
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
            _context.Add(fieldOfStudy);
            await _context.SaveChangesAsync();
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

        var fieldOfStudy = await _context.FieldOfStudies.FindAsync(id);
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
            try
            {
                _context.Update(fieldOfStudy);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldOfStudyExists(fieldOfStudy.Id))
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
        return View(fieldOfStudy);
    }

    // GET: FieldOfStudies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fieldOfStudy = await _context.FieldOfStudies
            .FirstOrDefaultAsync(m => m.Id == id);
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
        var fieldOfStudy = await _context.FieldOfStudies.FindAsync(id);
        if (fieldOfStudy != null)
        {
            _context.FieldOfStudies.Remove(fieldOfStudy);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FieldOfStudyExists(int id)
    {
        return _context.FieldOfStudies.Any(e => e.Id == id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers;

public class SubjectsController : Controller
{
    private readonly StudentsContext _context;
    private readonly IDatabaseService _databaseService;

    public SubjectsController(StudentsContext context,
                            IDatabaseService databaseService)
    {
        _context = context;
        _databaseService = databaseService;
    }

    // GET: Subjects
    public async Task<IActionResult> Index()
    {
        var result = await _databaseService.GetSubjectsList();
        return View(result);
    }

    // GET: Subjects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.GetSubjectToEditAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        return View(subject);
    }

    // GET: Subjects/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.FieldOfStudies = await _context.FieldOfStudies.ToListAsync();
        return View();
    }

    // POST: Subjects/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Credits")] Subject subject, int fieldOfStudyId)
    {
        var fieldOfStudy = await _context.FieldOfStudies.FindAsync(fieldOfStudyId);
        if(fieldOfStudy == null)
        {
            ModelState.AddModelError("FieldOfStudies", "Select field of study");
        } 
        if (ModelState.IsValid)
        {
            subject.FieldOfStudy = fieldOfStudy;
            var result = await _databaseService.CreateSubjectAsync(subject);
            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    // GET: Subjects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        ViewBag.FieldOfStudies = await _context.FieldOfStudies.ToListAsync();
        var subject = await _databaseService.GetSubjectToEditAsync(id);
        if (subject == null)
        {
            return NotFound();
        }
        return View(subject);
    }

    // POST: Subjects/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Credits")] Subject subject, int fieldOfStudyId)
    {
        if (id != subject.Id)
        {
            return NotFound();
        }
        try
        {
            var fieldOfStudy = await _context.FieldOfStudies.FindAsync(fieldOfStudyId);
            if (fieldOfStudy == null)
            {
                ModelState.AddModelError("FieldOfStudies", "Select field of study");
            }
            if (ModelState.IsValid)
            {
                subject.FieldOfStudy = fieldOfStudy;
                var result = await _databaseService.EditSubject(subject);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubjectExists(subject.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return View(subject);
    }

    // GET: Subjects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.GetSubjectToDelete(id);
        if (subject == null)
        {
            return NotFound();
        }

        return View(subject);
    }

    // POST: Subjects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _databaseService.DeleteSubject(id);
        return RedirectToAction(nameof(Index));
    }

    private bool SubjectExists(int id)
    {
        var result = _databaseService.CheckSubjectExists(id);
        return result;
    }
}

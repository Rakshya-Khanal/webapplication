using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorized]
    public class SInfoController : Controller
    {
        private readonly AppDbCOntext _context;

        public SInfoController(AppDbCOntext context)
        {
            _context = context;
        }

        // GET: SInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.studentinfo.ToListAsync());
        }

        // GET: SInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentinfo = await _context.studentinfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentinfo == null)
            {
                return NotFound();
            }

            return View(studentinfo);
        }

        // GET: SInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,PhoneNumber,Email,Faculty")] studentinfo studentinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentinfo);
        }

        // GET: SInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentinfo = await _context.studentinfo.FindAsync(id);
            if (studentinfo == null)
            {
                return NotFound();
            }
            return View(studentinfo);
        }

        // POST: SInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,PhoneNumber,Email,Faculty")] studentinfo studentinfo)
        {
            if (id != studentinfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!studentinfoExists(studentinfo.Id))
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
            return View(studentinfo);
        }
        [Authorized(Role ="Manager")] 
        // GET: SInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentinfo = await _context.studentinfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentinfo == null)
            {
                return NotFound();
            }

            return View(studentinfo);
        }
        

        // POST: SInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentinfo = await _context.studentinfo.FindAsync(id);
            _context.studentinfo.Remove(studentinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool studentinfoExists(int id)
        {
            return _context.studentinfo.Any(e => e.Id == id);
        }
    }
}

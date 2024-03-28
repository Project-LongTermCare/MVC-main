using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Long_Term_Care.Models;

namespace Long_Term_Care.Controllers
{
    public class AppointmentForms1Controller : Controller
    {
        private readonly LongTermCareContext _context;

        public AppointmentForms1Controller(LongTermCareContext context)
        {
            _context = context;
        }

        // GET: AppointmentForms1
        public async Task<IActionResult> Index()
        {
            var longTermCareContext = _context.AppointmentForms.Include(a => a.Case).Include(a => a.Employee).Include(a => a.Member).Include(a => a.Service);
            return View(await longTermCareContext.ToListAsync());
        }

        // GET: AppointmentForms1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentForm = await _context.AppointmentForms
                .Include(a => a.Case)
                .Include(a => a.Employee)
                .Include(a => a.Member)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (appointmentForm == null)
            {
                return NotFound();
            }

            return View(appointmentForm);
        }

        // GET: AppointmentForms1/Create
        public IActionResult Create()
        {
            AppointmentForm model = new AppointmentForm();
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId");
            return View(model);
        }

        // POST: AppointmentForms1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveId,MemberId,ReserveTime,CaseId,CaseName,EmployeeId,EmployeeName,ServiceId,WorkingDate,StartTime,EndTime,CaseAvatar")] AppointmentForm appointmentForm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        appointmentForm.CaseAvatar = dataStream.ToArray();
                    }
                }
                _context.Add(appointmentForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", appointmentForm.CaseId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", appointmentForm.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", appointmentForm.MemberId);
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId", appointmentForm.ServiceId);
            return View(appointmentForm);
        }

        // GET: AppointmentForms1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentForm = await _context.AppointmentForms.FindAsync(id);
            if (appointmentForm == null)
            {
                return NotFound();
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", appointmentForm.CaseId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", appointmentForm.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", appointmentForm.MemberId);
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId", appointmentForm.ServiceId);
            return View(appointmentForm);
        }

        // POST: AppointmentForms1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveId,MemberId,ReserveTime,CaseId,CaseName,EmployeeId,EmployeeName,ServiceId,WorkingDate,StartTime,EndTime,CaseAvatar")] AppointmentForm appointmentForm)
        {
            if (id != appointmentForm.ReserveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentFormExists(appointmentForm.ReserveId))
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
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", appointmentForm.CaseId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", appointmentForm.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", appointmentForm.MemberId);
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId", appointmentForm.ServiceId);
            return View(appointmentForm);
        }

        // GET: AppointmentForms1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentForm = await _context.AppointmentForms
                .Include(a => a.Case)
                .Include(a => a.Employee)
                .Include(a => a.Member)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (appointmentForm == null)
            {
                return NotFound();
            }

            return View(appointmentForm);
        }

        // POST: AppointmentForms1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentForm = await _context.AppointmentForms.FindAsync(id);
            if (appointmentForm != null)
            {
                _context.AppointmentForms.Remove(appointmentForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentFormExists(int id)
        {
            return _context.AppointmentForms.Any(e => e.ReserveId == id);
        }
    }
}

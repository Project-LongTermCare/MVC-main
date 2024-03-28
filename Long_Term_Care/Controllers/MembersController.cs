using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Long_Term_Care.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.IdentityModel.Tokens;

namespace Long_Term_Care.Controllers
{
    public class MembersController : Controller
    {
        private readonly LongTermCareContext _context;

        public MembersController(LongTermCareContext context)
        {
            _context = context;
        }
       
        // GET: Members
        public async Task<IActionResult> Index()
        {
            var longTermCareContext = _context.Members.Include(m => m.Case);
            
            return View(await longTermCareContext.ToListAsync());
        }
		
		// GET: Members/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Case)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,UserName,Password,MemberAvatar,MemberName,Gender,Email,HomePhone,MobilePhone,City,District,Address,CaseId")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", member.CaseId);
            return View(member);
        }
       
        public IActionResult LogIn()
        {
            
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> LogIn(string account, string password)
        {
            if (account == null)
            {
				return View("LogIn");
			}

            var access = await _context.Members
                .FirstOrDefaultAsync(m => m.UserName == account && m.Password == password);
            
            if (access == null)
            {
                 
                ViewBag.Error = "帳密驗證失敗";
                return View("LogIn");

            }
			var x = access!.MemberId;
			var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, access.UserName),			
			new Claim("FullName", access.MemberName),           
			new Claim(ClaimTypes.Role, "Administrator"),
		};

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				//AllowRefresh = <bool>,
				// Refreshing the authentication session should be allowed.

				//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				// The time at which the authentication ticket expires. A 
				// value set here overrides the ExpireTimeSpan option of 
				// CookieAuthenticationOptions set with AddCookie.

				//IsPersistent = true,
				// Whether the authentication session is persisted across 
				// multiple requests. When used with cookies, controls
				// whether the cookie's lifetime is absolute (matching the
				// lifetime of the authentication ticket) or session-based.

				//IssuedUtc = <DateTimeOffset>,
				// The time at which the authentication ticket was issued.

				//RedirectUri = <string>
				// The full path or absolute URI to be used as an http 
				// redirect response value.
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
			return RedirectToAction("Edit",new {id = x});
        }

        public async Task<ActionResult> Logout()
        {
			string xx = User.FindFirst("FullName")?.Value;
			if (!string.IsNullOrEmpty(xx))
			{
				ModelState.AddModelError(string.Empty,xx);
			}

			// Clear the existing external cookie
			await HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("LogIn");
        }


		[Authorize]
		//GET: Members/Edit/5
		public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
				
				return RedirectToPage("/Members/LogIn");

			}

			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			//var member = await _context.Members.FindAsync(id);
			
           
           var member= await _context.Members.FirstOrDefaultAsync(m => m.UserName == xx);
            if (member == null)
			{
				return RedirectToPage("/Members/LogIn");
			}


			if (!string.IsNullOrEmpty(xx))
			{
                ViewBag.CasIdValue = member.CaseId;
                var asdf = _context.Cases.Count();
                ViewBag.add = asdf + 1;
                ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", member.CaseId);
				return View(member);

			}


			return RedirectToPage("/Members/LogIn");
		}


        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,UserName,Password,MemberAvatar,MemberName,Gender,Email,HomePhone,MobilePhone,City,District,Address,CaseId")] Member member)
        {

            if (id != member.MemberId)
                {
                    return NotFound();
                }
            
          

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        IFormFile file = Request.Form.Files.FirstOrDefault();
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            member.MemberAvatar = dataStream.ToArray();
                        }
                    }
                    _context.Update(member);
                   

					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
		
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", member.CaseId);
            return View(member);
        }
        

        public async Task<IActionResult> CreateCase()
        {
            string xx = User.FindFirst(ClaimTypes.Name)?.Value;
            //var member = await _context.Members.FindAsync(id);


            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserName == xx);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId",member.MemberId);
            return View();       
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCase([Bind("CaseId,CaseName,Gender,Birthdate,Age,IdentificationNumber,IdentityType,CasePhone,FamilyPhone,FamilyName,City,District,Address,Relation,MemberId")] Case Cases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Cases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit));
            }
            ViewData["CaseId"] = new SelectList(_context.Members, "MemberId", "MemberId", Cases.MemberId);
            return View(Cases);
        }
        [Authorize]
        //GET: Members/EditCase/5
        public async Task<IActionResult> EditCase(int id)
        {
            if (id == null)
            {

                return RedirectToPage("/Members/LogIn");

            }

            //string xx = User.FindFirst(ClaimTypes.Name)?.Value;
            //var member = await _context.Members.FindAsync(id);
            var Cases = await _context.Cases.FirstOrDefaultAsync(m => m.CaseId == id);
            if (Cases == null)
            {
                return RedirectToPage("/Members/LogIn");
            }           
                ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", Cases.MemberId);
                return View(Cases);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCase(int id, [Bind("CaseId,CaseName,Gender,Birthdate,Age,IdentificationNumber,IdentityType,CasePhone,FamilyPhone,FamilyName,City,District,Address,Relation,MemberId")] Case Cases)
        {

            if (id != Cases.CaseId)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(Cases);


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(Cases.CaseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }

            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", Cases.MemberId);
            return View(Cases);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Case)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
        private bool CaseExists(int id)
        {
            return _context.Cases.Any(e => e.CaseId == id);
        }

        public IActionResult CertiFication()
		{
			return View();
		}
		public IActionResult SignUp()
		{
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
            return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("MemberId,UserName,Password,MemberAvatar,MemberName,Gender,Email,HomePhone,MobilePhone,City,District,Address,CaseId")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(LogIn)); 
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", member.CaseId);
            return View(member);
        }

    }
	
}

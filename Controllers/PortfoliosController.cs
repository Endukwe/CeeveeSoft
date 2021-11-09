using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CeeveeSoftWebProj.Data;
using CeeveeSoftWebProj.Models;
using CeeveeSoftWebProj.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace CeeveeSoftWebProj.Controllers
{
    public class PortfoliosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PortfoliosController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Portfolios
        [Route("{FullName}/{Id}")]
        [Route("portfolios/index/{Id}")]
        public async Task<IActionResult> Index(int? Id)
        {
            //Eagerloading of portfolio table with skills,education and experience nav properties
            var portfolio = await _context.Portfolios
                .Include(i => i.Skills)
                .Include(i => i.Experiences)
                .Include(i => i.Educations)
                .FirstOrDefaultAsync(m => m.Id == Id);

            //Convert profilepicture byte[] to base64string 
            string Profilebase64 = Convert.ToBase64String(portfolio.ProfilePicture);
            string ImgDataURL = string.Format("data:image/png;base64,{0}", Profilebase64);
            
            //store profilepics in a viewbag for view display
            ViewBag.Profilepics = ImgDataURL;
            

            return View(portfolio);
        }

        [Authorize]
        public async Task<IActionResult> userAction()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(m => m.IdentityId == userId);
            if (portfolio == null)
            {
                return RedirectToAction("Create", "Portfolios");
            }

            return RedirectToAction("Details", "Portfolios", new { Id = portfolio.Id });

        }

        // Action method to download CV PDF in Index view
        public async Task<IActionResult> OnPostDownload(int? id)
        {
            var context = await _context.Portfolios.FirstOrDefaultAsync(m => m.Id == id);

            return new FileContentResult(context.CV, "application/pdf");

        }

        // GET: Portfolios/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? Id)
        {
           
            var portfolio = await _context.Portfolios
                .Include(i => i.Skills)
                .Include(i => i.Experiences)
                .Include(i => i.Educations)
                .FirstOrDefaultAsync(m => m.Id == Id);
            
            //Convert ProfilePicture byte[] to base64string
            string Profilebase64 = Convert.ToBase64String(portfolio.ProfilePicture);
            string ImgDataURL = string.Format("data:image/png;base64,{0}", Profilebase64);
            ViewBag.Profilepics = ImgDataURL;
            var Fname = portfolio.FirstName;
            var Lname = portfolio.LastName;
            var LinkId = portfolio.Id.ToString();
            //Link url with format {www.domainname/firstnamelastname/id}
            var Linkurl = Fname + Lname + "/" + LinkId; //add domain name later
            //Pass Url to viewbag
            ViewBag.url = Linkurl;

            return View(portfolio);
        }




        // GET: Portfolios/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(m => m.IdentityId == userId);
            if (portfolio != null)
            {
                return RedirectToAction("Details", "Portfolios", new { Id = portfolio.Id });
            }

            return View();
        }

        // POST: Portfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,IdentityId,EmailAddress,CVfile, Upload,Proffesion,PhoneNumber,ProfessionalSummary,Address,LinkdinHandle")] PortfolioViewModel portfolioView)

        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            portfolioView.IdentityId = userId;

           
            
            //validate model state via binding
            if (ModelState.IsValid)
            {

                //Create an instance of memorystream
                //and copy the Iformfile datatype profile picture to it
                using var memorystream = new MemoryStream();
                await portfolioView.Upload.CopyToAsync(memorystream);

                //Create an instance of memorystream
                //and copy the Iforfile datatype of CV(pdf) to it
                using var CvStream = new MemoryStream();
                await portfolioView.CVfile.CopyToAsync(CvStream);

                if (memorystream.Length < 2097152) // Check if Uploaded profile picture is less than 2mb
                {
                    var portfolio = new Portfolio
                    {
                        ProfilePicture = memorystream.ToArray(),
                        CV = CvStream.ToArray(), //convert to array
                        FirstName = portfolioView.FirstName,
                        LastName = portfolioView.LastName,
                        EmailAddress = portfolioView.EmailAddress,
                        PhoneNumber = portfolioView.PhoneNumber,
                        ProfessionalSummary = portfolioView.ProfessionalSummary,
                        Address = portfolioView.Address,
                        LinkdinHandle = portfolioView.LinkdinHandle,
                        Proffesion = portfolioView.Proffesion,
                        IdentityId = portfolioView.IdentityId,

                    };
                    _context.Add(portfolio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create","Skills", new { Id = portfolio.Id }); // redirect 


                }
            }

            return View();
        }

        // GET: Portfolios/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            var ViewModel = new PortfolioViewModel()
            {
                Id = portfolio.Id,
                FirstName = portfolio.FirstName,
                LastName = portfolio.LastName,
                EmailAddress = portfolio.EmailAddress,
                PhoneNumber = portfolio.PhoneNumber,
                ProfessionalSummary = portfolio.ProfessionalSummary,
                Address = portfolio.Address,
                LinkdinHandle = portfolio.LinkdinHandle,
                Proffesion = portfolio.Proffesion
                
            };
            return View(ViewModel);
        }

        // POST: Portfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,IdentityId,EmailAddress,proffesion,CVfile, Upload,PhoneNumber,ProfessionalSummary,Skills,Education,Experience,Address,LinkdinHandle")] PortfolioViewModel portfolioView)

        {
            if (id != portfolioView.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            portfolioView.IdentityId = userId;

            var portfolio = await _context.Portfolios.FindAsync(id);


            if (ModelState.IsValid)
            {

                using var memorystream = new MemoryStream();
                await portfolioView.Upload.CopyToAsync(memorystream);
                using var CvStream = new MemoryStream();
                await portfolioView.CVfile.CopyToAsync(CvStream);

                try
                {
                    if (memorystream.Length < 2097152)
                    {

                        portfolio.ProfilePicture = memorystream.ToArray();
                        portfolio.CV = CvStream.ToArray();
                        portfolio.LastName = portfolioView.LastName;
                        portfolio.FirstName = portfolioView.FirstName;
                        
                        portfolio.Address = portfolioView.Address;
                        
                        portfolio.EmailAddress = portfolioView.EmailAddress;
                        portfolio.LinkdinHandle = portfolioView.LinkdinHandle;
                        portfolio.PhoneNumber = portfolioView.PhoneNumber;
                        portfolio.ProfessionalSummary = portfolioView.ProfessionalSummary;
                        portfolio.Proffesion = portfolioView.Proffesion;
                        portfolio.IdentityId = portfolioView.IdentityId;

                        _context.Update(portfolio);
                        await _context.SaveChangesAsync();


                    };
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioExists(portfolio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit","Skills", new { id = portfolio.Id });
            }
            return View(portfolio);
        }

    

    // GET: Portfolios/Delete/5
    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // POST: Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.Id == id);
        }
    }
}

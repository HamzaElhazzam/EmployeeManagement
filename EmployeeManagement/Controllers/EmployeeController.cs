using EmployeeManagement.Models;
using EmployeeManagement.Models.Repository;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly ICompanyRepository<Employee> _companyRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmployeeController(ICompanyRepository<Employee> companyRepository , IHostingEnvironment hostingEnvironment)
        {
            this._companyRepository = companyRepository;
            this._hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
           
            if (id is null)
                RedirectToAction("index");
            Employee Employee = _companyRepository.Get(id ?? 1);
            if (Employee is null)
                return View("ErrorView", id);
            return View(Employee);
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //IEnumerable<Employee> employees = _companyRepository.GetEntities();
            return View(_companyRepository.GetEntities());
        }
        [HttpPost]
        public ActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                    string path = Path.Combine(uploadsFolder, uniqueFileName);
                    //katched photo originale katlo7eha f dak lpath (kat creayih)
                    using (var x = new FileStream(path, FileMode.Create))
                    {
                        model.Photo.CopyTo(x);
                    }

                }
                Employee emp = new Employee()
                {
                    Departement = model.Departement,
                    Email = model.Email,
                    Name = model.Name,
                    PhotoPath = uniqueFileName,

                };
                _companyRepository.Add(emp);
                return RedirectToAction("details", new { id = emp.Id });
            }
            return View();

        }
        [Authorize(Roles = "Head Admin")]

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Employee emp = _companyRepository.Get(id);
            if(emp is null)
            {
                return View("ErrorView", id);
            }
            Employee employee = _companyRepository.Get(id);
            EmployeeEditViewModel model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Departement = employee.Departement,
                Email = employee.Email,
                PhotoPath = employee.PhotoPath
                
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee emp = _companyRepository.Get(model.Id);
                emp.Departement = model.Departement;
                emp.Name = model.Name;
                emp.Email = model.Email;

                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    //creation dyal path dyal image ou sevegarde dyalha f wwwroot
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                    string path = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var x = new FileStream(path, FileMode.Create))
                    {
                        model.Photo.CopyTo(x);
                    }
                    //hna ghadi nsupprimi tof lqdima donc rah dèja sift f model dyal update walakin drto hidden bash nstekhdmo f update bash nsuprimi mno image lqdima
                    // khassni ndkhel lhad lpath wwwroow/Images/img.png
                    //qbel khassni nverifi waqsh endo image wla la zeama matkun endo li dik par default
                    if (emp.PhotoPath != null)
                    {
                        string oldphotoEmployee  = Path.Combine(_hostingEnvironment.WebRootPath, "Images", emp.PhotoPath);
                        System.IO.File.Delete(oldphotoEmployee);
                        //men bead sir affecti Photopath jdida L Employee
                    }
                    emp.PhotoPath = uniqueFileName;
                }
                
                _companyRepository.Update(emp);
                return RedirectToAction("details", new { id = emp.Id });
            }
            return View();
            

        }
        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public ActionResult Delete(int id)
        {

            Employee emp = _companyRepository.Get(id);
            if (emp is null)
            {
                return View("ErrorView", id);
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult Delete(Employee emp)
        {

            if (emp is null)
            {
                return View("ErrorView", emp.Id);
            }

            _companyRepository.Delete(emp.Id);
            return RedirectToAction("index","Employee");
        }
        [AllowAnonymous]
        public ActionResult Acceuil()
        {
            return View();
        }
    }
}

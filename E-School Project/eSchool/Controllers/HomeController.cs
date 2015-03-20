using eSchool.Model;
using eSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eSchool.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeService _EmployeeService;
        //
        // GET: /Employee/

        public HomeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }
        public ActionResult Index()
        {
            return View(_EmployeeService.GetAll());
        }
        // GET: /Employee/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeService.GetById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.Create(employee);
                return RedirectToAction("Index");
            }

            
            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeService.GetById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            
            return View(employee);
        }

        // POST: /Employee/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Employee employee)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.Update(employee);
                return RedirectToAction("Index");
            }
            
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeService.GetById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Employee employee = _EmployeeService.GetById(id);
            _EmployeeService.Delete(employee);
            return RedirectToAction("Index");
        }

       

	}
}
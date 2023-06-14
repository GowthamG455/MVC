using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace curdopeartionmvc.Controllers
{
    public class HomeController : Controller
    {
      
    
    
        [HttpGet]
        public ActionResult Datagridview()
        {
            using (var context = new mvcappEntities())
            {
                var data = context.employees.ToList();
                return View(data);
            }
        }
        public ActionResult Create()
        {
            return View();
        }       

        [HttpPost]
        public ActionResult Create(employee model)
        {

            // To open a connection to the database
            using (var context = new mvcappEntities())
            {
                // Add data to the particular table
                context.employees.Add(model);

                // save the changes
                context.SaveChanges();
            }
            // string message = "Created the record successfully";
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int employeeId)
        {
            using (var context = new mvcappEntities())
            {
                var data = context.employees.FirstOrDefault(x=>x.EmployeeId == employeeId);
                if (data != null)
                {
                    context.employees.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Datagridview");
                }
                else
                    return View();
            }
        }

        public ActionResult Edit(int employeeId)
        {
            using (var context = new mvcappEntities())
            {
                var data = context.employees.Where(x => x.EmployeeId == employeeId).SingleOrDefault();
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit(int employeeId,employee model)
        {
            using (var context = new mvcappEntities())
            {

                // Use of lambda expression to access
                // particular record from a database
                var data = context.employees.FirstOrDefault(x => x.EmployeeId == employeeId);

                // Checking if any such record exist
                if (data != null)
                {

                    data.EmployeeId = model.EmployeeId;
                    data.Fristname = model.Fristname;
                    data.Lastname = model.Lastname;
                    data.Mobilenumber = model.Mobilenumber;
                    data.Gender= model.Gender;
                    data.DateofJoining = model.DateofJoining;
                    data.DateOfBrith = model.DateOfBrith;
                    data.Qualification = model.Qualification;
                    data.Department = model.Department;
                    context.SaveChanges();

                    // It will redirect to
                    // the Read method
                    return RedirectToAction("Datagridview");
                }
                else
                    return View();
            }
        }

        [HttpGet]
        public ActionResult mobilenumber(string mobilenumber)
        {
            if (mobilenumber != null && mobilenumber != "")
            {
                using (var context = new mvcappEntities())
                {
                    var data = context.employees.Where(x => x.Mobilenumber.Contains(mobilenumber)).ToList();
                    return View(data);
                }
            }
            else
            {
                return View();
            }
        }




    }

}
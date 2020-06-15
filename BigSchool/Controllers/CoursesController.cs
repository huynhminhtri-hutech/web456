using BigSchool.Models;
using BigSchool.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Models.ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new Models.ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }   
            var course = new Course
            {
                //Categories = _dbContext.Categories.ToList()
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryID = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            //  return View(viewModel);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModel);
        }
    }
}
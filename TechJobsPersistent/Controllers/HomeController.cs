﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            AddJobViewModel viewModel = new AddJobViewModel(employers, skills);
            return View(viewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel viewModel, string[] selectedSkills)
        {

            if (ModelState.IsValid)
            {
                Employer tempEmployer = context.Employers.Find(viewModel.EmployerId);
                Job job = new Job { Name = viewModel.Name, Employer = tempEmployer };

                foreach (string skill in selectedSkills)
                {
                    JobSkill jobSkill = new JobSkill
                    {
                        Job = job,
                        SkillId = Convert.ToInt32(skill)
                    };

                    context.JobSkills.Add(jobSkill);
                }
                context.Jobs.Add(job);
                context.SaveChanges();
                return Redirect("/Home/");
            }

            return View("AddJob", viewModel);
        }


        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}

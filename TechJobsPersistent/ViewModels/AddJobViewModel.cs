using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public List<SelectListItem> Employers { get; set; }
        public int SkillId { get; set; }
        public List<SelectListItem> Skills { get; set; }

        public AddJobViewModel()
        {

        }

        public AddJobViewModel(List<Employer> list, List<Skill> listSkill)
        {
            Employers = new List<SelectListItem>();
            Skills = new List<SelectListItem>();

            foreach (Employer employer in list)
            {
                Employers.Add(new SelectListItem { Value = employer.Id.ToString(), Text = employer.Name});
            }

            foreach (Skill skill in listSkill)
            {
                Skills.Add(new SelectListItem { Value = skill.Id.ToString(), Text = skill.Name});
            }
        }
    }
}

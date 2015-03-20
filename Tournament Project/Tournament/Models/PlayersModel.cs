using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTournament.Models
{
    public class PlayersModel
    {

        public int PID { get; set; }

        [Required]
        [Display(Name="Player Name")]
        public string Pname { get; set; }
        [Required]
        [Display(Name="Email Address")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string PEmailID { get; set; }
        [Required]
        [Display(Name = "Employee ID(IS123)")]
        public string EmpID { get; set; }
        [Display(Name = "Phone")]
        public string PPhone { get; set; }
        [Display(Name = "Gender")]
        public string gender { get; set; }
        [Display(Name = "DOB")]
        public string DOB { get; set; }
         [Required]
         [Display(Name = "Skill")] 
        public int SkillID { get; set; }
    }
}
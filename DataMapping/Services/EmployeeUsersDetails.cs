using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataMapping.Services
{
    public class EmployeeUsersDetails
    {
        public int EmployeeId { set; get; }
        public int UserId { get; set; }
        public bool isLocked { get; set; }
        [Required(ErrorMessage = "Please enter user name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the password")]
        [Display(Name = "Password")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("PassWord", ErrorMessage = "Must be the same")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassWord { get; set; }
        public string Email { set; get; }
        public string Phone1 { set; get; }
        public string Phone2 { get; set; }
        public string Address { set; get; }
        [Range(0, Double.MaxValue, ErrorMessage = "Enter a Positive number")]
        [DefaultValue(0)]
        public double CurrentSalary { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Enter a Positive number")]
        [DefaultValue(0)]
        public double PreviousSalary { get; set; }
        public int LastIncrementPercentage { get; set; }
        [Display(Name = "Image")]
        public string ImgURL { get; set; }
        [Display(Name = "Birth Date")]

        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public TimeSpan WorkDuration { get; set; }
        public int day { set; get; }
        public int Month { set; get; }
        public int year { set; get; }
        public int NumberOfDaysInMonth { set; get; }

        public int RoleId { get; set; }
        public string RoleDisplayName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Image { get; set; }
        public int VacationDays { get; set; }
    }
}

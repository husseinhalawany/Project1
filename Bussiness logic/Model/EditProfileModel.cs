using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class EditProfileModel
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone1 { set; get; }
        public string Phone2 { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public double CurrentSalary { get; set; }
        public double PreviousSalary { get; set; }
        public int LastIncrementPercentage { get; set; }
        [Display(Name = "Image")]
        public string ImgURL { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public int day { set; get; }
        public int Month { set; get; }
        public int year { set; get; }
        public int NumberOfDaysInMonth { set; get; }
        public string Image { get; set; }
    }
}

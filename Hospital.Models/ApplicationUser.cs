﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name  { get; set; }
        public Gender Gender { get; set; }

        public string Nationality { get; set; }

        public string Adress { get; set; }

        public DateTime DOB { get; set; }

        public string Specialist { get; set; }
        public Department Department { get; set; }

        [NotMapped]
        public ICollection<Payroll> payrolls { get; set; }
        [NotMapped]
        public ICollection<Appointment> Appointments { get; set; }

    }
}

namespace Hospital.Models
{
    public enum Gender
    {
        Male,Female
    }
}
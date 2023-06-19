﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.ViewModels;
using Hospital.Utilities;
using Hospital.Repositories;

namespace Hospital.Services
{
    public interface IHospitalInfo 
    {
        PageResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize);

        HospitalInfoViewModel GetHospitalById(int HospitalId);
        void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void DeleteHospitalInfo(int id);

    }
}

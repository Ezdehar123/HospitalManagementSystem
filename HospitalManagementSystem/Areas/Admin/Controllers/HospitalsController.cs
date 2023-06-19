﻿using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hospital.ViewModels;
using Hospital.Repositories;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")] 
    public class HospitalsController : Controller
    {
        
        private IHospitalInfo _hospitalInfo;  

        public HospitalsController(IHospitalInfo hospitalInfo)
        {

            _hospitalInfo = hospitalInfo;
        }
        public IActionResult Index(int pageNumber , int pageSize=10)
        {
            return View(_hospitalInfo.GetAll(pageNumber,pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id) {
        var viewModel = _hospitalInfo.GetHospitalById(id);
            return View(viewModel);
        }

        [HttpPost]

        public IActionResult Edit(HospitalInfoViewModel vm) {
        _hospitalInfo.UpdateHospitalInfo(vm);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(HospitalInfoViewModel vm) 
        {
            _hospitalInfo.InsertHospitalInfo(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) 
        { 
            _hospitalInfo.DeleteHospitalInfo(id);
            return RedirectToAction("Index");
        }
    }
}
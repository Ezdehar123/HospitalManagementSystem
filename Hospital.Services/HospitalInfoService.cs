﻿using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;
using Hospital.Models;
using Hospital.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Hospital.Services
{
    public class HospitalInfoService : IHospitalInfo   


    {

        private IUnitOfWork _unitOfWork;

        public HospitalInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteHospitalInfo(int id)
        {

            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetByID(id);  
            _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
            _unitOfWork.Save();
        }

        public PageResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new HospitalInfoViewModel();
            int totalCount;
            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                
                var modelList = _unitOfWork.GenericRepository<HospitalInfo>()
                    .GetAll()
                    .Skip(ExcludeRecords)
                    .Take(pageSize)
                    .ToList();
                totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }

            catch (Exception)
            {
                throw;
            }
            var result = new PageResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public HospitalInfoViewModel GetHospitalById(int HospitalId)
        {
            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetByID(HospitalId);
            var vm = new HospitalInfoViewModel(model);
            return vm;
        }

        public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
            _unitOfWork.Save();
            
        }

        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
           var ModelById= _unitOfWork.GenericRepository<HospitalInfo>().GetByID(model.Id);
            ModelById.Name = hospitalInfo.Name;
            ModelById.City = hospitalInfo.City;
            ModelById.PinCode = hospitalInfo.PinCode;
            ModelById.Country = hospitalInfo.Country;
            _unitOfWork.GenericRepository<HospitalInfo>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList) 
        { 



            return modelList.Select(x => new HospitalInfoViewModel(x)).ToList();
        }
    }
}
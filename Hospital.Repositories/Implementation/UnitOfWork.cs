using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicatopnDbContext _context;

        public UnitOfWork(ApplicatopnDbContext context)
        {
            _context = context;
        }
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }


            }
            this.disposed = true;
        }


        public IGenericRepository<T> genericRepository<T>() where T : class
        {
            IGenericRepository<T> repository = new Genericrepository<T>(_context);
            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
    }


using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PetShop2023DBContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(PetShop2023DBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public GenericRepository()
        {
            _dbContext = new PetShop2023DBContext();
            _dbSet = _dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public IEnumerable<T> GetAll(int pageNumber, int pageSize)
        {
            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((double)_dbSet.Count() / pageSize);
        }
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public void Insert(T entity)
        {
            if (_dbSet.Contains(entity))
                throw new Exception("Entity already exists in the data"); 
            else
                _dbSet.Add(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (_dbSet.Contains(entity))
                _dbSet.Update(entity);
            else
                throw new Exception("Entity does not exist in the data");
        }
        public void Delete(T entity)
        {
            if (_dbSet.Contains(entity))
                _dbSet.Remove(entity);
            else
                throw new Exception("Entity does not exist in the data"); 
        }
    }
}

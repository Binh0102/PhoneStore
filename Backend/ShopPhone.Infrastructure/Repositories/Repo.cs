using Microsoft.EntityFrameworkCore;
using ShopPhone.Domain.Repositories;
using ShopPhone.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Infrastructure.Repositories
{
    public class Repo<T>:IRepo<T> where T : class, new()
    {
        private readonly ShopDbContext _context;
        DbSet<T> _dbSet;
        public Repo(ShopDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T Get(int id)
        {
            return _dbSet.Find(id);
        }
        public bool Add(T entity)
        {
            if(!_dbSet.Any(e => e ==entity))
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(T entity)
        {
            if(!_dbSet.Any(e => e == entity))
            {
                return false;
            }
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }
        public bool Delete(int id)
        {
            var product = _dbSet.Find(id);
            if(product == null)
            {
                return false;
            }
            _dbSet.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}

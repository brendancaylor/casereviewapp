using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CaseReview.DataLayer.Models;

namespace CaseReview.DataLayer
{
    public class BaseService<T> where T : class
    {
        public BaseService()
        {
        
        }

        public ICollection<T> GetAll()
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Set<T>().ToList();
            }
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return await _context.Set<T>().ToListAsync();
            }
        }

        public T Get(Guid id)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Set<T>().Find(id);
            }
        }

        public T Get(int id)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Set<T>().Find(id);
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return await _context.Set<T>().FindAsync(id);
            }
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Set<T>().SingleOrDefault(match);
            }
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return await _context.Set<T>().SingleOrDefaultAsync(match);
            }
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Set<T>().Where(match).ToList();
            }
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return await _context.Set<T>().Where(match).ToListAsync();
            }
        }

        public T Add(T t)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                _context.Set<T>().Add(t);
                _context.SaveChanges();
                return t;
            }
        }

        public async Task<T> AddAsync(T t)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                _context.Set<T>().Add(t);
                await _context.SaveChangesAsync();
                return t;
            }
        }

        public T Update(T updated)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                
                if (updated == null)
                    return null;

                _context.Entry(updated).State = EntityState.Modified;
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return null;
                }

                return updated;
            }
        }

        public async Task<T> UpdateAsync(T updated, Guid key)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                if (updated == null)
                    return null;

                _context.Entry(updated).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                /*
                T existing = await _context.Set<T>().FindAsync(key);
                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(updated);
                    await _context.SaveChangesAsync();
                }
                 */
                return updated;
            }
        }

        public void Delete(Guid id)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                _context.Set<T>().Remove(_context.Set<T>().Find(id));
                _context.SaveChanges();
            }
        }

        public async Task<int> DeleteAsync(T t)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                _context.Set<T>().Remove(t);
                return await _context.SaveChangesAsync();
            }
        }

        public int Count()
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Set<T>().Count();
            }
        }

        public int Count(Expression<Func<T, bool>> match)
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return _context.Set<T>().Where(match).Count();
            }
        }


        public async Task<int> CountAsync()
        {
            using (CaseReviewsContext _context = new CaseReviewsContext())
            {
                return await _context.Set<T>().CountAsync();
            }
        }
    }
}

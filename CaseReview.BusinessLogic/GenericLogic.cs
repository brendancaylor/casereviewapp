using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CaseReview.DataLayer;
using CaseReview.DataLayer.Models;

namespace CaseReview.BusinessLogic
{
    public class GenericLogic<T> where T : class
    {
        private BaseService<T> bda = new BaseService<T>();

        public ICollection<T> GetAll()
        {
            return bda.GetAll().ToList();
        }

        public T Get(Guid id)
        {
            return bda.Get(id);
        }

        public T Get(int id)
        {
            return bda.Get(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return bda.Find(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return bda.FindAll(match);
        }

        public T Add(T t)
        {
            return bda.Add(t);
        }

        public async Task<T> AddAsync(T t)
        {
            await bda.AddAsync(t);
            return t;
        }

        public T Update(T updated)
        {
            return bda.Update(updated);
        }

        public void Delete(Guid id)
        {
            bda.Delete(id);
        }

        public int Count()
        {
            return bda.Count();
        }

        public int Count(Expression<Func<T, bool>> match)
        {
            return bda.Count(match);
        }

        public void UpdateReorder(List<T> items)
        {
            foreach (var item in items)
            {
                IBaseDomainModel model = (IBaseDomainModel)(object)item;
                var dbItem = (IBaseDomainModel)bda.Get(model.ID);
                dbItem.DisplayOrder = items.IndexOf(item);
                bda.Update((T)dbItem);
            }
        }

        public void UpdateActive(T item)
        {
            IBaseDomainModel model = (IBaseDomainModel)(object)item;
            var dbItem = (IBaseDomainModel)bda.Get(model.ID);
            dbItem.IsActive = model.IsActive;
            bda.Update((T)dbItem);
        }
    }
}

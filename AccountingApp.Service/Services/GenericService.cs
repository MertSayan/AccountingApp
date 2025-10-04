﻿using AccountingApp.Core.Models;
using AccountingApp.Core.Repositories;
using AccountingApp.Core.Services;
using AccountingApp.Core.UnitOfWorks;
using System.Linq.Expressions;

namespace AccountingApp.Service.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWorks _unitOfWorks;

        public GenericService(IGenericRepository<T> repository, IUnitOfWorks unitOfWorks)
        {
            _repository = repository;
            _unitOfWorks = unitOfWorks;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;

            await _repository.AddAsync(entity);

            await _unitOfWorks.CommitAsync();

            return entity;
        } 

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public void ChangeStatus(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = false;
            _repository.ChangeStatus(entity);
            _unitOfWorks.Commit();
        }

        public int Count()
        {
            return _repository.Count();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _unitOfWorks.Commit();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}

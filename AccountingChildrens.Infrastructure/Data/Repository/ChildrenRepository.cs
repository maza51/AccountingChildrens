using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccountingChildrens.Infrastructure.Data.Repository
{
    public class ChildrenRepository : IChildrenRepository
    {
        private AppDbContext _dBContext;

        public ChildrenRepository(AppDbContext dBContext)
        {
            _dBContext = dBContext;
        }

        public void Create(Children entity)
        {
            _dBContext.Add(entity);
        }

        public void Delete(Children entity)
        {
            _dBContext.Remove(entity);
        }

        public async Task<List<Children>> GetAllAsync()
        {
            return await _dBContext.Childrens
                .Include(x => x.ChildrenGroups)
                .ToListAsync();
        }

        public async Task<Children> GetByIdAsync(int id)
        {
            return await _dBContext.Childrens
                .Include(x => x.ChildrenGroups)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dBContext.SaveChangesAsync();
        }

        public void Update(Children entity)
        {
            _dBContext.Update(entity);
        }
    }
}


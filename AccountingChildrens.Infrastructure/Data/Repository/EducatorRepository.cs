using System;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccountingChildrens.Infrastructure.Data.Repository
{
    public class EducatorRepository : IEducatorRepository
    {
        private AppDbContext _dBContext;

        public EducatorRepository(AppDbContext dBContext)
        {
            _dBContext = dBContext;
        }

        public void Create(Educator entity)
        {
            _dBContext.Add(entity);
        }

        public void Delete(Educator entity)
        {
            _dBContext.Remove(entity);
        }

        public async Task<List<Educator>> GetAllAsync()
        {
            return await _dBContext.Educators
                .Include(x => x.EducatorGroups)
                .ToListAsync();
        }

        public async Task<Educator> GetByIdAsync(int id)
        {
            return await _dBContext.Educators
                .Include(x => x.EducatorGroups)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dBContext.SaveChangesAsync();
        }

        public void Update(Educator entity)
        {
            _dBContext.Update(entity);
        }
    }
}


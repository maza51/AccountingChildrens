using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccountingChildrens.Infrastructure.Data.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private AppDbContext _dBContext;

        public GroupRepository(AppDbContext dBContext)
        {
            _dBContext = dBContext;
        }

        public void Create(Group entity)
        {
            _dBContext.Add(entity);
        }

        public void Delete(Group entity)
        {
            _dBContext.Remove(entity);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _dBContext.Groups
                .Include(x => x.ChildrenGroups)
                .Include(x => x.EducatorGroups)
                .ToListAsync();
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _dBContext.Groups
                .Include(x => x.ChildrenGroups)
                .Include(x => x.EducatorGroups)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dBContext.SaveChangesAsync();
        }

        public void Update(Group entity)
        {
            _dBContext.Update(entity);
        }
    }
}


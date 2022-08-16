using System;
using AccountingChildrens.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingChildrens.Application.DTOs;

namespace AccountingChildrens.Application.Interfases
{
    public interface IGroupService
    {
        Task<List<GroupDTO>> GetAllAsync();
        Task<GroupDTO> GetByIdAsync(int id);
        Task CreateAsync(GroupDTO groupDTO);
        Task DeleteAsync(int id);
    }
}


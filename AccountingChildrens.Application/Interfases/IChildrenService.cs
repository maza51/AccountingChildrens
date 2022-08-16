using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingChildrens.Application.DTOs;
using AccountingChildrens.Domain.Entities;

namespace AccountingChildrens.Application.Interfases
{
    public interface IChildrenService
    {
        Task<List<ChildrenDTO>> GetAllAsync();
        Task<ChildrenDTO> GetByIdAsync(int id);
        Task CreateAsync(ChildrenDTO childrenDTO);
        Task DeleteAsync(int id);
        Task CreateChildrenGroupAsync(int childrenId, int groupId);
        Task DeleteChildrenGroupAsync(int childrenId, int groupId);
    }
}


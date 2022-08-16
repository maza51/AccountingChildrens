using System;
using AccountingChildrens.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingChildrens.Application.Interfases
{
    public interface IEducatorService
    {
        Task<List<EducatorDTO>> GetAllAsync();
        Task<EducatorDTO> GetByIdAsync(int id);
        Task CreateAsync(EducatorDTO educatorDTO);
        Task DeleteAsync(int id);
        Task CreateEducatorGroupAsync(int educatorId, int groupId);
        Task DeleteEducatorGroupAsync(int educatorId, int groupId);
    }
}


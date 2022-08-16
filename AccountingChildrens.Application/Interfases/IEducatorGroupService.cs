using System;
using System.Threading.Tasks;

namespace AccountingChildrens.Application.Interfases
{
    public interface IEducatorGroupService
    {
        Task CreateEducatorGroupAsync(int educatorId, int groupId);
        Task DeleteEducatorGroupAsync(int educatorId, int groupId);
    }
}


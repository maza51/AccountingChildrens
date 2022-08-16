using System;
using System.Threading.Tasks;

namespace AccountingChildrens.Application.Interfases
{
    public interface IChildrenGroupService
    {
        Task CreateChildrenGroupAsync(int childrenId, int groupId);
        Task DeleteChildrenGroupAsync(int childrenId, int groupId);
    }
}


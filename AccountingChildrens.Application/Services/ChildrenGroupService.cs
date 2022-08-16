using System;
using System.Linq;
using System.Threading.Tasks;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;

namespace AccountingChildrens.Application.Services
{
    public class ChildrenGroupService : IChildrenGroupService
    {
        IChildrenRepository _childrenRepository;
        IGroupRepository _groupRepository;

        public ChildrenGroupService(IChildrenRepository childrenRepository, IGroupRepository groupRepository)
        {
            _childrenRepository = childrenRepository;
            _groupRepository = groupRepository;
        }

        public async Task CreateChildrenGroupAsync(int childrenId, int groupId)
        {
            var childrenInDb = await _childrenRepository.GetByIdAsync(childrenId);
            if (childrenInDb == null)
                throw new ApplicationException($"children with id {childrenId} does not exist");

            var groupInDb = await _groupRepository.GetByIdAsync(groupId);
            if (groupInDb == null)
                throw new ApplicationException($"group with id {groupId} does not exist");

            var childrenGroup = childrenInDb.ChildrenGroups.FirstOrDefault(x => x.GroupId == groupId);
            if (childrenGroup != null)
                throw new ApplicationException($"childrenGroup with children id {childrenId} and group id {groupId} already exists");

            childrenInDb.ChildrenGroups.Add(new ChildrenGroup
            {
                ChildrenId = childrenId,
                GroupId = groupId,
                DateAdded = DateTime.Now
            });
            await _childrenRepository.SaveAsync();
        }

        public async Task DeleteChildrenGroupAsync(int childrenId, int groupId)
        {
            var childrenInDb = await _childrenRepository.GetByIdAsync(childrenId);
            if (childrenInDb == null)
                throw new ApplicationException($"children with id {childrenId} does not exist");

            var childrenGroup = childrenInDb.ChildrenGroups.FirstOrDefault(x => x.GroupId == groupId);
            if (childrenGroup == null)
                throw new ApplicationException($"childrenGroup with children id {childrenId} and group id {groupId} already exists");

            childrenInDb.ChildrenGroups.Remove(childrenGroup);
            await _childrenRepository.SaveAsync();
        }
    }
}


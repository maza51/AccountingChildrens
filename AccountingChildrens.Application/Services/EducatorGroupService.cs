using System;
using System.Linq;
using System.Threading.Tasks;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;

namespace AccountingChildrens.Application.Services
{
    public class EducatorGroupService : IEducatorGroupService
    {
        IEducatorRepository _educatorRepository;
        IGroupRepository _groupRepository;

        public EducatorGroupService(IEducatorRepository educatorRepository, IGroupRepository groupRepository)
        {
            _educatorRepository = educatorRepository;
            _groupRepository = groupRepository;
        }

        public async Task CreateEducatorGroupAsync(int educatorId, int groupId)
        {
            var educatorInDb = await _educatorRepository.GetByIdAsync(educatorId);
            if (educatorInDb == null)
                throw new ApplicationException($"educator with id {educatorId} does not exist");

            var groupInDb = await _groupRepository.GetByIdAsync(groupId);
            if (groupInDb == null)
                throw new ApplicationException($"group with id {groupId} does not exist");

            var educatorGroup = educatorInDb.EducatorGroups.FirstOrDefault(x => x.GroupId == groupId);
            if (educatorGroup != null)
                throw new ApplicationException($"educatorGroup with educator id {educatorId} and group id {groupId} already exists");

            educatorInDb.EducatorGroups.Add(new EducatorGroup
            {
                EducatorId = educatorId,
                GroupId = groupId,
                DateAdded = DateTime.Now
            });
            await _educatorRepository.SaveAsync();
        }

        public async Task DeleteEducatorGroupAsync(int educatorId, int groupId)
        {
            var educatorInDb = await _educatorRepository.GetByIdAsync(educatorId);
            if (educatorInDb == null)
                throw new ApplicationException($"educator with id {educatorId} does not exist");

            var educatorGroup = educatorInDb.EducatorGroups.FirstOrDefault(x => x.GroupId == groupId);
            if (educatorGroup == null)
                throw new ApplicationException($"educatorGroup with educator id {educatorId} and group id {groupId} already exists");

            educatorInDb.EducatorGroups.Remove(educatorGroup);
            await _educatorRepository.SaveAsync();
        }
    }
}


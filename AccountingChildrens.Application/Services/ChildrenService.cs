using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccountingChildrens.Application.DTOs;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Domain;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;
using AutoMapper;

namespace AccountingChildrens.Application.Services
{
    public class ChildrenService : IChildrenService
    {
        private IChildrenRepository _childrenRepository;
        private IGroupRepository _groupRepository;
        private IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ChildrenService(IChildrenRepository childrenRepository, IGroupRepository groupRepository, IMapper mapper, ILoggerManager logger)
        {
            _childrenRepository = childrenRepository;
            _groupRepository = groupRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateAsync(ChildrenDTO childrenDTO)
        {
            ValidateChildrenDTO(childrenDTO);

            var childrenInDB = await _childrenRepository.GetByIdAsync(childrenDTO.Id);
            if (childrenInDB != null)
                throw new ApplicationException($"children with id {childrenDTO.Id} already exists");

            var children = _mapper.Map<Children>(childrenDTO);

            _childrenRepository.Create(children);
            await _childrenRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var children = await _childrenRepository.GetByIdAsync(id);
            if (children == null)
                throw new ApplicationException($"children with id {id} does not exist");

            _childrenRepository.Delete(children);
            await _childrenRepository.SaveAsync();
        }

        public async Task<List<ChildrenDTO>> GetAllAsync()
        {
            var childrens = await _childrenRepository.GetAllAsync();
            var childrensDTO = _mapper.Map<List<ChildrenDTO>>(childrens);
            return childrensDTO;
        }

        public async Task<ChildrenDTO> GetByIdAsync(int id)
        {
            var children = await _childrenRepository.GetByIdAsync(id);
            var childrenDTO = _mapper.Map<ChildrenDTO>(children);
            return childrenDTO;
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

        private void ValidateChildrenDTO(ChildrenDTO childrenDTO)
        {
            if (childrenDTO == null)
                throw new ApplicationException($"children is null");

            if (childrenDTO.FirstName.Length <= 3)
                throw new ApplicationException($"children firstname must be more than 3 characters long");

            if (childrenDTO.LastName.Length <= 3)
                throw new ApplicationException($"children lastname must be more than 3 characters long");

            if (childrenDTO.Age < 1)
                throw new ApplicationException($"children age must be more than 0 age");
        }
    }
}


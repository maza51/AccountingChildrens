using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingChildrens.Application.DTOs;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Domain;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;
using AutoMapper;

namespace AccountingChildrens.Application.Services
{
    public class GroupService : IGroupService
    {
        private IGroupRepository _groupRepository;
        private IChildrenRepository _childrenRepository;
        private IEducatorRepository _educatorRepository;
        private IMapper _mapper;
        private readonly ILoggerManager _logger;

        public GroupService(IGroupRepository groupRepository, IChildrenRepository childrenRepository, IEducatorRepository educatorRepository, IMapper mapper, ILoggerManager logger)
        {
            _groupRepository = groupRepository;
            _childrenRepository = childrenRepository;
            _educatorRepository = educatorRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateAsync(GroupDTO groupDTO)
        {
            var groupInDB = await _groupRepository.GetByIdAsync(groupDTO.Id);
            if (groupInDB != null)
            {
                _logger.LogInfo($"group with id {groupDTO.Id} already exists");
                return;
            }

            var group = _mapper.Map<Group>(groupDTO);

            _groupRepository.Create(group);
            await _groupRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group == null)
            {
                _logger.LogInfo($"group with id {id} does not exist");
                return;
            }

            _groupRepository.Delete(group);
            await _groupRepository.SaveAsync();
        }

        public async Task<List<GroupDTO>> GetAllAsync()
        {
            var groups = await _groupRepository.GetAllAsync();
            var groupsDTO = _mapper.Map<List<GroupDTO>>(groups);
            return groupsDTO;
        }

        public async Task<GroupDTO> GetByIdAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            var groupDTO = _mapper.Map<GroupDTO>(group);
            return groupDTO;
        }

        private void ValidateGroupDTO(GroupDTO groupDTO)
        {
            if (groupDTO == null)
                throw new ApplicationException($"group is null");
        }
    }
}


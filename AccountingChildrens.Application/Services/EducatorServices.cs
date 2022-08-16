using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingChildrens.Application.DTOs;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Domain;
using AccountingChildrens.Domain.Entities;
using AccountingChildrens.Domain.Repositories;
using AutoMapper;

namespace AccountingChildrens.Application.Services
{
    public class EducatorServices : IEducatorService
    {
        private IEducatorRepository _educatorRepository;
        private IGroupRepository _groupRepository;
        private IMapper _mapper;
        private readonly ILoggerManager _logger;

        public EducatorServices(IEducatorRepository educatorRepository, IGroupRepository groupRepository, IMapper mapper, ILoggerManager logger)
        {
            _educatorRepository = educatorRepository;
            _groupRepository = groupRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateAsync(EducatorDTO educatorDTO)
        {
            ValidateEducatorDTO(educatorDTO);

            var educatorInDB = await _educatorRepository.GetByIdAsync(educatorDTO.Id);
            if (educatorInDB != null)
                throw new ApplicationException($"educator with id {educatorDTO.Id} already exists");

            var educator = _mapper.Map<Educator>(educatorDTO);

            _educatorRepository.Create(educator);
            await _educatorRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var educator = await _educatorRepository.GetByIdAsync(id);
            if (educator == null)
                throw new ApplicationException($"educator with id {id} does not exist");

            _educatorRepository.Delete(educator);
            await _educatorRepository.SaveAsync();
        }

        public async Task<List<EducatorDTO>> GetAllAsync()
        {
            var educators = await _educatorRepository.GetAllAsync();
            var educatorsDTO = _mapper.Map<List<EducatorDTO>>(educators);
            return educatorsDTO;
        }

        public async Task<EducatorDTO> GetByIdAsync(int id)
        {
            var educator = await _educatorRepository.GetByIdAsync(id);
            var educatorDTO = _mapper.Map<EducatorDTO>(educator);
            return educatorDTO;
        }

        public Task CreateEducatorGroupAsync(int educatorId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEducatorGroupAsync(int educatorId, int groupId)
        {
            throw new NotImplementedException();
        }

        private void ValidateEducatorDTO(EducatorDTO educatorDTO)
        {
            if (educatorDTO == null)
                throw new ApplicationException($"educator is null");

            if (educatorDTO.FirstName.Length <= 3)
                throw new ApplicationException($"educator firstname must be more than 3 characters long");

            if (educatorDTO.LastName.Length <= 3)
                throw new ApplicationException($"educator lastname must be more than 3 characters long");
        }
    }
}


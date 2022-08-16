using System;
using AccountingChildrens.Domain.Entities;
using System.Collections.Generic;

namespace AccountingChildrens.Application.DTOs
{
    public class EducatorDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<EducatorGroupDTO> EducatorGroups { get; set; }
    }
}


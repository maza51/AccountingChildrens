using System;
using AccountingChildrens.Domain.Entities;
using System.Collections.Generic;

namespace AccountingChildrens.Application.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ChildrenGroupDTO> ChildrenGroups { get; set; }

        public List<EducatorGroupDTO> EducatorGroups { get; set; }
    }
}


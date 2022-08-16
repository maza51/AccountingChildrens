using System;
using AccountingChildrens.Domain.Entities;
using System.Collections.Generic;

namespace AccountingChildrens.Application.DTOs
{
    public class ChildrenDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public List<ChildrenGroupDTO> ChildrenGroups { get; set; }
    }
}


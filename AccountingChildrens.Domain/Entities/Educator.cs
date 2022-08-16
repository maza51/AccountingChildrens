using System;
using System.Collections.Generic;

namespace AccountingChildrens.Domain.Entities
{
    public class Educator
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<EducatorGroup> EducatorGroups { get; set; }
    }
}


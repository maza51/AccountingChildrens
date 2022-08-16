using System;
using System.Collections.Generic;

namespace AccountingChildrens.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ChildrenGroup> ChildrenGroups { get; set; }

        public List<EducatorGroup> EducatorGroups { get; set; }
    }
}


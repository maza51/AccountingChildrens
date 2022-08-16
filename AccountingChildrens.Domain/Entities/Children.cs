using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingChildrens.Domain.Entities
{
    public class Children
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public List<ChildrenGroup> ChildrenGroups { get; set; }
    }
}


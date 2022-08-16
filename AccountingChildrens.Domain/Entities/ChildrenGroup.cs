using System;
namespace AccountingChildrens.Domain.Entities
{
    public class ChildrenGroup
    {
        public int ChildrenId { get; set; }

        public int GroupId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
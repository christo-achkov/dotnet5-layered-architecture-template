using System;
using System.ComponentModel.DataAnnotations;

namespace AAR.Model.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

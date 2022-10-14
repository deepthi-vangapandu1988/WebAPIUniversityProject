using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication4.Data;

namespace WebApplication4.Data
{
    public partial class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<Student>? Students { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Api.Models
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        [Column("emp_no")]
        public int EmpNo { get; set; }

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("gender")]
        public char Gender { get; set; }

        [Required]
        [Column("hire_date")]
        public DateTime HireDate { get; set; }
    }
}

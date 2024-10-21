namespace Diem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }

        [Required]
        [StringLength(270)]
        public string FullName { get; set; }

        public double ĐTB { get; set; }

        public int? FacultyID { get; set; }

        public int? MajorID { get; set; }

        [StringLength(270)]
        public string Avatar { get; set; }
    }
}

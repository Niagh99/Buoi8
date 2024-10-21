namespace Diem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Major")]
    public partial class Major
    {
        public int FacultyID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MajorID { get; set; }

        [Required]
        [StringLength(270)]
        public string MajorName { get; set; }
    }
}

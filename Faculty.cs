namespace Diem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faculty")]
    public partial class Faculty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacultyID { get; set; }

        [StringLength(270)]
        public string FacultyName { get; set; }
    }
}

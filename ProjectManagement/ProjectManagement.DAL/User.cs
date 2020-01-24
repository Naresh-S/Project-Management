namespace ProjectManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        public int User_ID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public int? Employee_ID { get; set; }

        public int? Project_ID { get; set; }

        public int? Task_ID { get; set; }

        public virtual Project Project { get; set; }

        public virtual Task Task { get; set; }
    }
}

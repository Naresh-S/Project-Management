namespace ProjectManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Users = new HashSet<User>();

        }

        [Key]
        public int Task_ID { get; set; }

        public int? Parent_ID { get; set; }

        public int? Project_ID { get; set; }

        public string TaskName { get; set; }

        public DateTime? Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        
        public int Priority { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public int? User_ID { get; set; }
        public virtual ParentTask ParentTask { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}

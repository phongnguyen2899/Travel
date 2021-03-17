namespace EF.Entiti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfor")]
    public partial class UserInfor
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? RoleID { get; set; }

        public virtual Role Role { get; set; }
    }
}

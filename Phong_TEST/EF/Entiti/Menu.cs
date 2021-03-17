namespace EF.Entiti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            Articles = new HashSet<Article>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string NameMenu { get; set; }

        [StringLength(250)]
        public string Location { get; set; }

        public int? Level { get; set; }

        public bool? Status { get; set; }

        public bool? isActive { get; set; }

        public string Decriptions { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        public string MetaDecriptions { get; set; }

        public int? ParenID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
    }
}

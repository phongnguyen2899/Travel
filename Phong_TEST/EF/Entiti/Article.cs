namespace EF.Entiti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string NameArticle { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public int? MenuID { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Content { get; set; }

        public bool? Status { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsShowHome { get; set; }

        public string Decriptions { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        public string MetaDecription { get; set; }

        public virtual Menu Menu { get; set; }
    }
}

namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chapter")]
    public partial class Chapter
    {
        public int ChapterId { get; set; }

        [StringLength(50)]
        public string ChapterName { get; set; }

        [StringLength(250)]
        public string ShortContent { get; set; }

        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}

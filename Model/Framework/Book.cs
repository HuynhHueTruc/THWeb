namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            Chapters = new HashSet<Chapter>();
        }
        public int BookId { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống!")]

        [StringLength(150)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tên tác giả không được để trống!")]

        [StringLength(150)]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Giá sách không được để trống!")]

        public double? Price { get; set; }

        [Required(ErrorMessage = "Năm xuất bản sách không được để trống!")]

        public int? Year { get; set; }

        [Required(ErrorMessage = "Hãy chọn ảnh!")]

        [StringLength(150)]
        public string CoverPage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}

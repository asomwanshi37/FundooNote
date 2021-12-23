using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModel
{
    public class NotesModel
    {
        [Key]
        public int NotesId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public string Remainder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Is_Archive { get; set; }
        public bool Is_Trash { get; set; }
        public bool Is_Pin { get; set; }
        [Required]
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    public class YearTerm
    {
        [Key]
        public int YearTermId { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        [UIHint("_TermDropDown")]
        [Required(ErrorMessage = "Term is required.")]
        public int Term { get; set; }

        [Required]
        [Display(Name = "Default Term")]
        public bool IsDefault { get; set; }

        //public List<Choice> Choices { get; set; }
    }
}

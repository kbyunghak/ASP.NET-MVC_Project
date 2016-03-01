using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Min length 2 characters"),MaxLength(50, ErrorMessage ="Max legth 50 characters")]
        [Display(Name = "Option Name")]
        public string Title { get; set; }

        [Required]
        [Display (Name ="Active")]
        public bool IsActive { get; set; }

       //public List<Choice> Choices { get; set; }
    }
}

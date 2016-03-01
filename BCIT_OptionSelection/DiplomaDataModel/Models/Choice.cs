using DiplomaDataModel.Models.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    [OptionsValidation()]
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        [Display(Name = "Term Year:")]
        public int YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        [Display(Name = "Term Year:")]
        public virtual YearTerm YearTerms { get; set; }

        [Required(ErrorMessage = "Student Number is required")]
        [StringLength(9, ErrorMessage = "Max length 9 characters")]
        [RegularExpression("A[0-9]{8}", ErrorMessage = "Must be A00000000 Form")]
        [Display(Name = "Student Id:")]
        public string StudentId { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max length 40 characters")]
        [Display(Name = "First Name:")]
        public string StudentFirstName { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Max length 40 characters")]
        [Display(Name = "Last Name:")]
        public string StudentLastName { get; set; }

        [Required]
        [Display(Name = "First Choice:")]
        [ForeignKey("FirstOption")]
        [UIHint("_OptionsDropDown")]      
        public int FirstChoiceOptionId { get; set; }

        [Required]
        [Display(Name = "Second Choice:")]
        [ForeignKey("SecondOption")]
        [UIHint("_OptionsDropDown")]
        public int SecondChoiceOptionId { get; set; }

        [Required]
        [Display(Name = "Third Choice:")]
        [ForeignKey("ThirdOption")]
        [UIHint("_OptionsDropDown")]
        public int ThirdChoiceOptionId { get; set; }

        [Required]
        [Display(Name = "Fourth Choice:")]
        [ForeignKey("FourthOption")]
        [UIHint("_OptionsDropDown")]
        public int FourthChoiceOptionId { get; set; }

        [ForeignKey("FirstOptionId")]
        [Display(Name = "First Choice:")]
        public virtual Option FirstOption { get; set; }

        [ForeignKey("SecondOptionId")]
        [Display(Name = "Second Choice:")]
        public virtual Option SecondOption { get; set; }

        [ForeignKey("ThirdOptionId")]
        [Display(Name = "Third Choice:")]
        public virtual Option ThirdOption { get; set; }

        [ForeignKey("FourthOptionId")]
        [Display(Name = "Fourth Choice:")]
        public virtual Option FourthOption { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Date Created is required.")]
        [Display(Name = "Date Selected:")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SelecionDate { get; set; }

        //public YearTerm YearTerm { get; set; }
        //public List<Option> Options { get; set; }

    }
}

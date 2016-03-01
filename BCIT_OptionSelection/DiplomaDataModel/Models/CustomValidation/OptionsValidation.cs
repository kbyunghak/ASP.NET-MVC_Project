using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models.CustomValidation
{
    public class OptionsValidation : ValidationAttribute
    {
        protected HashSet<int> optSet;
        public OptionsValidation() { optSet = new HashSet<int>(); }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            if (value == null)
            {
                return new ValidationResult("Options are empty");
            }
            Choice curChoice = validationContext.ObjectInstance as Choice;
            List<int> choices = new List<int>()
            {
                curChoice.FirstChoiceOptionId,
                curChoice.SecondChoiceOptionId,
                curChoice.ThirdChoiceOptionId,
                curChoice.FourthChoiceOptionId,
            };
            foreach(int c in choices)
            {
                if (!optSet.Contains(c)) {
                    //Hashset does NOT already contain the option
                    optSet.Add(c);
                } else {
                    //Hashset already contains the choice, Invalid input!
                    optSet.Clear();                    
                    return new ValidationResult("Cannot have duplicate options");
                } 
            }
            optSet.Clear();
            return ValidationResult.Success;
        }
    }
}

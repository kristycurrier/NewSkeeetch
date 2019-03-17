using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skeeetch.Models
{
    public class SearchTerms
    {
        [Required(ErrorMessage = "Try Again, you must enter what you want to do!")]
        public int[] Terms { get; set; }

        [Required(ErrorMessage = "Please enter a price!")]
        public int Price { get; set; }

        [Required(ErrorMessage = "How adventurous are you?")]
        public string Adventure { get; set; }

        [Required(ErrorMessage = "Please enter valid city")]
        [Range(3,50, ErrorMessage ="Please enter valid city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter valid state!")]
        [StringLength(2, ErrorMessage ="Please enter a valid state abbreviation!")]
        [RegularExpression("^(?:(A[KLRZ]|C[AOT]|D[CE]|FL|GA|HI|I[ADLN]|K[SY]|LA|M[ADEINOST]|N[CDEHJMVY]|O[HKR]|P[AR]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY]))$")]
        public string State { get; set; }
    }
}
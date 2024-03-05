using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpedizioniNazionali
{
    public class checkStatoSpedizione : ValidationAttribute
    {
        public string Situation { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] Situations = Situation.ToString().Split(',');
            if (Situations.Contains(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Lo Stato della Spedizione può essere solo In Transito, In Consegna, Consegnato o Non Consegnato.");
            }
        }
    }
}
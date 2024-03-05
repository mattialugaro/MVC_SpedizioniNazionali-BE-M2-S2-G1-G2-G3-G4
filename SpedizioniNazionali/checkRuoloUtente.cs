using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpedizioniNazionali
{
    public class checkRuoloUtente : ValidationAttribute
    {
        public string Kind { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] Kinds = Kind.ToString().Split(',');
            if(Kinds.Contains(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Il ruolo dell'Utente può essere solo Utente, Admin o SuperAdmin.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpedizioniNazionali
{
    public class checkTipoCliente : ValidationAttribute
    {
        public string Type { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] Types = Type.ToString().Split(',');
            if(Types.Contains(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Il Tipo di Cliente può essere solo Privato o Azienda.");
            }
        }
    }
}
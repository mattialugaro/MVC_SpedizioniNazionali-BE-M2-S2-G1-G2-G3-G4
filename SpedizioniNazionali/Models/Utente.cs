using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpedizioniNazionali.Models
{
    public class Utente
    {
        [ScaffoldColumn(false)]
        public int IDUtente { get; set; }

        [Required(ErrorMessage = "Il Nome è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Nome non può avere più di 50 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il Cognome è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Cognome non può avere più di 50 caratteri")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "L' Email è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "L' Email non può avere più di 50 caratteri")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lo Username è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Lo Username non può avere più di 50 caratteri")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La Password è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "La Password non può avere più di 50 caratteri")]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [checkRuoloUtente(Kind = "Utente,Admin,SuperAdmin", ErrorMessage = "Il ruolo dell'Utente può essere solo Utente, Admin o SuperAdmin.")]
        public string Ruolo {  get; set; }

    }
}
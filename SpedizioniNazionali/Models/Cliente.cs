using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpedizioniNazionali.Models
{
    public class Cliente
    {
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "Il Nome è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Nome non può avere più di 50 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il Cognome è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Cognome non può avere più di 50 caratteri")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "L' Email è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "L' Email non può avere più di 50 caratteri")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il Tipo di Cliente è un campo obbligatorio")]
        [StringLength(7, ErrorMessage = "Il Tipo di Cliente non può avere più di 7 caratteri")]
        [checkTipoCliente(Type = "Privato,Azienda", ErrorMessage = "Il Tipo di Cliente può essere solo Privato o Azienda.")]
        [DisplayName("Tipo di Cliente (Privato o Azienda)")]
        public string TipoCliente { get; set; }

        [StringLength(16, ErrorMessage = "Il Codice Fiscale non può avere più di 16 caratteri")]
        [DisplayName("Codice Fiscale (Solo per i Privati)")]
        public string CodiceFiscale {  get; set; }

        [StringLength(11, ErrorMessage = "La Partita Iva non può avere più di 11 caratteri")]
        [DisplayName("Partita Iva (Solo per le Aziende)")]
        public string PIva {  get; set; }

    }
}
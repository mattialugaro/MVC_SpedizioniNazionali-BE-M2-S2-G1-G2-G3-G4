using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpedizioniNazionali.Models
{
    public class AggiornamentoSpedizione
    {
        [ScaffoldColumn(false)]
        public int IDAggiornamentoSpedizione {  get; set; }

        [DisplayName("Stato della Spedizione")]
        [Required(ErrorMessage = "Lo Stato della Spedizione è un campo obbligatorio")]
        [StringLength(15, ErrorMessage = "Lo Stato della Spedizione non può avere più di 15 caratteri")]
        [checkStatoSpedizione(Situation = "In Transito,In Consegna,Consegnato,Non Consegnato", ErrorMessage = "Lo Stato della Spedizione può essere solo In Transito, In Consegna, Consegnato o Non Consegnato.")]
        public string StatoSpedizione { get; set; }

        [DisplayName("Luogo dove si trova il Pacco")]
        [StringLength(50, ErrorMessage = "Il Luogo dove si trova il Pacco non può avere più di 50 caratteri")]
        [Required(ErrorMessage = "Il Luogo dove si trova il Pacco è un campo obbligatorio")]
        public string LuogoPacco { get; set; }

        [StringLength(100, ErrorMessage = "La Descrizione non può avere più di 100 caratteri")]
        public string Descrizione { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [DisplayName("Aggiornamento Data e Ora della Spedizione")]
        [Required(ErrorMessage = "L' Aggiornamento Data e Ora della Spedizione è un campo obbligatorio")]
        public DateTime DataOraAggiornamento { get; set; }

        [DisplayName("L' ID della Spedizione")]
        [Required(ErrorMessage = "L' ID della Spedizione è un campo obbligatorio")]
        public int IDSpedizione { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SpedizioniNazionali.Models
{
    public class Spedizione
    {
        public int IDSpedizione { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Data della Spedizione (AAAA-MM-GG)")]
        [Required(ErrorMessage = "La Data della Spedizione è un campo obbligatorio")]
        public DateTime DataSpedizione { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DisplayName("Peso della Spedizione")]
        [Required(ErrorMessage = "Peso della Spedizione è un campo obbligatorio")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Città di Destinazione è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "La Città di Destinazione non può avere più di 50 caratteri")]
        [DisplayName("Città di Destinazione")]
        public string CittaDestinataria { get; set; }

        [Required(ErrorMessage = "L' Indirizzo di Consegna è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "L' Indirizzo di Consegna non può avere più di 50 caratteri")]
        [DisplayName("Indirizzo di Consegna")]
        public string IndirizzoConsegna { get; set; }

        [Required(ErrorMessage = "Il Nome Completo del Destinatario è un campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Nome Completo del Destinatario non può avere più di 50 caratteri")]
        [DisplayName("Nome Completo del Destinatario")]
        public string NomeDestinatario { get; set; }

        [Required(ErrorMessage = "Il Costo di Spedizione è un campo obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DisplayName("Costo di Spedizione")]
        public decimal CostoSpedizione { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Data di Conmsegna Prevista (AAAA-MM-GG)")]
        [Required(ErrorMessage = "La Data di Conmsegna Prevista è un campo obbligatorio")]
        public DateTime DataConsegnaPrevista { get; set; }

        [DisplayName("L' ID del Cliente")]
        [Required(ErrorMessage = "L' ID del Cliente è un campo obbligatorio")]
        public int IDCliente { get; set; }

    }
}
using SpedizioniNazionali.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpedizioniNazionali.Controllers
{
    public class AggiornamentoSpedizioneController : Controller
    {
        // GET: AggiornamentoSpedizione
        public ActionResult Index(int id)
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(int id, Spedizione s)
        //{
        //    string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
        //    SqlConnection conn = new SqlConnection(connectionstring);
        //    try
        //    {
        //        conn.Open();
        //        string query = "UPDATE AggiornamentoSpedizione SET StatoSpedizione = @StatoSpedizione, LuogoPacco = @LuogoPacco, Descrizione = @Descrizione, DataOraAggiornamento = DateTime.Now() WHERE IDSpedizione = @IDSpedizione;";

        //        SqlCommand cmd = new SqlCommand(query, conn);

        //        cmd.Parameters.AddWithValue("@StatoSpedizione", s.StatoSpedizione);
        //        cmd.Parameters.AddWithValue("@LuogoPacco", s.LuogoPacco);
        //        cmd.Parameters.AddWithValue("@Descrizione", s.Descrizione);
        //        cmd.Parameters.AddWithValue("DataOraAggiornamento", s.DateTime.Now());

        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return View();
        //}
    }

}
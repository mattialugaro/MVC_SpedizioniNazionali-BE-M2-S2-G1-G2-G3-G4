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
    public class SpedizioneController : Controller
    {
        // GET: Spedizione
        public ActionResult Index()
        {
            List<Spedizione> listaSpedizioni = new List<Spedizione>();
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "SELECT * FROM Spedizione";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Spedizione s = new Spedizione();
                    s.IDSpedizione = Convert.ToInt16(reader["IDSpedizione"]);
                    s.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                    s.Peso = Convert.ToDecimal(reader["Peso"]);
                    s.CittaDestinataria = reader["CittaDestinataria"].ToString();
                    s.IndirizzoConsegna = reader["IndirizzoConsegna"].ToString();
                    s.NomeDestinatario = reader["NomeDestinatario"].ToString();
                    s.CostoSpedizione = Convert.ToDecimal(reader["CostoSpedizione"]);
                    s.DataConsegnaPrevista = Convert.ToDateTime(reader["DataConsegnaPrevista"]);
                    s.IDCliente = Convert.ToInt16(reader["IDCliente"]);
                    listaSpedizioni.Add(s);
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(listaSpedizioni);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Spedizione s)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "INSET INTO Spedizione(DataSpedizione, Peso, CittaDestinataria, IndirizzoConsegna, NomeDestinatario, CostoSpedizione, DataConsegnaPrevista, IDCliente) VALUES(@DataSpedizione, @Peso, @CittaDestinataria, @IndirizzoConsegna, @NomeDestinatario, @CostoSpedizione, @DataConsegnaPrevista, @IDCliente)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@DataSpedizione", s.DataSpedizione);
                cmd.Parameters.AddWithValue("@Peso", s.Peso);
                cmd.Parameters.AddWithValue("@CittaDestinataria", s.CittaDestinataria);
                cmd.Parameters.AddWithValue("@IndirizzoConsegna", s.IndirizzoConsegna);
                cmd.Parameters.AddWithValue("@NomeDestinatario", s.NomeDestinatario);
                cmd.Parameters.AddWithValue("@CostoSpedizione", s.CostoSpedizione);
                cmd.Parameters.AddWithValue("@DataConsegnaPrevista", s.DataConsegnaPrevista);
                cmd.Parameters.AddWithValue("@IDCliente", s.IDCliente);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }
    }
}
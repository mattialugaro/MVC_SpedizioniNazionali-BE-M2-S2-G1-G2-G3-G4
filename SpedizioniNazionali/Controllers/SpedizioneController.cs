using SpedizioniNazionali.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SpedizioniNazionali.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
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
                string query = "INSERT INTO Spedizione(DataSpedizione, Peso, CittaDestinataria, IndirizzoConsegna, NomeDestinatario, CostoSpedizione, DataConsegnaPrevista) VALUES(@DataSpedizione, @Peso, @CittaDestinataria, @IndirizzoConsegna, @NomeDestinatario, @CostoSpedizione, @DataConsegnaPrevista)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@DataSpedizione", s.DataSpedizione);
                cmd.Parameters.AddWithValue("@Peso", s.Peso);
                cmd.Parameters.AddWithValue("@CittaDestinataria", s.CittaDestinataria);
                cmd.Parameters.AddWithValue("@IndirizzoConsegna", s.IndirizzoConsegna);
                cmd.Parameters.AddWithValue("@NomeDestinatario", s.NomeDestinatario);
                cmd.Parameters.AddWithValue("@CostoSpedizione", s.CostoSpedizione);
                cmd.Parameters.AddWithValue("@DataConsegnaPrevista", s.DataConsegnaPrevista);
                cmd.ExecuteNonQuery();

                try
                {
                    string query2 = "INSERT INTO AggiornamentoSpedizione(StatoSpedizione, LuogoPacco, Descrizione, DataOraAggiornamento, IDSpedizione) VALUES (@StatoSpedizione, @LuogoPacco, @Descrizione, @DataOraAggiornamento, @IDSpedizione)";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);

                    cmd2.Parameters.AddWithValue("@StatoSpedizione", "In Transito");
                    cmd2.Parameters.AddWithValue("@LuogoPacco", "Bologna");
                    cmd2.Parameters.AddWithValue("@Descrizione", "Il pacco è stato appena spedito");
                    cmd2.Parameters.AddWithValue("@DataOraAggiornamento", DateTime.Now);
                    cmd2.Parameters.AddWithValue("@IDSpedizione", s.IDSpedizione);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
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

            return RedirectToAction("Index");
        }

        public ActionResult CercaSpedizione()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CercaSpedizione(AggiornamentoSpedizione a)
        {
            return RedirectToAction("ControllaSpedizione", a);
        }

        [AllowAnonymous]
        public ActionResult ControllaSpedizione()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult ControllaSpedizione(AggiornamentoSpedizione a)
        {
            Spedizione s = new Spedizione();
            List<AggiornamentoSpedizione> ListaAggiornamenti = new List<AggiornamentoSpedizione>();

            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "SELECT IDSpedizione, DataSpedizione, Peso, CittaDestinataria, IndirizzoConsegna, NomeDestinatario, CostoSpedizione, DataConsegnaPrevista FROM Spedizione AS S " +
                                "INNER JOIN Cliente AS C ON S.IDCliente = C.IDCliente WHERE IDSpedizione = @IDSpedizione AND PIva = @PIva OR CodiceFiscale = @CodiceFiscale";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@IDSpedizione", a.IDSpedizione);
                cmd.Parameters.AddWithValue("@CodiceFiscale", a.CodiceFiscale);
                cmd.Parameters.AddWithValue("@PIva", a.CodiceFiscale);
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    s.IDSpedizione = Convert.ToInt16(reader["IDSpedizione"]);
                    s.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                    s.Peso = Convert.ToDecimal(reader["Peso"]);
                    s.CittaDestinataria = reader["CittaDestinataria"].ToString();
                    s.IndirizzoConsegna = reader["IndirizzoConsegna"].ToString();
                    s.NomeDestinatario = reader["NomeDestinatario"].ToString();
                    s.CostoSpedizione = Convert.ToDecimal(reader["CostoSpedizione"]);
                    s.DataConsegnaPrevista = Convert.ToDateTime(reader["DataConsegnaPrevista"]);
                    s.IDCliente = Convert.ToInt16(reader["IDCliente"]);
                }

                query = "SELECT A.IDSpedizione, IDAggiornamentoSpedizione, StatoSpedizione, LuogoPacco, Descrizione, DataOraAggiornamento FROM Spedizione AS S " +
                        "INNER JOIN AggiornamentoSpedizione AS A ON A.IDSpedizione = S.IDSpedizione " +
                        "INNER JOIN Cliente AS C ON S.IDCliente = C.IDCliente " +
                        "WHERE S.IDSpedizione = @IDSpedizione AND PIva = @PIva OR CodiceFiscale = @CodiceFiscale";

                SqlCommand cmd2 = new SqlCommand(query, conn);

                cmd2.Parameters.AddWithValue("@IDSpedizione", a.IDSpedizione);
                cmd2.Parameters.AddWithValue("@CodiceFiscale", a.CodiceFiscale);
                cmd2.Parameters.AddWithValue("@PIva", a.CodiceFiscale);
                cmd2.ExecuteNonQuery();

                SqlDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    AggiornamentoSpedizione agg = new AggiornamentoSpedizione();
                    agg.IDSpedizione = Convert.ToInt16(reader2["IDSpedizione"]);
                    agg.IDAggiornamentoSpedizione = Convert.ToInt16(reader2["IDAggiornamentoSpedizione"]);
                    agg.StatoSpedizione = reader2["StatoSpedizione"].ToString();
                    agg.LuogoPacco = reader2["LuogoPacco"].ToString();
                    agg.Descrizione = reader2["Descrizione"].ToString();
                    agg.DataOraAggiornamento = Convert.ToDateTime(reader2["DataOraAggiornamento"]);
                    agg.Spedizione = s;
                    ListaAggiornamenti.Add(agg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                Response.Write(ListaAggiornamenti);
                conn.Close();
            }

            return View(ListaAggiornamenti);
        }
    }
}
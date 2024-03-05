using SpedizioniNazionali.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace SpedizioniNazionali.Controllers
{
    public class UtenteController : Controller
    {
        // GET: Utente
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Index()
        {
            List<Utente> listaUtenti = new List<Utente>();
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "SELECT * FROM Utente";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Utente u = new Utente();
                    u.IDUtente = Convert.ToInt16(reader["IDUtente"]);
                    u.Nome = reader["Nome"].ToString();
                    u.Cognome = reader["Cognome"].ToString();
                    u.Email = reader["Email"].ToString();
                    u.Username = reader["Username"].ToString();
                    u.Password = reader["Password"].ToString();
                    u.Ruolo = reader["Ruolo"].ToString();
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

            return View(listaUtenti);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Utente u)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "SELECT * FROM Utente WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("Username", u.Username);
                cmd.Parameters.AddWithValue("Password", u.Password);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    conn.Close();
                    ViewBag.Error = "Autenticazione non riuscita, riprovare";
                    return View();

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

            return View();
        }

        public ActionResult Registrazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrazione(Utente u)
        {
            
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "INSERT INTO Utente(Nome, Cognome, Email, Username, Password, Ruolo) VALUES(@Nome, @Cognome, @Email, @Userrname, @Password, Utente)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Nome", u.Nome);
                cmd.Parameters.AddWithValue("@Cognome", u.Cognome);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Username", u.Username);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                cmd.Parameters.AddWithValue("@Ruolo", u.Ruolo = "Utente");
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

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
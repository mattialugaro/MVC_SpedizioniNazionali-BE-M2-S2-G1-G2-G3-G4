﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpedizioniNazionali.Models
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            List<Cliente> listaClienti = new List<Cliente>();
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "SELECT * FROM Cliente";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente c = new Cliente();
                    c.IDCliente = Convert.ToInt16(reader["IDCliente"]);
                    c.Nome = reader["Nome"].ToString();
                    c.Cognome = reader["Cognome"].ToString();
                    c.Email = reader["Email"].ToString();
                    c.TipoCliente = reader["TipoCliente"].ToString();
                    c.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    c.PIva = reader["PIva"].ToString();
                    listaClienti.Add(c);
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

            return View(listaClienti);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente c)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string query = "INSERT INTO Cliente(Nome, Cognome, Email, TipoCliente, PIva, CodiceFiscale) VALUES(@Nome, @Cognome, @Email, @TipoCliente, @PIva, @CodiceFiscale)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@Cognome", c.Cognome);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@TipoCliente", c.TipoCliente);

                cmd.Parameters.AddWithValue("@PIva", c.TipoCliente == "Azienda" ? c.PIva : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CodiceFiscale", c.TipoCliente != "Azienda" ? c.CodiceFiscale : (object)DBNull.Value);

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
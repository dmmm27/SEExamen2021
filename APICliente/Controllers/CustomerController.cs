
using APICliente.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace APICliente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : Controller
    {
        

        string connectionString = "Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = SEExamen2021; Data Source = localhost";
        
        // GET: CustomerController
        [HttpGet(Name ="GetCustomer")]
        public ActionResult Index()
        {
           try
            {
                List<ClientesBD> lstCliente = new List<ClientesBD>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_te_clientes", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@w_estado", "S");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ClientesBD cli = new ClientesBD();
                        cli.cli_codigo_cliente = (int)dr["cli_codigo_cliente"];
                        cli.cli_nombre1 = dr["cli_nombre1"].ToString();
                        cli.cli_nombre2 = dr["cli_nombre2"].ToString();
                        cli.cli_apellido1 = dr["cli_apellido1"].ToString();
                        cli.cli_apellido2 = Convert.IsDBNull(dr["cli_apellido2"]) ? "" : (string?)dr["cli_apellido2"];
                        cli.cli_apellido_casada = Convert.IsDBNull(dr["cli_apellido_casada"]) ? "" : (string)dr["cli_apellido_casada"];
                        cli.cli_direccion = dr["cli_direccion"].ToString();
                        cli.cli_telefono1 = (int)dr["cli_telefono1"];
                        cli.cli_telefono2 = Convert.IsDBNull(dr["cli_telefono2"]) ? 0 : (int)dr["cli_telefono2"];
                        cli.cli_identificacion = dr["cli_identificacion"].ToString();
                        cli.cli_fecha_nacimiento = (DateTime)dr["cli_fecha_nacimiento"];
                        lstCliente.Add(cli);
                    }
                    return Ok(lstCliente);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult  Get(int id)
        {

           
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                ClientesBD cli = new ClientesBD();
                SqlCommand cmd = new SqlCommand("sp_te_clientes", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@w_estado", "P");

                cmd.Parameters.AddWithValue("@i_cli_codigo_cliente", id);
               
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    cli.cli_nombre1 = dr["cli_nombre1"].ToString();

                    cli.cli_apellido1 = dr["cli_apellido1"].ToString();


                }
                con.Close();
            }
            return Ok(id);
        }


        // POST: CustomerController/Create
        [HttpPost]
        public ActionResult Customer(ClientesBD cliente)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_te_clientes", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@w_estado", "I");
                   
                    cmd.Parameters.AddWithValue("@i_nombre1", cliente.cli_nombre1);
                    cmd.Parameters.AddWithValue("@i_nombre2", cliente.cli_nombre2);
                    cmd.Parameters.AddWithValue("@i_apellido1", cliente.cli_apellido1);
                    cmd.Parameters.AddWithValue("@i_apellido2", cliente.cli_apellido2);
                    cmd.Parameters.AddWithValue("@i_apellido_casada", cliente.cli_apellido_casada);
                    cmd.Parameters.AddWithValue("@i_direccion", cliente.cli_direccion);
                    cmd.Parameters.AddWithValue("@i_telefono1", cliente.cli_telefono1);
                    cmd.Parameters.AddWithValue("@i_telefono2", cliente.cli_telefono2);
                    cmd.Parameters.AddWithValue("@i_identificacion", cliente.cli_identificacion);
                    cmd.Parameters.AddWithValue("@i_fecha_nacimiento", cliente.cli_fecha_nacimiento);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return CreatedAtRoute("GetCustomer", new { id = cliente.cli_codigo_cliente }, cliente);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      


        // PUT api/Customer/5
        [HttpPut("{id}")]
        public ActionResult  Put(ClientesBD cliente)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_te_clientes", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@w_estado", "U");
                    cmd.Parameters.AddWithValue("@i_cli_codigo_cliente", cliente.cli_codigo_cliente);
                    cmd.Parameters.AddWithValue("@i_telefono1", cliente.cli_telefono1);
                    cmd.Parameters.AddWithValue("@i_direccion", cliente.cli_direccion);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }

            return Ok(cliente.cli_codigo_cliente);
        }


        // DELETE api/CustomerController/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_te_clientes", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@w_estado", "D");
                    cmd.Parameters.AddWithValue("@i_cli_codigo_cliente", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok(id);

        }


       

       
    }
}

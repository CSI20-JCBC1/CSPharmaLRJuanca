using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;

namespace CSPharmaLRJuanca.Pages.Shared.Login
{
    public class Login : PageModel
    {

        public string Cadena { get; set; }

        public void OnGet()
        {
        }

        
        [ActionName("MiLogin")]
        public void OnPostSubmit(DlkCatAccEmpleado empleado)
        {
            //Se recoge la información de la vista
            var connection = new NpgsqlConnection("Host=localhost;Port=5432;Pooling=true;Database=cspharma_informacional;UserId=postgres;Password=Juancarbc2001;");
            Console.WriteLine("Conexión base de datos abierta.");
            connection.Open();
            //Establecida la conexion con la base de datos


            NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"dlk_informacional\".\"dlk_cat_acc_empleados\" WHERE cod_empleado='{empleado.CodEmpleado}' AND clave_empleado='{empleado.ClaveEmpleado}'", connection);
            NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
            //Ejecutamos la query dentro de la base de datos


            if (resultadoConsulta.HasRows)
            {
                Console.WriteLine("Ha iniciado sesion con exito.");
                //La base de datos tiene el usuario y la contraseña que hemos introducido
                this.Cadena = string.Format("Ha iniciado sesión como {0}", empleado.CodEmpleado);
                
                
            }
            else
            {
                Console.WriteLine("Error en el inicio de sesión.");
                //La base de datos no tene el usuario que hemos introducido
                this.Cadena = string.Format("Usuario o contraseña no válidos, por favor intentelo de nuevo.");
            }

            Console.WriteLine("Cerrando conexion");
            //Conexión cerrada
            connection.Close();
            
        }

    }
}

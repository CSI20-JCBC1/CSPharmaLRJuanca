using DAL.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Data;

namespace CSPharmaLRJuanca.Pages.Register
{
    public class RegisterModel : PageModel
    {

        public string Cadena { get; set; }



        public void OnGet()
        {
        }

        [ActionName("MiRegister")]
        public void OnPostSubmit(DlkCatAccEmpleado empleado, string ValContrasenia)
        {

            ///Se recoge la información de la vista
            var connection = new NpgsqlConnection("Host=localhost;Port=5432;Pooling=true;Database=cspharma_informacional;UserId=postgres;Password=Juancarbc2001;");
            Console.WriteLine("Conexión base de datos abierta.");
            connection.Open();
            //Establecida la conexion con la base de datos

            DateTime dateTime = DateTime.Now;
            string fecha = dateTime.ToString("yyyy-MM-dd HH:mm:ss"), contrasenia=empleado.ClaveEmpleado;
            //Damos formato a la fecha

            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var mduuid = new String(Charsarr);
            Console.WriteLine(mduuid);
            //mduuid


            //Comprobamos que la contraseña introducida y la validacion coinciden        
            if (ValContrasenia == empleado.ClaveEmpleado)
            {
              
                if (empleado.CodEmpleado==null||contrasenia == null||ValContrasenia==null)
                {
                    Console.WriteLine(" Ninguno de los campos a introducir puede ser null");
                    this.Cadena = string.Format("Ninguno de los datos a introducir puede ser null.");
                    //Controlamos los errores al introducir valores nulos
                }
                else if ( contrasenia.Length >= 7)
                {
                    //Controlado el numero de caracteres de la contraseña

                    NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"dlk_informacional\".\"dlk_cat_acc_empleados\" WHERE cod_empleado='{empleado.CodEmpleado}'", connection);
                    NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
                    //Consulta hecha a la base de datos



                    if (resultadoConsulta.HasRows)
                    {
                        //La consulta obtienne resultado
                        Console.WriteLine("El usuario ya existe");
                        this.Cadena = string.Format("El usuario ya está registrado.");

                    }
                    else
                    {
                        //No hay resultados en la base de datos que coincidan con los datos introducidos
                        //Insertamos los valores para el registro
                        connection.Close();

                        connection.Open();
                        Console.WriteLine("Insertando usuario en la base de datos");
                        consulta = new NpgsqlCommand($"INSERT INTO \"dlk_informacional\".\"dlk_cat_acc_empleados\" VALUES ('{mduuid}', '{fecha}', '{empleado.CodEmpleado}', '{empleado.ClaveEmpleado}', 1);", connection);
                        //Pondremos por defecto nivel de acceso 1 y se lo cambiaremos en la base de datos
                        consulta.ExecuteNonQuery();
                        Console.WriteLine("Insert realizado con éxito");
                        this.Cadena = string.Format("Registro completado.");

                    }
                }
                else
                {
                    
                    Console.WriteLine("La contraseña no cumple los requisitos minimos");
                    this.Cadena = string.Format("Debe introducir una contraseña con almenos 7 caracteres.");

                }





            }
            else
            {
                Console.WriteLine("Las contraseñas no coinciden");
                this.Cadena = string.Format("Las contraseñas introducidas no coinciden");
            }

           

            Console.WriteLine("Cerrando conexion");
            connection.Close();
        }

    }
}

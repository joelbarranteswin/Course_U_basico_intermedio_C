using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CRUD_Database
{
    internal class ConexionMySQL
    {
        MySqlConnection conexion = new MySqlConnection();
        string CadenaConexion = "Server=localhost;Database=crud;User=root;Password=;";


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;

namespace CapaDatos
{
    public class D_Dashboard
    {
        SqlConnection Conexion = new SqlConnection("Server=DUSTIN; DataBase=PRACTICA_DASHBOARD; Integrated Security=true");
        SqlCommand cmd;
        SqlDataReader dr;

        public void ProdPorCategoria(E_Dashboard obj)
        {
            cmd = new SqlCommand("ProdPorCategoria4", Conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                obj.Categoria1.Add(dr.GetString(0));
                obj.CantProd1.Add(dr.GetInt32(1));
            }
            dr.Close();
            Conexion.Close();
        }

        public void ProdPreferidos(E_Dashboard obj)
        {
            cmd = new SqlCommand("ProdPreferidos2", Conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                obj.Producto1.Add(dr.GetString(0));
                obj.Cantidad1.Add(dr.GetInt32(1));
            }
            dr.Close();
            Conexion.Close();
        }
        public void SumarioDatos(E_Dashboard obj)
        {

            cmd = new SqlCommand("DashboardDatos", Conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter total = new SqlParameter("@totVentas", SqlDbType.Int);
            total.Direction = ParameterDirection.Output;

            SqlParameter nprod = new SqlParameter("@nprod", SqlDbType.Int);
            nprod.Direction = ParameterDirection.Output;

            SqlParameter nmarca = new SqlParameter("@nmarc", SqlDbType.Int);
            nmarca.Direction = ParameterDirection.Output;

            SqlParameter ncategoria = new SqlParameter("@ncateg", SqlDbType.Int);
            ncategoria.Direction = ParameterDirection.Output;

            SqlParameter ncliente = new SqlParameter("@nclient", SqlDbType.Int);
            ncliente.Direction = ParameterDirection.Output;

            SqlParameter nproveedores = new SqlParameter("@nprove", SqlDbType.Int);
            nproveedores.Direction = ParameterDirection.Output;

            SqlParameter nempleados = new SqlParameter("@nemple", SqlDbType.Int);
            nempleados.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(total);
            cmd.Parameters.Add(nprod);
            cmd.Parameters.Add(nmarca);
            cmd.Parameters.Add(ncategoria);
            cmd.Parameters.Add(ncliente);
            cmd.Parameters.Add(nproveedores);
            cmd.Parameters.Add(nempleados);
            Conexion.Open();
            cmd.ExecuteNonQuery();

            obj.TotalVentas = cmd.Parameters["@totVentas"].Value.ToString();
            obj.CantlProductos1 = cmd.Parameters["@nprod"].Value.ToString();
            obj.CantlMarcas1 = cmd.Parameters["@nmarc"].Value.ToString();
            obj.CantCategorias1 = cmd.Parameters["@ncateg"].Value.ToString();
            obj.CantlClientes1 = cmd.Parameters["@nclient"].Value.ToString();
            obj.CantlProveedores1 = cmd.Parameters["@nprove"].Value.ToString();
            obj.CantEmpleados1 = cmd.Parameters["@nemple"].Value.ToString();

            Conexion.Close();

        }
    }
}

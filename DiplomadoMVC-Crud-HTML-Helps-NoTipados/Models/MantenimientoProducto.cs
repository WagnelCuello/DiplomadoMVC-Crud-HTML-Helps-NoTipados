using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomadoMVC_Crud_HTML_Helps_NoTipados.Models
{
    public class MantenimientoProducto
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
            con = new SqlConnection(constr);
        }

        /// <summary>
        /// Agrega un nuevo producto
        /// </summary>
        /// <param name="prod">Producto recibido como argumento</param>
        /// <returns>numero entero que especifica si se guardó o no</returns>
        public int Agregar(Productos prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("INSERT INTO Producto(descripcion,precio) VALUES(@descripcion,@precio)",con);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@precio", SqlDbType.Float);

            comando.Parameters["@descripcion"].Value = prod.Descripcion;
            comando.Parameters["@precio"].Value = prod.Precio;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }

        /// <summary>
        /// Recupera todos los registros encontrados en la base de datos de productos
        /// </summary>
        /// <returns></returns>
        public List<Productos> RecuperarTodos()
        {
            Conectar();
            List<Productos> productos = new List<Productos>();

            SqlCommand com = new SqlCommand("SELECT codigo,descripcion,precio FROM Producto", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();

            while (registros.Read())
            {
                Productos prod = new Productos()
                {
                    Codigo = int.Parse(registros["codigo"].ToString()),
                    Descripcion = registros["descripcion"].ToString(),
                    Precio = float.Parse(registros["precio"].ToString())
                };
                productos.Add(prod);
            }

            con.Close();
            return productos;
        }

        /// <summary>
        /// Recupera un solo producto
        /// </summary>
        /// <param name="codigo">parametro pasado para buscar ese en especifico</param>
        /// <returns>producto con todas sus propiedades</returns>
        public Productos Recuperar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("SELECT codigo,descripcion,precio FROM Producto WHERE codigo=@codigo", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;

            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Productos producto = new Productos();

            if (registros.Read())
            {
                producto.Codigo = int.Parse(registros["codigo"].ToString());
                producto.Descripcion = registros["descripcion"].ToString();
                producto.Precio = float.Parse(registros["precio"].ToString());
            }
            else
                producto = null;

            con.Close();
            return producto;
        }

        /// <summary>
        /// Modifica un producto
        /// </summary>
        /// <param name="prod">objeto de tipo producto</param>
        /// <returns>numero entero para saber si se guardó o no</returns>
        public int Modificar(Productos prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("UPDATE Producto SET descripcion=@descripcion,precio=@precio WHERE codigo=@codigo", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@precio", SqlDbType.Float);

            comando.Parameters["@codigo"].Value = prod.Codigo;
            comando.Parameters["@descripcion"].Value = prod.Descripcion;
            comando.Parameters["@precio"].Value = prod.Precio;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }

        /// <summary>
        /// Borra un elemento de tipo producto
        /// </summary>
        /// <param name="codigo">codigo del producto pasado como argumento</param>
        /// <returns>numero entero para saber si se borró o no</returns>
        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("DELETE FROM Producto WHERE codigo=@codigo",con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}
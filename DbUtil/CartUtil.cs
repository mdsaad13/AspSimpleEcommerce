using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineShopping.Models;

namespace OnlineShopping.DbUtil
{
    public class CartUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppEnv.DBName};Integrated Security=True");

        private readonly string TableName = "cart";

        private readonly string TablePK = "cart_id";

        internal bool Insert(Cart model)
        {
            bool result = false;
            try
            {
                string query = $"INSERT INTO {TableName} (user_id, prod_id) VALUES(@user_id, @prod_id)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("user_id", model.UserID));
                cmd.Parameters.Add(new SqlParameter("prod_id", model.ProdID));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal List<Cart> List(int UserID)
        {
            DataTable td = new DataTable();
            List<Cart> list = new List<Cart>();

            ProductsUtil productsUtil = new ProductsUtil();
            try
            {
                string sqlquery = $"SELECT * FROM {TableName} WHERE user_id = @ID ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("ID", UserID));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();
                adp.Fill(td);
                Conn.Close();

                foreach (DataRow row in td.Rows)
                {
                    Cart obj = new Cart
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        UserID = Convert.ToInt32(row["user_id"]),
                        ProdID = Convert.ToInt32(row["prod_id"]),
                    };

                    obj.Product = productsUtil.GetByID(obj.ProdID);

                    list.Add(obj);
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }

            return list;
        }

        internal bool Delete(int ID)
        {
            bool result = false;
            try
            {
                string query = $"DELETE from {TableName} where {TablePK} = @id";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("id", ID));

                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }
            finally
            {
                Conn.Close();
            }

            return result;
        }

        internal bool DeleteByUser(int UserID)
        {
            bool result = false;
            try
            {
                string query = $"DELETE {TableName} FROM {TableName} INNER JOIN products on products.prod_id = cart.prod_id WHERE user_id = @id AND products.quantity > 0";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("id", UserID));

                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }
            finally
            {
                Conn.Close();
            }

            return result;
        }

    }
}
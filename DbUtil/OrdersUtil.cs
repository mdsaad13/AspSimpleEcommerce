using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineShopping.Models;

namespace OnlineShopping.DbUtil
{
    public class OrdersUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppEnv.DBName};Integrated Security=True");

        private readonly string TableName = "orders";

        private readonly string TablePK = "order_id";

        internal bool Insert(Orders model)
        {
            bool result = false;
            try
            {
                string query = $"INSERT INTO {TableName} (user_id, prod_id, status, created_at, updated_at) VALUES(@user_id, @prod_id, @status, @created_at, @updated_at)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("user_id", model.UserID));
                cmd.Parameters.Add(new SqlParameter("prod_id", model.ProdID));
                cmd.Parameters.Add(new SqlParameter("status", model.Status));
                cmd.Parameters.Add(new SqlParameter("created_at", model.Created_at));
                cmd.Parameters.Add(new SqlParameter("updated_at", model.Updated_at));

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

        internal List<Orders> List(bool FetchUserDetails = false)
        {
            DataTable td = new DataTable();
            List<Orders> list = new List<Orders>();

            ProductsUtil productsUtil = new ProductsUtil();
            AccountUtil accountUtil = new AccountUtil();

            try
            {
                string sqlquery = $"SELECT * FROM {TableName} ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Orders obj = new Orders
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        UserID = Convert.ToInt32(row["user_id"]),
                        ProdID = Convert.ToInt32(row["prod_id"]),
                        Status = Convert.ToInt32(row["status"]),
                        Created_at = Convert.ToDateTime(row["created_at"]),
                        Updated_at = Convert.ToDateTime(row["updated_at"]),
                    };
                    obj.Product = productsUtil.GetByID(obj.ProdID, false);
                    if (FetchUserDetails)
                    {
                        obj.User = accountUtil.GetUserByID(obj.UserID);
                    }
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
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal List<Orders> UsersList(int UserID)
        {
            DataTable td = new DataTable();
            List<Orders> list = new List<Orders>();

            ProductsUtil productsUtil = new ProductsUtil();
            try
            {
                string sqlquery = $"SELECT * FROM {TableName} WHERE user_id = @ID ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("ID", UserID));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Orders obj = new Orders
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        UserID = Convert.ToInt32(row["user_id"]),
                        ProdID = Convert.ToInt32(row["prod_id"]),
                        Status = Convert.ToInt32(row["status"]),
                        Created_at = Convert.ToDateTime(row["created_at"]),
                        Updated_at = Convert.ToDateTime(row["updated_at"]),
                    };
                    obj.Product = productsUtil.GetByID(obj.ProdID, false);

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
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal Orders GetByID(int ID)
        {
            DataTable td = new DataTable();
            List<Orders> list = new List<Orders>();

            ProductsUtil productsUtil = new ProductsUtil();
            try
            {
                string sqlquery = $"SELECT * FROM {TableName} WHERE {TablePK} = @ID ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("ID", ID));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Orders obj = new Orders
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        UserID = Convert.ToInt32(row["user_id"]),
                        ProdID = Convert.ToInt32(row["prod_id"]),
                        Status = Convert.ToInt32(row["status"]),
                        Created_at = Convert.ToDateTime(row["created_at"]),
                        Updated_at = Convert.ToDateTime(row["updated_at"]),
                    };
                    obj.Product = productsUtil.GetByID(obj.ProdID, false);

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
            finally
            {
                Conn.Close();
            }
            return list[0];
        }

        internal bool UpdateStatus(int OrderID, int Status)
        {
            bool result = false;
            DateTime dateTime = DateTime.Now;
            try
            {
                string query = $"UPDATE {TableName} SET status = @status, updated_at = @updated_at WHERE {TablePK} = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("status", Status));
                cmd.Parameters.Add(new SqlParameter("updated_at", dateTime));

                cmd.Parameters.Add(new SqlParameter("id", OrderID));

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

        internal bool Delete(int ID)
        {
            bool result = false;
            try
            {
                string query = $"DELETE FROM {TableName} WHERE {TablePK} = @id";
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

    }
}
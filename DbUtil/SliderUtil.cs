using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineShopping.Models;


namespace OnlineShopping.DbUtil
{
    public class SliderUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppEnv.DBName};Integrated Security=True");

        private readonly string TableName = "slider";

        private readonly string TablePK = "id";

        internal bool Insert(Slider model)
        {
            bool result = false;
            try
            {
                string query = $"INSERT INTO {TableName} (title, image, redirect) VALUES(@title, @image, @redirect)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("title", model.Title));
                cmd.Parameters.Add(new SqlParameter("image", model.Image));
                cmd.Parameters.Add(new SqlParameter("redirect", model.Redirect));

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

        internal List<Slider> List()
        {
            DataTable td = new DataTable();
            List<Slider> list = new List<Slider>();
            try
            {
                string sqlquery = $"SELECT * FROM {TableName} ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Slider obj = new Slider
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        Title = Convert.ToString(row["title"]),
                        Image = Convert.ToString(row["image"]),
                        Redirect = Convert.ToString(row["redirect"]),
                    };

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

        internal Slider GetByID(int ID)
        {
            DataTable td = new DataTable();
            List<Slider> list = new List<Slider>();
            try
            {
                string sqlquery = $"SELECT * FROM {TableName} WHERE {TablePK} = @ID";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("ID", ID));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Slider obj = new Slider
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        Title = Convert.ToString(row["title"]),
                        Image = Convert.ToString(row["image"]),
                        Redirect = Convert.ToString(row["redirect"]),
                    };

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

        internal bool Update(Slider model)
        {
            bool result = false;
            try
            {
                string query = $"UPDATE {TableName} SET title = @title, image = @image, redirect = @redirect WHERE {TablePK} = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("title", model.Title));
                cmd.Parameters.Add(new SqlParameter("image", model.Image));
                cmd.Parameters.Add(new SqlParameter("redirect", model.Redirect));

                cmd.Parameters.Add(new SqlParameter("id", model.ID));

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
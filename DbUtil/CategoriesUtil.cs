using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineShopping.Models;

namespace OnlineShopping.DbUtil
{
    public class CategoriesUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppEnv.DBName};Integrated Security=True");

        private readonly string TableName = "categories";

        private readonly string TablePK = "cat_id";

        internal bool Insert(Categories model)
        {
            bool result = false;
            try
            {
                string query = $"INSERT INTO {TableName} (name, isactive) VALUES(@name, @isactive)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("isactive", model.IsActive));

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

        internal List<Categories> List(bool OnlyActive = true)
        {
            DataTable td = new DataTable();
            List<Categories> list = new List<Categories>();
            try
            {
                string Active = $"WHERE {TableName}.isactive = 'true'";
                if (!OnlyActive)
                {
                    Active = "";
                }

                string sqlquery = $"SELECT {TableName}.*, (SELECT COUNT(*) FROM products WHERE {TableName}.cat_id = products.cat_id AND products.isactive = 1) as active_products, (SELECT COUNT(*) FROM products WHERE {TableName}.cat_id = products.cat_id) as total_products FROM {TableName} {Active} ORDER BY {TableName}.cat_id DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Categories obj = new Categories
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        Name = Convert.ToString(row["name"]),
                        IsActive = Convert.ToBoolean(row["isactive"]),
                        TotalProducts = Convert.ToInt32(row["total_products"]),
                        ActiveProducts = Convert.ToInt32(row["active_products"]),
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
        
        internal List<Categories> ListWithProducts(bool OnlyActive = true)
        {
            DataTable td = new DataTable();
            List<Categories> list = new List<Categories>();

            ProductsUtil productsUtil = new ProductsUtil();
            try
            {
                string Active = $"WHERE {TableName}.isactive = 'true'";
                if (!OnlyActive)
                {
                    Active = "";
                }

                string sqlquery = $"SELECT * FROM {TableName} {Active} ORDER BY name ASC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Categories obj = new Categories
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        Name = Convert.ToString(row["name"]),
                        IsActive = Convert.ToBoolean(row["isactive"]),
                    };

                    obj.Products = productsUtil.ListByCat(obj.ID, OnlyActive);

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

        internal Categories GetByID(int ID, bool OnlyActive = true)
        {
            DataTable td = new DataTable();
            List<Categories> list = new List<Categories>();
            try
            {
                string Active = $"WHERE {TableName}.isactive = 'true' AND";
                if (!OnlyActive)
                {
                    Active = "WHERE";
                }

                string sqlquery = $"SELECT {TableName}.*, (SELECT COUNT(*) FROM products WHERE {TableName}.cat_id = products.cat_id AND products.isactive = 1) as active_products, (SELECT COUNT(*) FROM products WHERE {TableName}.cat_id = products.cat_id) as total_products FROM {TableName} {Active} {TableName}.cat_id = @ID";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("ID", ID));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Categories obj = new Categories
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        Name = Convert.ToString(row["name"]),
                        IsActive = Convert.ToBoolean(row["isactive"]),
                        TotalProducts = Convert.ToInt32(row["total_products"]),
                        ActiveProducts = Convert.ToInt32(row["active_products"]),
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

        internal bool Update(Categories model)
        {
            bool result = false;
            try
            {
                string query = $"UPDATE {TableName} SET name = @name, isactive = @isactive WHERE {TablePK} = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("isactive", model.IsActive));

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
                string query = $"DELETE FROM orders WHERE prod_id in (SELECT prod_id FROM products WHERE cat_id = @id); DELETE FROM products WHERE cat_id = @id; DELETE FROM categories WHERE cat_id = @id;";
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
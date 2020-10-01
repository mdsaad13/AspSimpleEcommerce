using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineShopping.Models;

namespace OnlineShopping.DbUtil
{
    public class ProductsUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppEnv.DBName};Integrated Security=True");

        private readonly string TableName = "products";

        private readonly string TablePK = "prod_id";

        internal bool Insert(Products model)
        {
            bool result = false;
            try
            {
                string query = $"INSERT INTO {TableName} (cat_id, title, description, image, isactive, isfeatured, quantity, price) VALUES(@cat_id, @title, @description, @image, @isactive, @isfeatured, @quantity, @price)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("cat_id", model.CatID));
                cmd.Parameters.Add(new SqlParameter("title", model.Title));
                cmd.Parameters.Add(new SqlParameter("description", model.Description));
                cmd.Parameters.Add(new SqlParameter("image", model.ImageUrl));
                cmd.Parameters.Add(new SqlParameter("isactive", model.IsActive));
                cmd.Parameters.Add(new SqlParameter("isfeatured", model.IsFeatured));
                cmd.Parameters.Add(new SqlParameter("quantity", model.Quantity));
                cmd.Parameters.Add(new SqlParameter("price", model.Price));

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

        internal List<Products> List(bool OnlyActive = true)
        {
            DataTable td = new DataTable();
            List<Products> list = new List<Products>();
            try
            {
                string Active = $"WHERE {TableName}.isactive = 'true'";
                if (!OnlyActive)
                {
                    Active = "";
                }

                string sqlquery = $"SELECT {TableName}.*, categories.name as CategoryName FROM {TableName} INNER JOIN categories on categories.cat_id = {TableName}.cat_id {Active} ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Products obj = new Products
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        CatID = Convert.ToInt32(row["cat_id"]),
                        Title = Convert.ToString(row["title"]),
                        Description = Convert.ToString(row["description"]),
                        ImageUrl = Convert.ToString(row["image"]),
                        IsActive = Convert.ToBoolean(row["isactive"]),
                        IsFeatured = Convert.ToBoolean(row["isfeatured"]),
                        Quantity = Convert.ToInt32(row["quantity"]),
                        Price = Convert.ToDouble(row["price"]),
                        CategoryName = Convert.ToString(row["CategoryName"]),
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
        
        internal Products GetByID(int ID, bool OnlyActive = true)
        {
            DataTable td = new DataTable();
            List<Products> list = new List<Products>();
            try
            {
                string Active = $"WHERE {TableName}.isactive = 'true' AND";
                if (!OnlyActive)
                {
                    Active = "WHERE";
                }

                string sqlquery = $"SELECT {TableName}.*, categories.name as CategoryName FROM {TableName} INNER JOIN categories on categories.cat_id = {TableName}.cat_id {Active} {TablePK} = @ID ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("ID", ID));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Products obj = new Products
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        CatID = Convert.ToInt32(row["cat_id"]),
                        Title = Convert.ToString(row["title"]),
                        Description = Convert.ToString(row["description"]),
                        ImageUrl = Convert.ToString(row["image"]),
                        IsActive = Convert.ToBoolean(row["isactive"]),
                        IsFeatured = Convert.ToBoolean(row["isfeatured"]),
                        Quantity = Convert.ToInt32(row["quantity"]),
                        Price = Convert.ToDouble(row["price"]),
                        CategoryName = Convert.ToString(row["CategoryName"]),
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
        
        internal List<Products> ListByCat(int CatID, bool OnlyActive = true)
        {
            DataTable td = new DataTable();
            List<Products> list = new List<Products>();
            try
            {
                string Active = $"WHERE {TableName}.isactive = 'true' AND";
                if (!OnlyActive)
                {
                    Active = "WHERE";
                }

                string sqlquery = $"SELECT {TableName}.*, categories.name as CategoryName FROM {TableName} INNER JOIN categories on categories.cat_id = {TableName}.cat_id {Active} {TableName}.cat_id = @cat_id ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("cat_id", CatID));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Products obj = new Products
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        CatID = Convert.ToInt32(row["cat_id"]),
                        Title = Convert.ToString(row["title"]),
                        Description = Convert.ToString(row["description"]),
                        ImageUrl = Convert.ToString(row["image"]),
                        IsActive = Convert.ToBoolean(row["isactive"]),
                        IsFeatured = Convert.ToBoolean(row["isfeatured"]),
                        Quantity = Convert.ToInt32(row["quantity"]),
                        Price = Convert.ToDouble(row["price"]),
                        CategoryName = Convert.ToString(row["CategoryName"]),
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

        internal List<Products> ListByFeatured(bool OnlyActive = true)
        {
            DataTable td = new DataTable();
            List<Products> list = new List<Products>();
            try
            {
                string Active = $"WHERE {TableName}.isactive = 'true' AND isfeatured = 'true'";
                if (!OnlyActive)
                {
                    Active = "WHERE isfeatured = 'true'";
                }

                string sqlquery = $"SELECT {TableName}.*, categories.name as CategoryName FROM {TableName} INNER JOIN categories on categories.cat_id = {TableName}.cat_id {Active} ORDER BY {TablePK} DESC";

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Products obj = new Products
                    {
                        ID = Convert.ToInt32(row[TablePK]),
                        CatID = Convert.ToInt32(row["cat_id"]),
                        Title = Convert.ToString(row["title"]),
                        Description = Convert.ToString(row["description"]),
                        ImageUrl = Convert.ToString(row["image"]),
                        IsActive = Convert.ToBoolean(row["isactive"]),
                        IsFeatured = Convert.ToBoolean(row["isfeatured"]),
                        Quantity = Convert.ToInt32(row["quantity"]),
                        Price = Convert.ToDouble(row["price"]),
                        CategoryName = Convert.ToString(row["CategoryName"]),
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

        internal bool Update(Products model)
        {
            bool result = false;
            try
            {
                string query = $"UPDATE {TableName} SET cat_id = @cat_id, title = @title, description = @description, image = @image, isactive = @isactive, isfeatured = @isfeatured, quantity = @quantity WHERE {TablePK} = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("cat_id", model.CatID));
                cmd.Parameters.Add(new SqlParameter("title", model.Title));
                cmd.Parameters.Add(new SqlParameter("description", model.Description));
                cmd.Parameters.Add(new SqlParameter("image", model.ImageUrl));
                cmd.Parameters.Add(new SqlParameter("isactive", model.IsActive));
                cmd.Parameters.Add(new SqlParameter("isfeatured", model.IsFeatured));
                cmd.Parameters.Add(new SqlParameter("quantity", model.Quantity));

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
        
        internal bool UpdateQuantity(int ProdID, int Quantity)
        {
            bool result = false;
            try
            {
                string query = $"UPDATE {TableName} SET quantity = @quantity WHERE {TablePK} = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("quantity", Quantity));

                cmd.Parameters.Add(new SqlParameter("id", ProdID));

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
    }
}
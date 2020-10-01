using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using OnlineShopping.Models;

namespace OnlineShopping.DbUtil
{
    public class AccountUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppEnv.DBName};Integrated Security=True");

        public bool UserLogin(string Email, string Pass)
        {
            // default Login failed
            bool result = false;
            try
            {
                string query = "SELECT * FROM users WHERE email = @email AND password = @password";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("email", Email));
                cmd.Parameters.Add(new SqlParameter("password", Pass));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Current.Session[AppEnv.UserSessionKey] = dt.Rows[0]["user_id"];
                    HttpContext.Current.Session["UserName"] = dt.Rows[0]["name"];
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
            return result;
        }

        public bool AdminLogin(string Email, string Pass)
        {
            bool result = false;
            try
            {
                string query = "SELECT * FROM admin WHERE email = @email AND password = @password";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("email", Email));
                cmd.Parameters.Add(new SqlParameter("password", Pass));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = true;
                    HttpContext.Current.Session[AppEnv.AdminSessionKey] = dt.Rows[0]["admin_id"];
                    HttpContext.Current.Session["Name"] = dt.Rows[0]["name"];
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }
            return result;
        }

        internal bool AddUser(Users model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO users (name, email, phone, password, address) VALUES(@name, @email, @phone, @password, @address)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("phone", model.Phone));
                cmd.Parameters.Add(new SqlParameter("password", model.Password));
                cmd.Parameters.Add(new SqlParameter("address", model.Address));

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

        internal List<Users> AllUsers()
        {
            DataTable td = new DataTable();
            List<Users> list = new List<Users>();
            try
            {
                string sqlquery = "SELECT * FROM users ORDER BY user_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Users obj = new Users
                    {
                        ID = Convert.ToInt32(row["user_id"]),
                        Name = Convert.ToString(row["name"]),
                        Email = Convert.ToString(row["email"]),
                        Phone = Convert.ToString(row["phone"]),
                        Password = Convert.ToString(row["password"]),
                        Address = Convert.ToString(row["address"]),
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

        internal Users GetUserByID(int id)
        {
            DataTable td = new DataTable();
            Users obj = new Users();
            try
            {
                string sqlquery = "SELECT * FROM users where user_id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                obj.ID = Convert.ToInt32(td.Rows[0]["user_id"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Email = Convert.ToString(td.Rows[0]["email"]);
                obj.Phone = Convert.ToString(td.Rows[0]["phone"]);
                obj.Password = Convert.ToString(td.Rows[0]["password"]);
                obj.Address = Convert.ToString(td.Rows[0]["address"]);
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
            return obj;

        }

        internal bool UpdateUser(Users model)
        {
            bool result = false;
            try
            {
                string query = "UPDATE users SET name = @name, email = @email, phone = @phone, address = @address WHERE user_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("phone", model.Phone));
                cmd.Parameters.Add(new SqlParameter("address", model.Address));

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

        internal bool UpdateUserPassword(string NewPassword, int ID)
        {
            bool result = false;
            try
            {
                string query = "UPDATE users SET password = @password WHERE user_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("password", NewPassword));

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

        internal bool AddAdmin(Admin model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO admin (name, email, phone, password) VALUES(@name, @email, @phone, @password)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("phone", model.Phone));
                cmd.Parameters.Add(new SqlParameter("password", model.Password));

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

        internal List<Admin> AllAdmins(int IdNotInclude)
        {
            DataTable td = new DataTable();
            List<Admin> list = new List<Admin>();
            try
            {
                string sqlquery = "SELECT * FROM admin WHERE admin_id <> @admin_id ORDER BY admin_id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("admin_id", IdNotInclude));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Admin obj = new Admin
                    {
                        ID = Convert.ToInt32(row["admin_id"]),
                        Name = Convert.ToString(row["name"]),
                        Email = Convert.ToString(row["email"]),
                        Phone = Convert.ToString(row["phone"]),
                        Password = Convert.ToString(row["password"])
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

        internal Admin GetAdminByID(int id)
        {
            DataTable td = new DataTable();
            Admin obj = new Admin();
            try
            {
                string sqlquery = "SELECT * FROM admin where admin_id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                obj.ID = Convert.ToInt32(td.Rows[0]["admin_id"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Email = Convert.ToString(td.Rows[0]["email"]);
                obj.Phone = Convert.ToString(td.Rows[0]["phone"]);
                obj.Password = Convert.ToString(td.Rows[0]["password"]);
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
            return obj;

        }

        internal bool UpdateAdmin(Admin model)
        {
            bool result = false;
            try
            {
                string query = "UPDATE admin SET name = @name, email = @email, phone = @phone WHERE admin_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("phone", model.Phone));

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

        internal bool UpdateAdminPassword(string NewPassword, int ID)
        {
            bool result = false;
            try
            {
                string query = "UPDATE admin SET password = @password WHERE admin_id = @id";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("password", NewPassword));

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
using Microsoft.Extensions.Configuration;
using ShopBridge.Model;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;

namespace ShopBridge.Reopsitory
{
    public class ShopItemRepository : IShopItemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string conn;
        public ShopItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = _configuration.GetConnectionString("ConnectionString");
        }

        public async Task<ErrorModel> GetShopItem(int Id = 0, string Name = "")
        {
            DataTable Dt = new DataTable();
            var model = new ErrorModel();
            using SqlConnection con = new SqlConnection(conn);
            try
            {
                using SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "ShopBridgeItemCRUD";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "R");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);

                con.Open();

                SqlDataAdapter sdr = new SqlDataAdapter(cmd);

                sdr.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    model.ERR_CODE = 1;
                    model.MESSAGE = "Success";
                    model.FetchData = Generic.ConvertTableToList<ShopItemDTO>(Dt);
                }
                else
                {
                    model.ERR_CODE = 0;
                    model.MESSAGE = "No Data Found";
                    model.FetchData = Generic.ConvertTableToList<ShopItemDTO>(Dt);
                }
            }
            catch (Exception Ex)
            {
                model.ERR_CODE = 0;
                model.MESSAGE = "Failed" + "\n" + Ex.Message;
            }
            finally
            {
                con.Close();
            }
            return model;
        }

        public async Task<ErrorModel> AddShopItem(ShopItemCreat shopItem)
        {
            var model = new ErrorModel();
            if (CheckingItemExistornot(shopItem.Name))
            {
                using SqlConnection con = new SqlConnection(conn);
                try
                {

                    using SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "ShopBridgeItemCRUD";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.InputOutput, Value = shopItem.Id };
                    cmd.Parameters.AddWithValue("@Mode", "C");
                    cmd.Parameters.Add(parameter);
                    cmd.Parameters.AddWithValue("@Name", shopItem.Name);
                    cmd.Parameters.AddWithValue("@Description", shopItem.Description);
                    cmd.Parameters.AddWithValue("@Price", shopItem.Price);
                    cmd.Parameters.AddWithValue("@Actv_Ind", 1);
                    cmd.Parameters.AddWithValue("@Created_Dt", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    var data = (int)cmd.Parameters[1].Value;
                    model.ERR_CODE = 1;
                    model.MESSAGE = "Successfully added Item";
                    model.FetchData = "Id = " + data + ", Item Name = " + shopItem.Name;
                }
                catch (Exception Ex)
                {
                    model.ERR_CODE = 0;
                    model.MESSAGE = "Failed" + "\n" + Ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                model.ERR_CODE = 0;
                model.MESSAGE = "Item Already Exist";
            }
            return model;
        }

        private bool CheckingItemExistornot(string name)
        {
            var model = new ErrorModel();
            DataTable Dt = new DataTable();
            using SqlConnection con = new SqlConnection(conn);
            {
                try
                {

                    using SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "ShopBridgeItemCRUD";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "R");
                    cmd.Parameters.AddWithValue("@Name", name);

                    con.Open();

                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);

                    sdr.Fill(Dt);
                }
                catch (Exception ex)
                {
                    model.ERR_CODE = 0;
                    model.MESSAGE = "Failed" + "\n" + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            if (Dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<ErrorModel> UpdateShopItem(ShopItem shopItem)
        {
            var model = new ErrorModel();
            using SqlConnection con = new SqlConnection(conn);
            try
            {
                using SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "ShopBridgeItemCRUD";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.InputOutput, Value = shopItem.Id };
                cmd.Parameters.AddWithValue("@Mode", "U");
                cmd.Parameters.Add(parameter);
                cmd.Parameters.AddWithValue("@Name", shopItem.Name);
                cmd.Parameters.AddWithValue("@Description", shopItem.Description);
                cmd.Parameters.AddWithValue("@Price", shopItem.Price);
                cmd.Parameters.AddWithValue("@Actv_Ind", 1);
                cmd.Parameters.AddWithValue("@Created_Dt", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
                var data = (int)cmd.Parameters[1].Value;
                model.ERR_CODE = 1;
                model.MESSAGE = "Successfully Updated Item";
                model.FetchData = "Id = " + data + ", Item Name = " + shopItem.Name;
            }
            catch (Exception Ex)
            {
                model.ERR_CODE = 0;
                model.MESSAGE = "Failed" + "\n" + Ex.Message;
            }
            finally
            {
                con.Close();
            }
            return model;
        }

        public async Task<ErrorModel> RemoveShopItem(int Id)
        {
            var model = new ErrorModel();
            using SqlConnection con = new SqlConnection(conn);
            try
            {
                using SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "ShopBridgeItemCRUD";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "D");
                cmd.Parameters.AddWithValue("@Id", Id);

                con.Open();

                cmd.ExecuteNonQuery();
                model.ERR_CODE = 1;
                model.MESSAGE = "Successfully Item Deleted";
            }
            catch (Exception Ex)
            {
                model.ERR_CODE = 0;
                model.MESSAGE = "Failed" + "\n" + Ex.Message;
            }
            finally
            {
                con.Close();
            }
            return model;
        }
    }
}

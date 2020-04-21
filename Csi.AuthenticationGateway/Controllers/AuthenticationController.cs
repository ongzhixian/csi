using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Csi.AuthenticationGateway.Controllers
{
    [Route("auth")]
    [Route("api/auth")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public static byte[] CreateRandomSalt(int length)
        {
            // Create a buffer
            byte[] randBytes;

            if (length >= 1)
            {
                randBytes = new byte[length];
            }
            else
            {
                randBytes = new byte[1];
            }

            // Create a new RNGCryptoServiceProvider.
            System.Security.Cryptography.RNGCryptoServiceProvider rand = new System.Security.Cryptography.RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            // return the bytes.
            return randBytes;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // Yes, we can use HTTP GET but credentials to be stored in request header

            //System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=./wwwroot/data/mydb.db;Version=3;");
            // using (System.Data.IDbConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=./wwwroot/data/auth.db;Version=3;"))
            // {
            //     conn.Open();

            //     System.Data.IDbCommand cmd = conn.CreateCommand();

            //     cmd.CommandText = "INSERT INTO user (email, password_salt, password_hash, cre_dt, upd_dt) VALUES (@email, @salt, @hash, @cre, @upd);";
            //     cmd.CommandType = System.Data.CommandType.Text;

            //     System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();

            //     byte[] saltBytes = CreateRandomSalt(8);
            //     byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes("some password");
            //     byte[] amal = new byte[saltBytes.Length + pwdBytes.Length];
            //     Array.Copy(saltBytes, 0, amal, 0, saltBytes.Length);
            //     Array.Copy(pwdBytes, 0, amal, saltBytes.Length - 1, pwdBytes.Length);

            //     System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create();
            //     byte[] hashBytes = hash.ComputeHash(amal);


            //     cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@email", "ad1s@ads.zxc"));
            //     cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@salt", saltBytes));
            //     cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@hash", hashBytes));
            //     cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@cre", DateTime.UtcNow));
            //     cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@upd", DateTime.UtcNow));

            //     cmd.ExecuteNonQuery();
            // }

            using (System.Data.IDbConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=./wwwroot/data/auth.db;Version=3;"))
            {
                

                System.Data.IDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM user WHERE id = 2";
                
                conn.Open();
                System.Data.IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string email = (string)reader["email"];
                    byte[] salt = (byte[])reader["password_salt"];
                    byte[] hash = (byte[])reader["password_hash"];

                    // ITE
                    byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes("some password");
                    byte[] amal = new byte[salt.Length + pwdBytes.Length];
                    Array.Copy(salt, 0, amal, 0, salt.Length);
                    Array.Copy(pwdBytes, 0, amal, salt.Length - 1, pwdBytes.Length);

                    System.Security.Cryptography.SHA256 hasher = System.Security.Cryptography.SHA256.Create();
                    byte[] hashBytes = hasher.ComputeHash(amal);
                    
                    bool eq =hashBytes.SequenceEqual(hash);

                }
            }





            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // Use body and workout credentials

        }

        // Other HTTP methods; we are not supporting these

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }
    }
}

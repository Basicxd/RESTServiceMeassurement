using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTServiceMeassurement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeassurementsController : ControllerBase
    {
        private string ConnectionString =
            "Server=tcp:basic1997.database.windows.net,1433;Initial Catalog=MessurementMOCK;Persist Security Info=False;User ID=basic;Password=Polo1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Meassurements
        [HttpGet]
        public IList<Meassurement> GetAll()
        {
            const string selectString = "select * from dbo.Meassurement";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Meassurement> meassurementslList = new List<Meassurement>();
                        while (reader.Read())
                        {
                            Meassurement book = ReadMes(reader);
                            meassurementslList.Add(book);
                        }
                        return meassurementslList;
                    }
                }
            }
        }

        private static Meassurement ReadMes(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            int pressure = reader.GetInt32(1);
            int humidity = reader.GetInt32(2);
            int temperature = reader.GetInt32(3);
            DateTime timestamp = reader.GetDateTime(4);
            Meassurement sensor = new Meassurement
            {
                MesId = id,
                Pressure = pressure,
                Humidity = humidity,
                Temperature = temperature,
                TimeStamp = timestamp
            };
            return sensor;
        }

        // GET: api/Meassurements/5
        [HttpGet("{id}")]
        public Meassurement GetMeassOne(int id)
        {
            const string selectString = "select * from dbo.Meassurement where MesId=@id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        reader.Read();
                        return ReadMes(reader);
                    }
                }
            }
        }

        // POST: api/Meassurements
        [HttpPost]
        public HttpResponseMessage PostMeass([FromBody] Meassurement value)
        {
                const string insertString = "insert into dbo.Meassurement (pressure, humidity, temperature) values (@pressure, '@humidity', '@temperature')";
                using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@pressure", value.Pressure);
                        insertCommand.Parameters.AddWithValue("@humidity", value.Humidity);
                        insertCommand.Parameters.AddWithValue("@temperature", value.Temperature);
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            const string deleteStatement = "delete from book where MessId=@id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(deleteStatement, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
        }
    }
}

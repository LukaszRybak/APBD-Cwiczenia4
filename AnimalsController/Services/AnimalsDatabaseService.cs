using AnimalsController.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace AnimalsController.Services
{

    public class AnimalsDatabaseService : IDatabaseService
    {
        private IConfiguration _configuration;

        public AnimalsDatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            var result = new List<Animal>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {

                SqlCommand com = new SqlCommand();
                com.Connection = con;
                string sql = "SELECT * FROM Animal";
                if (orderBy == "name")
                {
                    sql += " ORDER BY name ASC";
                }
                else if (orderBy == "description")
                {
                    sql += " ORDER BY description ASC";
                }
                else if (orderBy == "category")
                {
                    sql += " ORDER BY category ASC";
                }
                else if (orderBy == "area")
                {
                    sql += " ORDER BY area ASC";
                }
                com.CommandText = sql;

                Console.WriteLine(com);
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Animal
                    {
                        Id = (int)reader["IdAnimal"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Category = reader["Category"].ToString(),
                        Area = reader["Area"].ToString()

                    });



                }

            }

            return result;
        }

        public void AddAnimal(Animal newAnimal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@name, @description, @category, @area)";
                com.Parameters.AddWithValue("@name", newAnimal.Name);
                com.Parameters.AddWithValue("@description", newAnimal.Description);
                com.Parameters.AddWithValue("@category", newAnimal.Category);
                com.Parameters.AddWithValue("@area", newAnimal.Area);

                con.Open();
                com.ExecuteNonQuery();
            }
        }

        public bool UpdateAnimal(int idAnimal, Animal updatedAnimal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area " +
                                  "WHERE IdAnimal = @idAnimal";
                com.Parameters.AddWithValue("@idAnimal", idAnimal);
                com.Parameters.AddWithValue("@name", updatedAnimal.Name);
                com.Parameters.AddWithValue("@description", updatedAnimal.Description);
                com.Parameters.AddWithValue("@category", updatedAnimal.Category);
                com.Parameters.AddWithValue("@area", updatedAnimal.Area);

                con.Open();
                int rowsAffected = com.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteAnimal(int idAnimal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM Animal WHERE IdAnimal = @idAnimal";
                com.Parameters.AddWithValue("@idAnimal", idAnimal);

                con.Open();
                int rowsAffected = com.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

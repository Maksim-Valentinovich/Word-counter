using Npgsql;
using System;
using System.Collections.Generic;


namespace New_Structure
{

    class DataBase
    {
        public void InputDB(NpgsqlDataSource dataSource)
        {
            using (var cmd = dataSource.CreateCommand("INSERT INTO Numbers (step) VALUES ($1)"))
            {
                Console.WriteLine("Введите текст, передаваемый в базу данных:");

                cmd.Parameters.AddWithValue(Console.ReadLine());

                cmd.ExecuteNonQuery();
            }
        }

        public string OutputDB(NpgsqlDataSource dataSource)
        {
            List<string> list = new List<string>();

            using (var cmd = dataSource.CreateCommand("SELECT * FROM numbers"))

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(1));

                    list.Add(reader.GetString(1));
                }
            }
            
            string[] chars = list.ToArray();

            return chars[0];
        }
    }
}

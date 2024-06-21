﻿using Chat.WebApi.Objetos;
using System.Data.SqlClient;

namespace Chat.WebApi.Repositorio
{
    public class Repositorio
    {
        private string connectionString;

        public Repositorio()
        {
            this.connectionString = "Server=DELLZAO\\SQLEXPRESS;Database=CHATBOT;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
        }

        public void SalvarPessoa(string nome, int idade, string email, string sexo)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Pessoa (Nome, Idade, Email, Sexo) VALUES (@Nome, @Idade, @Email, @Sexo)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", nome);
                        command.Parameters.AddWithValue("@Idade", idade);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Sexo", sexo);

                        int rowsAffected = command.ExecuteNonQuery();
                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public PessoaDto BuscarPessoaPorEmail(string email)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Nome, Idade, Email, Sexo FROM Pessoa WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PessoaDto
                                {
                                    Nome = reader["Nome"].ToString(),
                                    Idade = Convert.ToInt32(reader["Idade"]),
                                    Email = reader["Email"].ToString(),
                                    Sexo = reader["Sexo"].ToString()
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return null;
                }
            }
        }
    }
}

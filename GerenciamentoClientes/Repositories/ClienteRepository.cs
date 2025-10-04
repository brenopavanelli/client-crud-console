using GerenciamentoClientes.Models; // classe cliente criada
using Microsoft.Data.SqlClient;

namespace GerenciamentoClientes.Repositories;

public class ClienteRepository
{
    private readonly string _connectionString = "Server=PCPAVAS;Database=COMERCIODB;Trusted_Connection=True;";
    
    public void Inserir(Cliente cliente)
    {
        using (var conexao = new SqlConnection(_connectionString))
        {
            conexao.Open();
            
            var sql = "INSERT INTO Cliente (Nome, Email, Telefone) VALUES (@nome, @email, @telefone)";

            using (var comando = new SqlCommand(sql, conexao))
            {
                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@email", cliente.Email);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                
                comando.ExecuteNonQuery();
            }
        }
    }
}
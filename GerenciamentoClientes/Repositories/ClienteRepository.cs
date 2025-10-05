using GerenciamentoClientes.Models; // classe cliente criada
using Microsoft.Data.SqlClient;

namespace GerenciamentoClientes.Repositories;

public class ClienteRepository
{
    private readonly string _connectionString = "Server=PCPAVAS;Database=COMERCIODB;Trusted_Connection=True;TrustServerCertificate=True;";
    
    public void Inserir(Cliente cliente) // Recebe um objeto Cliente (a planta baixa preenchida) e o salva no banco de dados.
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
    public List<Cliente> Listar() // Vai até o banco de dados e retorna uma lista com todos os clientes cadastrados.
    {
        var listaDeClientes = new List<Cliente>();
    
        using (var conexao = new SqlConnection(_connectionString))
        {
            conexao.Open();
        
            var sql = "SELECT Id, Nome, Email, Telefone FROM Cliente";

            using (var comando = new SqlCommand(sql, conexao))
            {

                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {

                        var cliente = new Cliente();
                        
                        cliente.Id = leitor.GetInt32(0); // Coluna 0 (Id)
                        cliente.Nome = leitor.GetString(1); // Coluna 1 (Nome)
                        cliente.Email = leitor.GetString(2); // Coluna 2 (Email)
                        cliente.Telefone = leitor.GetString(3); // Coluna 3 (Telefone)
                        
                        listaDeClientes.Add(cliente);
                    }
                }
            }
        }
        return listaDeClientes;
    }

    public void Atualizar(Cliente cliente) // Recebe um cliente com dados modificados e atualiza o registro correspondente no banco.
    {
        using (var conexao = new SqlConnection(_connectionString))
        {
            conexao.Open();
            
            var sql = "UPDATE Cliente SET Nome = @nome, Email = @email, Telefone = @telefone WHERE Id = @id";

            using (var comando = new SqlCommand(sql, conexao))
            {
                comando.Parameters.AddWithValue("@id", cliente.Id);
                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@email", cliente.Email);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                
                comando.ExecuteNonQuery();
            }
        }
    }

    public void Deletar(int id) // Recebe um id e elimina o cliente com o respectivo id
    {
        using (var conexao = new SqlConnection(_connectionString))
        {
            conexao.Open();
            
            var sql = "DELETE FROM Cliente WHERE Id = @id";

            using (var comando = new SqlCommand(sql, conexao))
            {
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
        }
    }
}


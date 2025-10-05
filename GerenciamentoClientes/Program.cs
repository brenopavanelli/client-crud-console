// import
using GerenciamentoClientes.Models;
using GerenciamentoClientes.Repositories;

class Program
{
    static void Main(string[] args)
    {
        
        var clienteRepository = new ClienteRepository();

        
        while (true)
        {
            Console.WriteLine("\n--- Gerenciamento de Clientes ---");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Inserir Cliente");
            Console.WriteLine("2 - Listar Clientes");
            Console.WriteLine("3 - Atualizar Cliente");
            Console.WriteLine("4 - Deletar Cliente");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");

            
            if (!int.TryParse(Console.ReadLine(), out int opcao))
            {
                Console.WriteLine("Opção inválida! Pressione Enter para continuar.");
                Console.ReadLine();
                continue; 
            }

            
            switch (opcao)
            {
                case 1:
                    InserirCliente(clienteRepository);
                    break;
                case 2:
                    ListarClientes(clienteRepository);
                    break;
                case 3:
                    AtualizarCliente(clienteRepository);
                    break;
                case 4:
                    DeletarCliente(clienteRepository);
                    break;
                case 0:
                    Console.WriteLine("Saindo do sistema...");
                    return; 
                default:
                    Console.WriteLine("Opção inválida! Pressione Enter para continuar.");
                    Console.ReadLine();
                    break;
            }
        }
    }
    
    private static void InserirCliente(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Inserir Novo Cliente ---");
        var novoCliente = new Cliente(); 

        Console.Write("Nome: ");
        novoCliente.Nome = Console.ReadLine();

        Console.Write("Email: ");
        novoCliente.Email = Console.ReadLine();

        Console.Write("Telefone: ");
        novoCliente.Telefone = Console.ReadLine();
        
        repository.Inserir(novoCliente); // chama a função de inserir no repository

        Console.WriteLine("Cliente inserido com sucesso! Pressione Enter para continuar.");
        Console.ReadLine();
    }
    
    private static void ListarClientes(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Lista de Clientes ---");

        var clientes = repository.Listar();

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.Id}, Nome: {cliente.Nome}, Email: {cliente.Email}, Telefone: {cliente.Telefone}");
            }
        }

        Console.WriteLine("\nPressione Enter para continuar.");
        Console.ReadLine();
    }
    
    private static void AtualizarCliente(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Atualizar Cliente ---");
        Console.Write("Digite o ID do cliente que deseja atualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        
        var clienteAtualizado = new Cliente { Id = id };

        Console.Write("Novo Nome: ");
        clienteAtualizado.Nome = Console.ReadLine();
        Console.Write("Novo Email: ");
        clienteAtualizado.Email = Console.ReadLine();
        Console.Write("Novo Telefone: ");
        clienteAtualizado.Telefone = Console.ReadLine();

        repository.Atualizar(clienteAtualizado);

        Console.WriteLine("Cliente atualizado com sucesso! Pressione Enter para continuar.");
        Console.ReadLine();
    }
    
    private static void DeletarCliente(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Deletar Cliente ---");
        Console.Write("Digite o ID do cliente que deseja deletar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        
        repository.Deletar(id);

        Console.WriteLine("Cliente deletado com sucesso! Pressione Enter para continuar.");
        Console.ReadLine();
    }
}
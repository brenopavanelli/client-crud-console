// Importando os "endereços" das classes que vamos usar.
using GerenciamentoClientes.Models;
using GerenciamentoClientes.Repositories;

// O gerente precisa saber onde o construtor está (Repository)
// e qual é a planta baixa (Model).

/// <summary>
/// Esta é a classe principal, a "Recepção" do nosso sistema.
/// Ela é responsável por interagir com o usuário e orquestrar as operações.
/// </summary>
class Program
{
    // O ponto de entrada do nosso programa. A execução começa aqui.
    static void Main(string[] args)
    {
        // 1. CONTRATANDO O CONSTRUTOR
        // Criamos uma única instância do nosso Repository.
        // O gerente terá este construtor à sua disposição durante toda a execução.
        var clienteRepository = new ClienteRepository();

        // 2. MANTENDO A RECEPÇÃO ABERTA
        // Um loop infinito para que o menu sempre reapareça após uma operação,
        // a menos que o usuário escolha sair.
        while (true)
        {
            // 3. APRESENTANDO O MENU DE OPÇÕES
            Console.WriteLine("\n--- Gerenciamento de Clientes ---");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Inserir Cliente");
            Console.WriteLine("2 - Listar Clientes");
            Console.WriteLine("3 - Atualizar Cliente");
            Console.WriteLine("4 - Deletar Cliente");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");

            // 4. RECEBENDO A ORDEM DO USUÁRIO
            // Lê o que o usuário digitou e tenta converter para um número.
            if (!int.TryParse(Console.ReadLine(), out int opcao))
            {
                // Se o usuário não digitou um número, avisa e volta ao início do loop.
                Console.WriteLine("Opção inválida! Pressione Enter para continuar.");
                Console.ReadLine();
                continue; // Pula para a próxima iteração do loop
            }

            // 5. DIRECIONANDO A TAREFA
            // O switch é como a central telefônica do gerente.
            // Ele direciona a chamada para o ramal (método) correto.
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
                    // Se a opção for 0, encerra o programa.
                    Console.WriteLine("Saindo do sistema...");
                    return; // Encerra o método Main, e consequentemente o programa.
                default:
                    // Se for qualquer outro número.
                    Console.WriteLine("Opção inválida! Pressione Enter para continuar.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    /// <summary>
    /// Método responsável por coletar os dados e pedir ao repository para inserir.
    /// </summary>
    private static void InserirCliente(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Inserir Novo Cliente ---");
        var novoCliente = new Cliente(); // Cria uma "ficha" em branco.

        Console.Write("Nome: ");
        novoCliente.Nome = Console.ReadLine();

        Console.Write("Email: ");
        novoCliente.Email = Console.ReadLine();

        Console.Write("Telefone: ");
        novoCliente.Telefone = Console.ReadLine();

        // Entrega a ficha preenchida para o construtor.
        repository.Inserir(novoCliente);

        Console.WriteLine("Cliente inserido com sucesso! Pressione Enter para continuar.");
        Console.ReadLine();
    }

    /// <summary>
    /// Método que pede a lista de clientes ao repository e a exibe na tela.
    /// </summary>
    private static void ListarClientes(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Lista de Clientes ---");
        // Pede a lista de todas as fichas para o construtor.
        var clientes = repository.Listar();

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            // Itera sobre cada ficha na lista e exibe seus dados.
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.Id}, Nome: {cliente.Nome}, Email: {cliente.Email}, Telefone: {cliente.Telefone}");
            }
        }

        Console.WriteLine("\nPressione Enter para continuar.");
        Console.ReadLine();
    }

    /// <summary>
    /// Método para a lógica de atualização de um cliente.
    /// </summary>
    private static void AtualizarCliente(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Atualizar Cliente ---");
        Console.Write("Digite o ID do cliente que deseja atualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        // Aqui, um sistema real primeiro buscaria o cliente para ver se ele existe.
        // Para simplificar, vamos pedir todos os dados novamente.
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

    /// <summary>
    /// Método para a lógica de exclusão de um cliente.
    /// </summary>
    private static void DeletarCliente(ClienteRepository repository)
    {
        Console.WriteLine("\n--- Deletar Cliente ---");
        Console.Write("Digite o ID do cliente que deseja deletar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        
        // Pede para o construtor demolir o registro com o ID informado.
        repository.Deletar(id);

        Console.WriteLine("Cliente deletado com sucesso! Pressione Enter para continuar.");
        Console.ReadLine();
    }
}
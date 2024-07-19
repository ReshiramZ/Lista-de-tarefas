using Lista_de_Tarefas;

bool ProgramaAberto = true;
string Input;

while (ProgramaAberto == true)
{
    Console.Clear();
    Console.WriteLine("O que deseja fazer?\n"); 
    Console.WriteLine("A. Adicionar tarefa\n" +
                      "D. Deletar tarefa\n" +
                      "M. Marcar tarefa\n\n" +
                      "S. Salvar lista de tarefas\n" +
                      "C. Carregar uma lista de tarefas\n" +
                      "O. Sobrescrever lista de tarefas\n\n" +
                      "Q. Sair do programa\n");

    Metodos.ListarTarefas();

    Input = Console.ReadLine().ToUpper();

    switch (Input)
    {
        case "A":
            Metodos.AdicionarTarefa();
            break;

        case "D":
            Metodos.DeletarTarefa();
            break;

        case "M":
            Metodos.MarcarTarefa();
            break;

        case "S":
            Metodos.SalvarLista();
            break;

        case "C":
            Metodos.CarregarLista();
            break;

        case "O":
            Metodos.SobrescreverLista();
            break;

        case "Q":
            ProgramaAberto = false;
            break;

        default:
            Console.WriteLine("Opção inválida, digite novamente");
            Console.ReadKey();
            break;
    }    
}
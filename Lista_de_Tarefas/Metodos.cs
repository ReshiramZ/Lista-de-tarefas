using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace Lista_de_Tarefas
{
    internal class Metodos
    {
        private static List<string> ListaDeTarefas = new();
        private static string DiretorioPrincipal = "Tarefas Salvas/";
        private static string ArquivoComFormato;
        private static string NomeArquivo;
        private static bool ConverterDeuCerto;


        public static void AdicionarTarefa()
        {
            string Tarefa;
            Console.WriteLine("O que deseja anotar?");
            Tarefa = Console.ReadLine();
            if (!string.IsNullOrEmpty(Tarefa)) // Verifica se a string está vazia ou não.
            {
                ListaDeTarefas.Add(Tarefa);                                     
            }
            else
            {
                Console.WriteLine("A anotação está vazia, anotação negada.");
                Console.ReadKey();
            }
        }

        public static void DeletarTarefa()
        {
            if (ListaDeTarefas.Count > 0)
            {
                Console.WriteLine("Qual tarefa você quer deletar?");
                ConverterDeuCerto = int.TryParse(Console.ReadLine(), out int DeletarTarefa);
                
                if (ConverterDeuCerto == true)
                {
                    if (DeletarTarefa >= 0 && DeletarTarefa < ListaDeTarefas.Count)
                    {
                        ListaDeTarefas.RemoveAt(DeletarTarefa);
                        Console.WriteLine("Tarefa deletada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("O número fornecido não corresponde aos números acima, não foi possível deletar.");
                    }
                }
                else
                {
                    Console.WriteLine("O 'número' fornecido não é válido, não foi possível deletar.");
                }
            }
            else
            {
                Console.WriteLine("A lista está vazia, não foi possível deletar nada.");
            }
            Console.ReadKey();
        }
        public static void MarcarTarefa()
        {
            if (ListaDeTarefas.Count > 0)
            { 
                Console.WriteLine("Qual tarefa você quer marcar?");
                ConverterDeuCerto = int.TryParse(Console.ReadLine(), out int MarcarTarefa);
                if (ConverterDeuCerto == true)
                {
                    if (MarcarTarefa >= 0 && MarcarTarefa < ListaDeTarefas.Count)
                    {
                        if (!ListaDeTarefas[MarcarTarefa].Contains(" *Concluído*"))
                        {
                            ListaDeTarefas[MarcarTarefa] += " *Concluído*";
                            Console.WriteLine("Tarefa marcada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("A tarefa já está marcada como concluída.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("O número fornecido não corresponde aos números acima, não foi possível marcar.");
                    }
                }
                else
                {
                    Console.WriteLine("O 'número' fornecido não é válido, não foi possível marcar.");
                }
            }
            else
            {
                Console.WriteLine("A lista está vazia, não foi possível marcar nada.");

            }
            Console.ReadKey();
        }
        public static void SalvarLista() // Salva um arquivo .txt em uma subpasta, que fica na pasta Raíz do executável
        {
            if (ListaDeTarefas.Count > 0)
            {
                Console.WriteLine("Digite o nome do arquivo...");

                NomeArquivo = Console.ReadLine();
                if (!string.IsNullOrEmpty(NomeArquivo))
                {
                    ArquivoComFormato = $"Tarefas Salvas/{NomeArquivo}.txt";
                    File.WriteAllLines(ArquivoComFormato, ListaDeTarefas);
                    Console.WriteLine($"O arquivo {NomeArquivo} foi criado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Não foi possível salvar a lista, pois o nome do mesmo está vazio.");
                }
            }
            else
            {
                Console.WriteLine("Você não pode salvar uma lista de tarefa se ela estiver vazia.");
            }
            Console.ReadKey();
        }
        public static void CarregarLista()
        {
            int Contador = 0;
            var Arquivos = Directory.GetFiles(DiretorioPrincipal);

            Console.Clear();
            Console.WriteLine("Selecione uma lista salva:\n");

            foreach (var Arquivo in Arquivos)
            {
                Console.WriteLine($"{Contador} - {Path.GetFileNameWithoutExtension(Arquivo)}");
                Contador++;
            }

            ConverterDeuCerto = int.TryParse(Console.ReadLine(), out int EscolherArquivo);

            if (ConverterDeuCerto == true)
            {
                if (EscolherArquivo >= 0 && EscolherArquivo < Arquivos.Length)      // Verifica o número que o usuário digitou e, em seguida,
                {                                                                   // coloca o número como index para que possa ler o arquivo
                    var LerArquivo = File.ReadAllLines(Arquivos[EscolherArquivo]);

                    Console.WriteLine("Arquivo carregado com sucesso!");

                    ListaDeTarefas.Clear();

                    foreach (var Linha in LerArquivo)
                    {
                        ListaDeTarefas.Add(Linha);
                    }
                }
                else
                {
                    Console.WriteLine("O número fornecido não corresponde aos números acima, não foi possível carregar a lista.");
                }
            }
            else
            {
                Console.WriteLine("O 'número' fornecido não é válido, não foi possível carregar a lista.");
            }

            Console.ReadKey();
        }

        public static void SobrescreverLista()
        {
            var Arquivos = Directory.GetFiles(DiretorioPrincipal);
            int EscolherArquivo;
            int Contador = 0;

            Console.WriteLine("Qual lista deseja sobrescrever?");
            foreach (var Arquivo in Arquivos)
            {
                Console.WriteLine($"{Contador} - {Path.GetFileNameWithoutExtension(Arquivo)}");
                Contador++;
            }

            ConverterDeuCerto = int.TryParse(Console.ReadLine(), out EscolherArquivo);
            if (ConverterDeuCerto == true)
            {
                if (EscolherArquivo >= 0 && EscolherArquivo < Arquivos.Length)
                {
                    File.WriteAllLines(Arquivos[EscolherArquivo], ListaDeTarefas);
                    Console.WriteLine("Lista sobrescrita com sucesso!");
                }
                else
                {
                    Console.WriteLine("O número fornecido não corresponde aos números acima, não foi possível sobrescrever a lista.");
                }
            }
            else
            {
                Console.WriteLine("O 'número' fornecido não é válido, não foi possível sobrescrever a lista.");
            }
            Console.ReadKey();
        }

        public static void ListarTarefas()
        {
            for (int i = 0; i < ListaDeTarefas.Count; i++)
            {
                Console.WriteLine($"{i} - {ListaDeTarefas[i]}");
            }
        }
    }
}

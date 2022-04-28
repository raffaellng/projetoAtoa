using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        private static string gravaTitulo = "";
        private static int arquivoCriado = 0;
        private static int parte1Preenchida = 0;

        static void Main(string[] args)
        {


            try
            {
                Console.WriteLine("Informe o caminho do arquivo");
                string caminho = Console.ReadLine();

                if (!Directory.Exists(caminho))
                    throw new ArgumentNullException("Diretorio não existe");

                var diretorioArquivo = Directory.GetFiles(caminho, "*.txt").First();

                if (diretorioArquivo == null)
                    throw new ArgumentNullException("argcannot be null");

                string[] linhas = File.ReadAllLines(diretorioArquivo);

                foreach (var linha in linhas)
                {

                    char[] charArray = linha.ToCharArray();

                    if (charArray.Length != 0)
                    {
                        char first = charArray[0];
                        if (first.ToString() != "%" && linha != "fim")
                        {
                            gravaTitulo = linha;
                            criarPasta(caminho, linha);
                        }
                        else if (first.ToString() == "%")
                        {
                            if (arquivoCriado == 0)
                            {
                                criarArquivo3GetSetInicial(gravaTitulo, caminho);
                                criarArquivo3GetSetInsirindo(linha, gravaTitulo, caminho);
                                arquivoCriado = 1;
                            }
                            else
                            {
                                criarArquivo3GetSetInsirindo(linha, gravaTitulo, caminho);
                            }
                        }
                        else if (linha == "fim")
                        {
                            criarArquivo3GetSetFinal(gravaTitulo, caminho);
                        }
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex + "Erro interno, ou envio de dados no formato errado");
            }

        }

        private static void criarPasta(string caminho, string titulo)
        {
            string caminhoFinal = caminho + "\\" + titulo;
            if (!Directory.Exists(caminhoFinal))
            {
                Directory.CreateDirectory(caminhoFinal);
                Console.WriteLine(caminhoFinal);
            }

            criarArquivo(caminhoFinal, titulo);
        }

        private static void criarArquivo(string caminho, string titulo)
        {
            string arquivo1 = "TransitionCommand.cs";
            string arquivo2 = "TransitionCommandHandler.cs";

            string arquivoCriado1 = Path.Combine(caminho, titulo + arquivo1);
            File.Create(arquivoCriado1).Dispose();
            preencherArquivoHeranca(arquivoCriado1, titulo);

            string arquivoCriado2 = Path.Combine(caminho, titulo + arquivo2);
            File.Create(arquivoCriado2).Dispose();
            preencherArquivoServicos(arquivoCriado2, titulo);
        }

        private static void criarArquivo3GetSetInicial(string titulo, string caminho)
        {
            string arquivo3 = "TransitionCommandResult.cs";
            string arquivoCriado3 = Path.Combine(caminho + "\\" + titulo + "\\" + titulo + arquivo3);

            File.Create(arquivoCriado3).Dispose();
            arquivoCriado = 1;

            preencherDadosArquivo3(arquivoCriado3, titulo);
        }

        private static void criarArquivo3GetSetInsirindo(string variavel, string gravaTitulo, string caminho)
        {
            string arquivo3 = "TransitionCommandResult.cs";
            string arquivoCriado3 = Path.Combine(caminho + "\\" + gravaTitulo + "\\" + gravaTitulo + arquivo3);

            string novoTitulo = formataVariavel(variavel);
            preencherDadosArquivo3Parte2(arquivoCriado3, novoTitulo);
        }

        private static void criarArquivo3GetSetFinal(string gravaTitulo, string caminho)
        {
            string arquivo3 = "TransitionCommandResult.cs";
            string arquivoCriado3 = Path.Combine(caminho + "\\" + gravaTitulo + "\\" + gravaTitulo + arquivo3);

            preencherDadosArquivo3Final(arquivoCriado3);
        }

        private static string formataVariavel(string titulo)
        {
            titulo = titulo.Remove(0, 1);
            titulo = char.ToUpper(titulo[0]) + titulo.Substring(1);

            return titulo;
        }

        private static void preencherArquivoHeranca(string arquivo, string titulo)
        {
            try
            {
                StreamWriter valor = new StreamWriter(arquivo);
                valor.WriteLine("namespace Mutant.Galileo.Domain.Application.Commands.States." + titulo);
                valor.WriteLine("{");
                valor.WriteLine("    public class " + titulo + "TransitionCommand : BaseStateCommand<" + titulo + "TransitionCommandResult>");
                valor.WriteLine("    {");
                valor.WriteLine("    }");
                valor.WriteLine("}");
                valor.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Arquivos " + titulo + "TransitionCommand criado");
            }
        }
        private static void preencherArquivoServicos(string arquivo, string titulo)
        {
            try
            {
                StreamWriter valor = new StreamWriter(arquivo);
                valor.WriteLine("using Microsoft.Extensions.Logging;");
                valor.WriteLine("using Mutant.Galileo.Domain.Contracts.Application.Orchestration;");
                valor.WriteLine("using System.Threading;");
                valor.WriteLine("");
                valor.WriteLine("namespace Mutant.Galileo.Domain.Application.Commands.States." + titulo);
                valor.WriteLine("{");
                valor.WriteLine("    public class " + titulo + "TransitionCommandHandler :");
                valor.WriteLine("        BaseStateCommandHandler<" + titulo + "TransitionCommand, " + titulo + "TransitionCommandResult>");
                valor.WriteLine("    {");
                valor.WriteLine("        private readonly ILogger<" + titulo + "TransitionCommandHandler> _logger;");
                valor.WriteLine("        private readonly IClienteLogic _clienteLogic;");
                valor.WriteLine("");
                valor.WriteLine("        public " + titulo + "TransitionCommandHandler(ILogger<" + titulo + "TransitionCommandHandler> logger, IClienteLogic clienteLogic)");
                valor.WriteLine("        {");
                valor.WriteLine("            this._logger = logger;");
                valor.WriteLine("            this._clienteLogic = clienteLogic;");
                valor.WriteLine("        }");
                valor.WriteLine("");
                valor.WriteLine("        public override " + titulo + "TransitionCommandResult HandleRequest(" + titulo + "TransitionCommand request, CancellationToken cancellationToken)");
                valor.WriteLine("        {");
                valor.WriteLine("            var ret = new " + titulo + "TransitionCommandResult();");
                valor.WriteLine("");
                valor.WriteLine("            return ret;");
                valor.WriteLine("        }");
                valor.WriteLine("    }");
                valor.WriteLine("}");

                valor.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Arquivos " + titulo + "TransitionCommandHandler criado");
            }
        }

        private static void preencherDadosArquivo3(string arquivo, string titulo)
        {
            try
            {
                StreamWriter valor = new StreamWriter(arquivo, true);
                valor.WriteLine("namespace Mutant.Galileo.Domain.Application.Commands.States." + titulo);
                valor.WriteLine("{");
                valor.WriteLine("    public class ModeloTransitionCommandResult");
                valor.WriteLine("    {");
                valor.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Arquivos 3 criado e preenchido");
            }
        }

        private static void preencherDadosArquivo3Parte2(string arquivo, string titulo)
        {
            try
            {
                StreamWriter valor = new StreamWriter(arquivo, true);
                valor.WriteLine("        public bool " + titulo + " { get; set; }");
                valor.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Arquivos 3 criado e preenchido");
            }
        }

        private static void preencherDadosArquivo3Final(string arquivoCriado3)
        {
            try
            {
                StreamWriter valor = new StreamWriter(arquivoCriado3, true);
                valor.WriteLine("    }");
                valor.WriteLine("}");
                valor.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Arquivos 3 criado e preenchido");
            }
        }
    }
}

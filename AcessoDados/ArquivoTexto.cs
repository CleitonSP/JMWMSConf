using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace AcessoDados
{
    public class ArquivoTexto
    {

        public String LerArquivoTexto(string strCaminhoNomeArquivo)
        {
            try
            {
                return System.IO.File.ReadAllText(@"" + strCaminhoNomeArquivo);

            }
            catch (System.IO.IOException Erro)
            {
                return "Erro : " + Erro.Message;
            }
        }

        public String[] LerLinhasTexto(string strCaminhoNomeArquivo)
        {
            try
            {
                return System.IO.File.ReadAllLines(@"" + strCaminhoNomeArquivo);

            }
            catch (System.IO.IOException Erro)
            {
                return new string[] { "Erro : " + Erro.Message };
            }
        }

        public byte[] LerArquivoBytes(string strCaminhoNomeArquivo)
        {
            try
            {
                return
                System.IO.File.ReadAllBytes(@"" + strCaminhoNomeArquivo);

            }
            catch (Exception Erro)
            {
                Console.WriteLine("Erro : " + Erro.Message);
                return null;
            }

        }

        public Boolean EscreverArquivoXml(string strCaminhoNomeArquivo, XmlDocument xmlConteudo)
        {
            try
            {
                xmlConteudo.Save(@"" + strCaminhoNomeArquivo + ".xml");
                return true;
            }
            catch (System.IO.IOException Erro)
            {
                Console.WriteLine("Erro : " + Erro.Message);
                return false;
            }
        }

        public Boolean EscreverArquivoTexto(string strCaminhoNomeArquivo, string strTexto)
        {
            try
            {
                //escreve todo o texto no caminho especificado
                System.IO.File.WriteAllText(@"" + strCaminhoNomeArquivo, strTexto);

                return true;
            }
            catch (System.IO.IOException Erro)
            {
                Console.WriteLine("Erro : " + Erro.Message);
                return false;
            }

        }

        public Boolean ConcatenaArquivoTexto(string strCaminhoNomeArquivo, string strTexto)
        {
            try
            {
                //concatena ao arquivo informado todo o texto recebido como parametro
                System.IO.File.AppendAllText(@"" + strCaminhoNomeArquivo, strTexto);

                return true;
            }
            catch (System.IO.IOException Erro)
            {
                Console.WriteLine("Erro : " + Erro.Message);
                return false;
            }
        }

        public Boolean EscreverArquivoBytes(string strCaminhoNomeArquivo, byte[] btBytes)
        {
            try
            {
                System.IO.File.WriteAllBytes(@"" + strCaminhoNomeArquivo, btBytes);
                return true;
            }
            catch (Exception Erro)
            {
                Console.WriteLine("Erro : " + Erro.Message);
                return false;
            }

        }

        public Boolean DeletarArquivoTexto(string NomeCaminho)
        {
            try
            {
                FileInfo fiArqquivo = new FileInfo(NomeCaminho);

                fiArqquivo.Delete();

                return true;
            }
            catch (Exception Erro)
            {
                Console.WriteLine("Erro : " + Erro.Message);
                return false;
            }

        }

    }
}


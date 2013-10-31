using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using AcessoDados.Properties;
using System.Data;

namespace AcessoDados
{
    public class ConexaoJmoreira
    {

        #region Pegar Conexão
        public SqlConnection PegarConexao()
        {
            try
            {

                return new SqlConnection(Settings.Default.Jmoreira);


            }
            catch (Exception Erro)
            {
                throw new Exception(Erro.Message);
            }
        }
        #endregion

        #region Parametros

        private SqlParameterCollection objparametros = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            try
            {
                objparametros.Clear();
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Limpar paramêtro. Messagem:" + erro.Message);
            }
        }

        public void AddParametros(string strNomeParametro, object objValorparametro)
        {
            try
            {
                objparametros.Add(new SqlParameter(strNomeParametro, objValorparametro));
            }
            catch
            {
                throw new Exception("Erro ao adicionar paramêtro na camada de Acesso a Dados!");
            }
        }
        #endregion

        #region Persistencia, Inserir, Alterar, Excluir.

        public object ExecutarManipulacao(CommandType objtipocomando, string strtextosql)
        {
            try
            {
                SqlConnection objconexao = PegarConexao();
                objconexao.Open();
                SqlCommand ObjComando = objconexao.CreateCommand();

                //ObjComando.CommandTimeout = 8000;

                ObjComando.CommandType = objtipocomando;

                ObjComando.CommandText = strtextosql;


                foreach (SqlParameter objparametro in objparametros)
                {
                    ObjComando.Parameters.Add(new SqlParameter { ParameterName = objparametro.ParameterName, Value = objparametro.Value });
                }

                object objEscalar = ObjComando.ExecuteScalar();
                objconexao.Close();
                return objEscalar;

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        #endregion

        #region Consultar

        public DataTable ConsultarRegistros(CommandType objtipocomando, string strtextosql)
        {
            try
            {
                SqlConnection objConexao = PegarConexao();

                objConexao.Open();

                SqlCommand objcomando = objConexao.CreateCommand();
                objcomando.CommandTimeout = 0;

                objcomando.CommandType = objtipocomando;

                objcomando.CommandText = strtextosql;

                foreach (SqlParameter objParametro in objparametros)
                {
                    objcomando.Parameters.Add(new SqlParameter { ParameterName = objParametro.ParameterName, Value = objParametro.Value });
                }

                SqlDataAdapter objadaptador = new SqlDataAdapter(objcomando);

                DataTable objTabela = new DataTable();

                objadaptador.Fill(objTabela);


                //fecha a conexão
                objConexao.Close();

                return objTabela;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        #endregion

        #region INSERE NO BANCO UM DATATABLE COMPLETO SEM FOREACH COM A FUNÇÃO BULKCOPY
        public Boolean TransfereBancoJmoreira(DataTable dtOrigem, String strTblDestino)
        {
            SqlConnection Conexao = PegarConexao();
            Conexao.Open();

            using (SqlBulkCopy bcCarregar = new SqlBulkCopy(Conexao))
            {
                bcCarregar.DestinationTableName =
                    strTblDestino;

                try
                {
                    // Write from the source to the destination.
                    bcCarregar.WriteToServer(dtOrigem);

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    Conexao.Close();
                }
            }


        }
        #endregion


    }
}

using ApiCatalogoJogosData.IRepository;
using ApiCatalogoJogos.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCatalogoJogos.Data.IRepository;

namespace ApiCatalogoJogosData.Repository
{
    public class JogoSql : IJogoRepository
    {
        private readonly SqlConnection sqlConnection;

        public JogoSql(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"update Jogo set Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = {jogo.Preco.ToString().Replace(",", ".")} where Id = '{jogo.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQueryAsync();

            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public Task Inserir(Jogo jogo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            throw new NotImplementedException();
        }

        public Task<Jogo> Obter(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos where Nome = '{nome} and Produtora = '{produtora}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }
            await sqlConnection.CloseAsync();

            return jogos;
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Jogo where Id = '{id}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}

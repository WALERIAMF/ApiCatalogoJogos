using ApiCatalogoJogos.Data.IRepository;
using ApiCatalogoJogos.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Data.Repository
{
    public class JogoSqlServerRepository : IJogoRepository
    {
        private readonly SqlConnection sqlConnection;

        public JogoSqlServerRepository(IConfiguration configuration)
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

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"insert Jogos (id, Nome, Produtora, Preco) values ('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', '{jogo.Preco.ToString().Replace(",", ".")}";

            await sqlConnection.OpenAsync();
            SqlConnection sqlCommand = new SqlConnection(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos order by id offaset {((pagina - 1) * quantidade)} rows fetch next {quantidade} SqlRowsCopiedEventArgs only";

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
                await sqlConnection.CloseAsync();
                return jogos;

            }
        }

        public async Task<Jogo> Obter(Guid id)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogo where Nome = '{nome}' and Produtora = '{produtora}'";

            await sqlConnection.OpenAsync();
            SqlConnection sqlConnection = new SqlConnection(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogo.Add(new Jogo
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

using ApiCatalogoJogos.Data.IRepository;
using ApiCatalogoJogos.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Data.Repository
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("5e9765ce3b42da0001056373"), new Jogo{Id = Guid.Parse("5e9765ce3b42da0001056373"), Nome = "Fifa", Produtora = "EA", Preco = 300} },
            {Guid.Parse("5e970c323b42da0001056361"), new Jogo{Id = Guid.Parse("5e970c323b42da0001056361"), Nome = "Grand Theft Auto V", Produtora = "Rockstar Games", Preco = 400} },
            {Guid.Parse("5eba50742f620a61b0db2283"), new Jogo{Id = Guid.Parse("5eba50742f620a61b0db2283"), Nome = "Fortnite", Produtora = "Epic Games", Preco = 250} },
            {Guid.Parse("5eba50662f620a61b0db2259"), new Jogo{Id = Guid.Parse("5eba50662f620a61b0db2259"), Nome = "Minecraft", Produtora = "Mojang Studios", Preco = 150} },
            {Guid.Parse("5eba589dc4f540a79ce64cc2"), new Jogo{Id = Guid.Parse("5eba589dc4f540a79ce64cc2"), Nome = "Spider-Man", Produtora = "Insomniac Games", Preco = 300} },
            {Guid.Parse("5eba58b4c4f540a79ce64d06"), new Jogo{Id = Guid.Parse("5eba58b4c4f540a79ce64d06"), Nome = "Midway Games", Produtora = "Pac-Man", Preco = 30} }
        };
        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }


        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }
            return Task.FromResult(retorno);
        }
        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}

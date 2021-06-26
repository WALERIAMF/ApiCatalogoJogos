using ApiCatalogoJogos.Domain.ViewModel;
using ApiCatalogoJogos.Service.InputModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Service.InterfaceService
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid grid);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(Guid id, JogoInputModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}

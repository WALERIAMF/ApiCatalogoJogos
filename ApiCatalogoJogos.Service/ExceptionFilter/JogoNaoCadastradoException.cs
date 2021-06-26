using System;

namespace ApiCatalogoJogos.Service.ExceptionFilter
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException() : base("Este jogo não possui cadastro") { }
    }
}

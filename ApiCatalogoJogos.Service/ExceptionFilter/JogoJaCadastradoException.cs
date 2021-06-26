using System;

namespace ApiCatalogoJogos.Service.ExceptionFilter
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException() : base("Este jogo já possui cadastro") { }
    }
}

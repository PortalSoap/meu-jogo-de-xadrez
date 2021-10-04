using System;
using Meu_Jogo_de_Xadrez.TabuleiroNamespace;
using Meu_Jogo_de_Xadrez.JogoDeXadrez;

namespace Meu_Jogo_de_Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez p = new PosicaoXadrez('c', 7);
            try
            {
                Console.WriteLine(p);
                Console.WriteLine(p.ToPosicao());
            }
            catch (TabuleiroException a)
            {
                Console.WriteLine(a.Message);
            }
        }
    }
}

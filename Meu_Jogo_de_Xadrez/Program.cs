using System;
using Meu_Jogo_de_Xadrez.TabuleiroNamespace;

namespace Meu_Jogo_de_Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro a = new Tabuleiro(8, 8);
            Tela.ImprimirTabuleiro(a);
        }
    }
}

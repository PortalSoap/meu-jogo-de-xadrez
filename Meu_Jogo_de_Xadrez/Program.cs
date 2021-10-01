using System;
using Meu_Jogo_de_Xadrez.TabuleiroNamespace;
using Meu_Jogo_de_Xadrez.JogoDeXadrez;

namespace Meu_Jogo_de_Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro t = new Tabuleiro(8, 8);
            try
            {
                t.ColocarPeca(new Torre(t, Cor.Preto), new Posicao(0, 0));
                t.ColocarPeca(new Torre(t, Cor.Preto), new Posicao(1, 3));
                t.ColocarPeca(new Rei(t, Cor.Preto), new Posicao(0, 2));
                Tela.ImprimirTabuleiro(t);
            }
            catch (TabuleiroException a)
            {
                Console.WriteLine(a.Message);
            }
        }
    }
}

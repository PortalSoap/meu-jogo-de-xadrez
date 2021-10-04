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


                t.ColocarPeca(new Torre(t, Cor.Amarelo), new Posicao(0, 0));
                t.ColocarPeca(new Torre(t, Cor.Amarelo), new Posicao(1, 3));
                t.ColocarPeca(new Rei(t, Cor.Amarelo), new Posicao(0, 2));

                t.ColocarPeca(new Torre(t, Cor.Branco), new Posicao(3, 5));

                Tela.ImprimirTabuleiro(t);
            }
            catch (TabuleiroException a)
            {
                Console.WriteLine(a.Message);
            }
        }
    }
}

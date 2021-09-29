using System;
using Meu_Jogo_de_Xadrez.Tabuleiro;

namespace Meu_Jogo_de_Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(5, 3);
            Console.WriteLine($"Posição: {p}");
        }
    }
}

using System;
using Meu_Jogo_de_Xadrez.TabuleiroNamespace;
using Meu_Jogo_de_Xadrez.JogoDeXadrez;

namespace Meu_Jogo_de_Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao origem, destino;
            PartidaDeXadrez p = new PartidaDeXadrez();

            try
            {
                while(p.PartidaEncerrada != true)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirTabuleiro(p.Tabuleiro);
                        Console.WriteLine();
                        Console.WriteLine("Turno: " + p.Turno);
                        Console.WriteLine("Aguardando jogada: " + p.JogadorAtual);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        origem = Tela.LerPosicaoXadrez().ToPosicao();
                        p.ValidarPosicaoOrigem(origem);

                        Console.Clear();
                        bool[,] posicoesPossiveis = p.Tabuleiro.RetornarPeca(origem).MovimentosPossiveis();
                        Tela.ImprimirTabuleiro(p.Tabuleiro, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        destino = Tela.LerPosicaoXadrez().ToPosicao();
                        p.ValidarPosicaoDestino(origem, destino);

                        p.RealizarJogada(origem, destino);
                    }
                    catch(TabuleiroException a)
                    {
                        Console.WriteLine(a.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (TabuleiroException a)
            {
                Console.WriteLine(a.Message);
            }
        }
    }
}

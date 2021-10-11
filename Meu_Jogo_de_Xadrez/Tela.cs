using System;
using System.Collections.Generic;
using Meu_Jogo_de_Xadrez.JogoDeXadrez;
using Meu_Jogo_de_Xadrez.TabuleiroNamespace;

namespace Meu_Jogo_de_Xadrez
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez p)
        {
            ImprimirTabuleiro(p.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(p);
            Console.WriteLine("Turno: " + p.Turno);
            Console.WriteLine("Aguardando jogada: " + p.JogadorAtual);
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças Capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branco));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preto));
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> pecas)
        {
            ConsoleColor corOriginal = Console.ForegroundColor;
            Console.Write("[ ");
            foreach(Peca p in pecas)
            {
                if (p.Cor == Cor.Branco)
                {
                    Console.Write(p + " ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(p + " ");
                    Console.ForegroundColor = corOriginal;
                }
            }
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.RetornarPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if(posicoesPossiveis[i, j] == true)
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tabuleiro.RetornarPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string entrada;
            char coluna;
            int linha;

            entrada = Console.ReadLine();
            coluna = entrada[0];
            linha = int.Parse(entrada[1].ToString());
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca + " ");
                }
                else
                {
                    ConsoleColor defaultColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca + " ");
                    Console.ForegroundColor = defaultColor;
                }
            }
        }
    }
}

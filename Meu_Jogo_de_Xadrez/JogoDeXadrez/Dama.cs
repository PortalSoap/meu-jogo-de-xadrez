using Meu_Jogo_de_Xadrez.TabuleiroNamespace;

namespace Meu_Jogo_de_Xadrez.JogoDeXadrez
{
    class Dama : Peca
    {
        public Dama()
        {
        }

        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.RetornarPeca(posicao);
            if (peca == null || peca.Cor != Cor)
            {
                return true;
            }
            return false;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);


            // Cima.
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Linha -= 1;
            }

            // Diagonal superior direita.
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
            }

            // Direita.
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Coluna += 1;
            }

            // Diagonal inferior direita.
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
            }

            // Baixo.
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Linha += 1;
            }

            // Diagonal inferior esquerda.
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
            }

            // Esquerda.
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Coluna -= 1;
            }

            // Diagonal superior esquerda.
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.RetornarPeca(posicao) != null && Tabuleiro.RetornarPeca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
            }

            return matriz;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}

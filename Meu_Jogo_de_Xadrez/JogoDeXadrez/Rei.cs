using Meu_Jogo_de_Xadrez.TabuleiroNamespace;

namespace Meu_Jogo_de_Xadrez.JogoDeXadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez _partida;

        public Rei()
        {
        }

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }

        public bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.RetornarPeca(posicao);
            if(peca == null || peca.Cor != Cor)
            {
                return true;
            }
            return false;
        }

        private bool TesteDaTorreParaRoque(Posicao posicao)
        {
            Peca p = Tabuleiro.RetornarPeca(posicao);
            if (p != null && p is Torre && p.Cor == Cor && p.QuantidadeDeMovimentos == 0)
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
            if(Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Diagonal superior direita.
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Direita.
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Diagonal inferior direita.
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Baixo.
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Diagonal inferior esquerda.
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Esquerda.
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // Diagonal superior esquerda.
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // JOGADA ESPECIAL: Roque.
            if(QuantidadeDeMovimentos == 0 && _partida.Xeque == false)
            {
                // Roque pequeno.
                Posicao posicaoTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if(TesteDaTorreParaRoque(posicaoTorre1) == true)
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if(Tabuleiro.RetornarPeca(p1) == null && Tabuleiro.RetornarPeca(p2) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // Roque grande.
                Posicao posicaoTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteDaTorreParaRoque(posicaoTorre2) == true)
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.RetornarPeca(p1) == null && Tabuleiro.RetornarPeca(p2) == null &&
                        Tabuleiro.RetornarPeca(p3) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return matriz;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}

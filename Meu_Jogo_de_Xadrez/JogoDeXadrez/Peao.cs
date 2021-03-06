using Meu_Jogo_de_Xadrez.TabuleiroNamespace;

namespace Meu_Jogo_de_Xadrez.JogoDeXadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez _partida;

        public Peao()
        {
        }

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca p = Tabuleiro.RetornarPeca(posicao);
            if(p != null && p.Cor != Cor)
            {
                return true;
            }
            return false;
        }

        private bool FrenteLivre(Posicao posicao)
        {
            if(Tabuleiro.RetornarPeca(posicao) == null)
            {
                return true;
            }
            return false;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // Regras para peça branca.
            if(Cor == Cor.Branco)
            {
                // Movimento comum.
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if(Tabuleiro.PosicaoValida(posicao) && FrenteLivre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Primeiro movimento.
                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && FrenteLivre(posicao) && QuantidadeDeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Diagonal superior esquerda (eliminar inimigo)
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Diagonal superior direita (eliminar inimigo)
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // JOGADA ESPECIAL:En Passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if(Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.RetornarPeca(esquerda) == _partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) && Tabuleiro.RetornarPeca(direita) == _partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }

            // Regras para peça preta.
            else
            {
                // Movimento comum.
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && FrenteLivre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Primeiro movimento.
                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && FrenteLivre(posicao) && QuantidadeDeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Diagonal inferior esquerda (eliminar inimigo)
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Diagonal inferior direita (eliminar inimigo)
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // JOGADA ESPECIAL:En Passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.RetornarPeca(esquerda) == _partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) && Tabuleiro.RetornarPeca(direita) == _partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}

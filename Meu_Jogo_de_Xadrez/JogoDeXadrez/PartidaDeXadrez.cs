using System.Collections.Generic;

using Meu_Jogo_de_Xadrez.TabuleiroNamespace;

namespace Meu_Jogo_de_Xadrez.JogoDeXadrez
{
    class PartidaDeXadrez
    {
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool PartidaEncerrada { get; private set; }
        public Tabuleiro Tabuleiro { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _pecasCapturadas;
        public Peca VulneravelEnPassant { get; private set; }
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            PartidaEncerrada = false;
            Xeque = false;
            _pecas = new HashSet<Peca>();
            _pecasCapturadas = new HashSet<Peca>();
            VulneravelEnPassant = null;
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca, pecaCapturada;

            peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQtdMovimento();
            pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);
            }

            // JOGADA ESPECIAL: Roque pequeno.
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemTorre);
                T.IncrementarQtdMovimento();
                Tabuleiro.ColocarPeca(T, destinoTorre);
            }

            // JOGADA ESPECIAL: Roque grande.
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(origemTorre);
                T.IncrementarQtdMovimento();
                Tabuleiro.ColocarPeca(T, destinoTorre);
            }

            // JOGADA ESPECIAL: En Passant.
            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(peca.Cor == Cor.Branco)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }

                    pecaCapturada = Tabuleiro.RetirarPeca(posP);
                    _pecasCapturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementarQtdMovimento();

            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                _pecasCapturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(peca, origem);

            // JOGADA ESPECIAL: Roque pequeno.
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemTorre);
                T.DecrementarQtdMovimento();
                Tabuleiro.ColocarPeca(T, destinoTorre);
            }

            // JOGADA ESPECIAL: Roque grande.
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(origemTorre);
                T.DecrementarQtdMovimento();
                Tabuleiro.ColocarPeca(T, destinoTorre);
            }

            // JOGADA ESPECIAL: En Passant.
            if(peca is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posP;
                    if(peca.Cor == Cor.Branco)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }

                    Tabuleiro.ColocarPeca(peao, posP);
                }
            }
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);
            if (VerificarXeque(JogadorAtual) == true)
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca peca = Tabuleiro.RetornarPeca(destino);

            // JOGADA ESPECIAL: Promoção.
            if(peca is Peao)
            {
                if (peca.Cor == Cor.Branco && destino.Linha == 0
                    || (peca.Cor == Cor.Preto && destino.Linha == 7))
                {
                    peca = Tabuleiro.RetirarPeca(destino);
                    _pecas.Remove(peca);
                    Peca dama = new Dama(Tabuleiro, peca.Cor);
                    Tabuleiro.ColocarPeca(dama, destino);
                    _pecas.Add(dama);
                }
            }

            if (VerificarXeque(RetornarPecaAdversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequemate(RetornarPecaAdversaria(JogadorAtual)))
            {
                PartidaEncerrada = true;
            }
            else
            {
                Turno += 1;
                MudarJogador();
            }

            // JOGADA ESPECIAL:En Passant.
            if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = peca;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            if (Tabuleiro.RetornarPeca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (Tabuleiro.RetornarPeca(posicao).Cor != JogadorAtual)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (Tabuleiro.RetornarPeca(posicao).ExistemMovimentosPossiveis() != true)
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (Tabuleiro.RetornarPeca(origem).PodeMoverParaDestino(destino) != true)
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudarJogador()
        {
            if (JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in _pecasCapturadas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in _pecas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor RetornarPecaAdversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca RetornarRei(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if (p is Rei)
                {
                    return p;
                }
            }
            return null;
        }

        public bool VerificarXeque(Cor cor)
        {
            Peca r = RetornarRei(cor);

            if (r == null)
            {
                throw new TabuleiroException($"Não existe rei da cor {cor} neste tabuleiro!");
            }

            foreach (Peca p in PecasEmJogo(RetornarPecaAdversaria(cor)))
            {
                bool[,] matriz = p.MovimentosPossiveis();
                if (matriz[r.Posicao.Linha, r.Posicao.Coluna] == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if (VerificarXeque(cor) != true)
            {
                return false;
            }

            foreach (Peca p in PecasEmJogo(cor))
            {
                bool[,] matriz = p.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (matriz[i, j] == true)
                        {
                            Posicao origem = p.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = VerificarXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if (testeXeque != true)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            // Peças Brancas.
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branco, this));

            // Peças Pretas.
            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preto, this));
        }
    }
}

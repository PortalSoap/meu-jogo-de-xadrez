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
            ColocarPecas();
        }

        public Peca ExecutarMoviento(Posicao origem, Posicao destino)
        {
            Peca peca, pecaCapturada;

            peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQtdMovimento();
            pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if(pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);    
            }
            return pecaCapturada;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);
            p.DecrementarQtdMovimento();

            if(pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                _pecasCapturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(p, origem);
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMoviento(origem, destino);
            if(VerificarXeque(JogadorAtual) == true)
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (VerificarXeque(RetornarPecaAdversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            Turno += 1;
            MudarJogador();
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
            foreach(Peca p in _pecasCapturadas)
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
            foreach(Peca p in _pecas)
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
            if(cor == Cor.Branco)
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
                if(p is Rei)
                {
                    return p;
                }
            }
            return null;
        }

        public bool VerificarXeque(Cor cor)
        {
            Peca r = RetornarRei(cor);

            if(r == null)
            {
                throw new TabuleiroException($"Não existe rei da cor {cor} neste tabuleiro!");
            }

            foreach(Peca p in PecasEmJogo(RetornarPecaAdversaria(cor)))
            {
                bool[,] matriz = p.MovimentosPossiveis();
                if(matriz[r.Posicao.Linha, r.Posicao.Coluna] == true)
                {
                    return true;
                }
            }
            return false;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            // Peças Brancas.
            ColocarNovaPeca('c', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 2, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 2, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 2, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rei(Tabuleiro, Cor.Branco));

            // Peças Pretas.
            ColocarNovaPeca('c', 7, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('c', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 7, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 7, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 8, new Rei(Tabuleiro, Cor.Preto));
        }
    }
}

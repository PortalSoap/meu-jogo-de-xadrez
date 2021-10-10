namespace Meu_Jogo_de_Xadrez.TabuleiroNamespace
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca()
        {
        }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Tabuleiro = tabuleiro;
            Cor = cor;
            QuantidadeDeMovimentos = 0;
            Posicao = null;
        }

        public abstract bool[,] MovimentosPossiveis();

        public void IncrementarQtdMovimento()
        {
            QuantidadeDeMovimentos++;
        }

        public bool ExistemMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();
            for(int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for(int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if(matriz[i, j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PodeMoverParaDestino(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }
    }
}

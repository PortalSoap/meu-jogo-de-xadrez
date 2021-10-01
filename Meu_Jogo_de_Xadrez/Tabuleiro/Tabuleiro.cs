namespace Meu_Jogo_de_Xadrez.TabuleiroNamespace
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro()
        {
        }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[Linhas, Colunas];
        }

        // Métodos relacionados a peças.
        public Peca RetornarPeca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca RetornarPeca(Posicao posicao)
        {
            return _pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição.");
            }
            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            if(_pecas[posicao.Linha, posicao.Coluna] != null)
            {
                return true;
            }
            return false;
        }

        // Verificação de Posição.
        public bool PosicaoValida(Posicao posicao)
        {
            if(posicao.Linha < 0 || posicao.Coluna <0 ||
                posicao.Linha >= Linhas || posicao.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if(PosicaoValida(posicao) != true)
            {
                throw new TabuleiroException("Posição Inválida.");
            }
        }
    }
}

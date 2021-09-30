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
            Linhas = Linhas;
            Colunas = Colunas;
            _pecas = new Peca[Linhas, Colunas];
        }
    }
}

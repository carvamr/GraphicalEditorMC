using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditorMC
{
    class Comando
    {
        int M, N, X, X1, X2, Y, Y1, Y2 = 0;
        string[,] imagem;
        string cor;


        internal List<string> tratar(string comando)
        {
            List<string> comm_dict = new List<string>();

            //transforma comando numa lista
            //1o elemento e o comando principal. restantes elementos sao os argumentos do comando
            comm_dict = comando.Split(' ').ToList();

            //validacoes ao comando inserido
            testesUnitarios(ref comm_dict);

            //devolve comando ou lista vazia
            return comm_dict;
        }

        private void testesUnitarios(ref List<string> comm_dict)
        {
            try
            {
                //validacoes
                switch (comm_dict[0].ToUpper())
                {
                    case "I":
                        if (comm_dict.Count == 3 &&
                            Convert.ToInt32(comm_dict[1]) >= 1 &&
                            Convert.ToInt32(comm_dict[1]) <= 250 &&
                            Convert.ToInt32(comm_dict[2]) >= 1 &&
                            Convert.ToInt32(comm_dict[2]) <= 250) return;
                        break;
                    case "C":
                    case "S":
                        if (comm_dict.Count == 1 &&
                            imagem.Length > 0) return;
                        break;
                    case "L":
                    case "F":
                        if (comm_dict.Count == 4 &&
                            imagem.Length > 0 &&
                            Convert.ToInt32(comm_dict[1]) >= 1 &&
                            Convert.ToInt32(comm_dict[1]) <= imagem.GetLength(0) &&
                            Convert.ToInt32(comm_dict[2]) >= 1 &&
                            Convert.ToInt32(comm_dict[2]) <= imagem.GetLength(1)) return;
                        break;
                    case "V":
                        if (comm_dict.Count == 5 &&
                           imagem.Length > 0 &&
                           Convert.ToInt32(comm_dict[1]) >= 1 &&
                           Convert.ToInt32(comm_dict[1]) <= imagem.GetLength(0) &&
                           Convert.ToInt32(comm_dict[2]) >= 1 &&
                           Convert.ToInt32(comm_dict[2]) <= imagem.GetLength(1) &&
                           Convert.ToInt32(comm_dict[3]) >= 1 &&
                           Convert.ToInt32(comm_dict[3]) <= imagem.GetLength(1)) return;
                        break;
                    case "H":
                        if (comm_dict.Count == 5 &&
                           imagem.Length > 0 &&
                           Convert.ToInt32(comm_dict[1]) >= 1 &&
                           Convert.ToInt32(comm_dict[1]) <= imagem.GetLength(0) &&
                           Convert.ToInt32(comm_dict[2]) >= 1 &&
                           Convert.ToInt32(comm_dict[2]) <= imagem.GetLength(0) &&
                           Convert.ToInt32(comm_dict[3]) >= 1 &&
                           Convert.ToInt32(comm_dict[3]) <= imagem.GetLength(1)) return;
                        break;
                    case "X":
                        if (comm_dict.Count == 1) return;
                        break;
                    default:
                        break;
                }
                //caso o comando nao passe nas validacoes ou caso de alguma excecao, limpa conteudo
                comm_dict.Clear();
            }
            catch
            {
                comm_dict.Clear();
            }
        }

        internal void executar(ref List<string> comando)
        {
            try
            {
                switch (comando[0].ToUpper())
                {
                    case "I":
                        //I M N. Create a new M x N image with all pixels coloured white (O).
                        M = Convert.ToInt32(comando[1]);
                        N = Convert.ToInt32(comando[2]);
                        imagem = new string[M, N];
                        colorirArea(imagem, M, N);
                        break;
                    case "C":
                        //C. Clears the table, setting all pixels to white (O).
                        colorirArea(imagem, imagem.GetLength(0), imagem.GetLength(1));
                        break;
                    case "L":
                        //L X Y C. Colours the pixel (X,Y) with colour C.
                        X = Convert.ToInt32(comando[1]);
                        Y = Convert.ToInt32(comando[2]);
                        cor = comando[3];
                        colorirArea(imagem, X, Y, cor, X - 1, Y - 1);
                        break;
                    case "V":
                        //V X Y1 Y2 C. Draw a vertical segment of colour C in column X between rows Y1 and Y2 (inclusive).
                        X = Convert.ToInt32(comando[1]);
                        Y1 = Convert.ToInt32(comando[2]);
                        Y2 = Convert.ToInt32(comando[3]);
                        cor = comando[4];
                        colorirArea(imagem, X, Y2, cor, X - 1, Y1 - 1);
                        break;
                    case "H":
                        //H X1 X2 Y C. Draw a horizontal segment of colour C in row Y between columns X1 and X2 (inclusive).
                        X1 = Convert.ToInt32(comando[1]);
                        X2 = Convert.ToInt32(comando[2]);
                        Y = Convert.ToInt32(comando[3]);
                        cor = comando[4];
                        colorirArea(imagem, X2, Y, cor, X1 - 1, Y - 1);
                        break;
                    case "F":
                        //F X Y C. Fill the region R with the colour C. R is defined as: Pixel (X,Y) belongs to R. Any other
                        //pixel which is the same colour as (X, Y) and shares a common side with any pixel in R also belongs
                        //to this region.
                        X = Convert.ToInt32(comando[1]);
                        Y = Convert.ToInt32(comando[2]);
                        cor = comando[3];
                        preencherImagem(imagem, X - 1, Y - 1, cor);
                        break;
                    case "S":
                        //S. Show the contents of the current image
                        mostrarImagem(imagem);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Unprocessed Exception!");
            }
        }

        private void colorirArea(string[,] imagem, int coluna_fim, int linha_fim, string cor = "O", int coluna_inicio = 0, int linha_inicio = 0)
        {
            for (int i = linha_inicio; i < linha_fim; i++)
            {
                for (int j = coluna_inicio; j < coluna_fim; j++)
                {
                    imagem[j, i] = cor;
                }
            }
        }

        private void preencherImagem(string[,] imagem, int x, int y, string cor)
        {
            string cor_antiga = imagem[x, y];
            preencherVizinhos(imagem, x, y, cor, cor_antiga);
        }

        private void preencherVizinhos(string[,] imagem, int x, int y, string cor, string cor_antiga)
        {
            if (imagem[x, y].Equals(cor_antiga)) imagem[x, y] = cor;

            if (x - 1 >= 0 && imagem[x - 1, y].Equals(cor_antiga))
            {
                imagem[x - 1, y] = cor;
                preencherVizinhos(imagem, x - 1, y, cor, cor_antiga);
            }
            if (y - 1 >= 0 && imagem[x, y - 1].Equals(cor_antiga))
            {
                imagem[x, y - 1] = cor;
                preencherVizinhos(imagem, x, y - 1, cor, cor_antiga);
            }
            if (x + 1 < imagem.GetLength(0) && imagem[x + 1, y].Equals(cor_antiga))
            {
                imagem[x + 1, y] = cor;
                preencherVizinhos(imagem, x + 1, y, cor, cor_antiga);
            }
            if (y + 1 < imagem.GetLength(1) && imagem[x, y + 1].Equals(cor_antiga))
            {
                imagem[x, y + 1] = cor;
                preencherVizinhos(imagem, x, y + 1, cor, cor_antiga);
            }
        }

        private void mostrarImagem(string[,] imagem)
        {
            for (int i = 0; i < imagem.GetLength(1); i++)
            {
                for (int j = 0; j < imagem.GetLength(0); j++)
                {
                    Console.Write(imagem[j, i]);
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}

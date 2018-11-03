using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditorMC
{
    class Program
    {
        private static Comando comando = new Comando();

        static void Main(string[] args)
        {
            string commandUserInput;
            List<string> comm_dict = new List<string>();
            Console.WriteLine("Welcome to Mauro Carvalho's Graphical Editor" + Environment.NewLine +
                "Image positions vary from 1 to M/N (columns/rows)" + Environment.NewLine + Environment.NewLine +
                "Available Commands:" + Environment.NewLine +
                "(I) - I M N. Create a new M x N image with all pixels coloured white (O)." + Environment.NewLine +
                "(C) - C. Clears the table, setting all pixels to white (O)." + Environment.NewLine +
                "(L) - L X Y C. Colours the pixel (X,Y) with colour C." + Environment.NewLine +
                "(V) - V X Y1 Y2 C. Draw a vertical segment of colour C in column X between rows Y1 and Y2 (inclusive)." + Environment.NewLine +
                "(H) - H X1 X2 Y C. Draw a horizontal segment of colour C in row Y between columns X1 and X2 (inclusive)." + Environment.NewLine +
                "(F) - F X Y C. Fill the region R with the colour C. R is defined as: Pixel (X,Y) belongs to R. Any other pixel which is the same colour as (X,Y) and shares a common side with any pixel in R also belongs to this region." + Environment.NewLine +
                "(S) - S. Show the contents of the current image" + Environment.NewLine +
                "(X) - X. Terminate the session" + Environment.NewLine + Environment.NewLine +
                "Insert commands:");

            do
            {
                commandUserInput = Console.ReadLine();
                comm_dict = comando.tratar(commandUserInput);

                if (comm_dict.Count == 0)
                {
                    Console.WriteLine("Error in Command!");
                    continue;
                }

                comando.executar(ref comm_dict);
            }
            while (!commandUserInput.ToUpper().Equals("X"));
        }
    }
}

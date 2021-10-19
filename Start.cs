using System;

namespace NN
{
    internal class Start
    {
        public static void Main(string[] args)
        {
            DatasetGenerator d = new DatasetGenerator(44);

			while (true)
			{
				Console.WriteLine("Supported Operations:\n" +
					"1 - Single threaded write to disk\n" +
					"2 - Octa threaded write to disk\n" +
					"3 - Octa threaded write to array -> disk\n" +
					"4 - Read file\n" +
					"5 - Execute neural network");

				int choice = int.Parse(Console.ReadLine());

				if (choice == 1 || choice == 2 || choice == 3)
				{
					Console.WriteLine("Gib die Größe des Datensatzes an (5 000 000 benötigt ca. 1,4GB): ");
					int lines = int.Parse(Console.ReadLine());

					if (choice == 1)
					{
						d.WriteToFile(lines);
					}
					else if (choice == 2)
					{
						d.ThreadedWriteToFile(lines);
					}
					else if (choice == 3)
					{
						//Console.WriteLine("Number of Threads?");
						d.ThreadedWriteToFileArray(lines);
					}
				}
				else if (choice == 4)
				{
					Console.WriteLine("Dateiname?");
					var fileName = Console.ReadLine();

					double[] buffer;

                    do
                    {
						buffer = d.ReadLine(fileName);
						for(int i = 0; i < buffer.Length; i++)
                        {
							Console.Write(buffer[i] + " ");
                        }
						Console.WriteLine();
                    }while(buffer != default);

				}
				else if (choice == 5)
				{
					Netzwerk n = new Netzwerk();

					Console.WriteLine("Enter learning rate:");
					double learningRate = double.Parse(Console.ReadLine());

					Console.WriteLine("Enter desired iteration count:");
					int iterationCount = int.Parse(Console.ReadLine());

					InputExport:
						Console.WriteLine("Export weights when the NN is done? Y/N");
						string export = Console.ReadLine();
						if (!(export.Equals("Y") || export.Equals("N")))
							goto InputExport;

					InputImport:
						Console.WriteLine("Import weights from a previous run? Y/N");
						string import = Console.ReadLine();
						if (!import.Equals("Y") && !import.Equals("N"))
							goto InputImport;

					if (import.Equals("Y"))
					{
						Console.WriteLine("Filename of weights to import? Y/N");
						string filename = Console.ReadLine();

						n.ImportWeightsFromFile(filename);
					}

				InputPrecalc:
					Console.WriteLine("Use precalculated dataset?");
					string precalculated = Console.ReadLine();

					if (!(precalculated.Equals("Y") || precalculated.Equals("N")))
						goto InputPrecalc;

					if (precalculated.Equals("Y"))
					{
						Console.WriteLine("Enter filename of precalculated dataset:");
						string filenamePrecalc = Console.ReadLine();

						Console.WriteLine();

						n.MutierenFile(learningRate, iterationCount, filenamePrecalc);
					}
					else
                    {
						Console.WriteLine();
						n.MutierenRT(learningRate, iterationCount);
                    }

					if (export.Equals("Y"))
					{
						n.WriteWeightsToFile();
					}

					Console.WriteLine();
				}
			}
            /*Netzwerk n = new Netzwerk();
			n.Mutieren(0.1, 1000000);
			n.WriteWeightsToFile();
			Console.Read();

			Console.WriteLine();

            /*Form1 form = new Form1();
            form.ShowDialog();*/
        }
    }
}

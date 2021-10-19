using System;
using System.IO;

namespace NN
{
    class Netzwerk
    {
        Random r;
        InputNode[] inputNodes = new InputNode[16];
        Node[] hiddenNodes = new Node[4];
        Node outputNode;
        DatasetGenerator d;

        public Netzwerk()
        {
            r = new Random();

            for (int i = 0; i < inputNodes.Length; i++)
            {
                inputNodes[i] = new InputNode(r.NextDouble());
            }
            for (int j = 0; j < hiddenNodes.Length; j++)
            {
                hiddenNodes[j] = new Node(r.NextDouble());
                hiddenNodes[j].SetOperation(Operationen.MULTIPLIKATION);
            }

            outputNode = new Node(r.NextDouble());

            d = new DatasetGenerator(r.Next());
        }

        public double Test(double[] pLine, InputNode[] pInputNodes, Node[] pHiddenNodes, Node pOutputNode)
        {
            double[] outputInputNodes = new double[16];

            for (int i = 0; i < pInputNodes.Length; i++)
            {
                pInputNodes[i].SetInput(pLine[i]);
                outputInputNodes[i] = pInputNodes[i].GetOutput();
                //Console.WriteLine(i + ": " + pInputNodes[i].GetOutput());
            }

            double[] outputHiddenNodes = new double[4];
            int position = 0;

            for (int j = 0; j < pHiddenNodes.Length; j++)
            {
                pHiddenNodes[j].SetInputs(outputInputNodes[position], outputInputNodes[position + 1], outputInputNodes[position + 2], outputInputNodes[position + 3]);
                outputHiddenNodes[j] = pHiddenNodes[j].GetOutput();
                //Console.WriteLine(j + ": " + pHiddenNodes[j].GetOutput());

                position += 4;
            }

            pOutputNode.SetInputs(outputHiddenNodes[0], outputHiddenNodes[1], outputHiddenNodes[2], outputHiddenNodes[3]);

            return Math.Abs(pLine[16] - pOutputNode.GetOutput());
        }

        public void MutierenFile(double pLernrate, int pDurchgaenge, string pFilename)
        {
            double abweichungHochInput = 0;
            double abweichungTiefInput = 0;
            double abweichungInput = 0;
            double abweichungHochHidden = 0;
            double abweichungTiefHidden = 0;
            double abweichungHidden = 0;
            double abweichungHoch = 0;
            double abweichungTief = 0;
            double abweichung = 0;

            for (int j = 0; j < pDurchgaenge; j++)
            {
                var line = d.ReadLine(pFilename);

                for (int i = 0; i < inputNodes.Length; i++)
                {
                    //var line = d.ErzeugeTestdaten();

                    InputNode[] inputNodesHoch = new InputNode[16];

                    for (int a = 0; a < inputNodesHoch.Length; a++)
                    {
                        inputNodesHoch[a] = new InputNode(inputNodes[a].GetWeight());
                    }

                    inputNodesHoch[i].ModifyWeight(1 + pLernrate);

                    InputNode[] inputNodesTief = new InputNode[16];

                    for (int a = 0; a < inputNodesTief.Length; a++)
                    {
                        inputNodesTief[a] = new InputNode(inputNodes[a].GetWeight());
                    }

                    inputNodesTief[i].ModifyWeight(1 - pLernrate);

                    abweichungHochInput = Test(line, inputNodesHoch, hiddenNodes, outputNode);
                    abweichungInput = Test(line, inputNodes, hiddenNodes, outputNode);
                    abweichungTiefInput = Test(line, inputNodesTief, hiddenNodes, outputNode);

                    if (abweichungInput >= abweichungHochInput)
                    {
                        if (abweichungHochInput >= abweichungTiefInput)
                        {
                            inputNodes = inputNodesTief;
                        }
                        else
                        {
                            inputNodes = inputNodesHoch;
                        }
                    }
                    else if (abweichungInput >= abweichungTiefInput)
                    {
                        inputNodes = inputNodesTief;
                    }
                }

                for (int i = 0; i < hiddenNodes.Length; i++)
                {
                    //var line = d.ErzeugeTestdaten();

                    Node[] hiddenNodesHoch = new Node[4];

                    for (int a = 0; a < hiddenNodesHoch.Length; a++)
                    {
                        hiddenNodesHoch[a] = new Node(hiddenNodes[a].GetWeight());
                    }

                    hiddenNodesHoch[i].ModifyWeight(1 + pLernrate);

                    Node[] hiddenNodesTief = new Node[4];

                    for (int a = 0; a < hiddenNodesTief.Length; a++)
                    {
                        hiddenNodesTief[a] = new Node(hiddenNodes[a].GetWeight());
                    }

                    hiddenNodesTief[i].ModifyWeight(1 - pLernrate);

                    abweichungHochHidden = Test(line, inputNodes, hiddenNodesHoch, outputNode);
                    abweichungHidden = Test(line, inputNodes, hiddenNodes, outputNode);
                    abweichungTiefHidden = Test(line, inputNodes, hiddenNodesTief, outputNode);

                    if (abweichungHidden >= abweichungHochHidden)
                    {
                        if (abweichungHochHidden >= abweichungTiefHidden)
                        {
                            hiddenNodes = hiddenNodesTief;
                        }
                        else
                        {
                            hiddenNodes = hiddenNodesHoch;
                        }
                    }
                    else if (abweichungHidden >= abweichungTiefHidden)
                    {
                        hiddenNodes = hiddenNodesTief;
                    }
                }

                Node outputNodeHoch = new Node(outputNode.GetWeight());
                Node outputNodeTief = new Node(outputNode.GetWeight());

                outputNodeHoch.ModifyWeight(1 + pLernrate);
                outputNodeTief.ModifyWeight(1 - pLernrate);

                abweichungHoch = Test(line, inputNodes, hiddenNodes, outputNodeHoch);
                abweichung = Test(line, inputNodes, hiddenNodes, outputNode);
                abweichungTief = Test(line, inputNodes, hiddenNodes, outputNodeTief);

                if (abweichung >= abweichungHoch)
                {
                    if (abweichungHoch >= abweichungTief)
                    {
                        outputNode = outputNodeTief;
                    }
                    else
                    {
                        outputNode = outputNodeHoch;
                    }
                }
                else if (abweichung >= abweichungTief)
                {
                    outputNode = outputNodeTief;
                }
            }

            Console.WriteLine("AbweichungInput: " + abweichungInput + "\t\tAbweichungHoch: " + abweichungHochInput + "\t\tAbweichungTief: " + abweichungTiefInput);
            Console.WriteLine("AbweichungHidden: " + abweichungHidden + "\t\tAbweichungHoch: " + abweichungHochHidden + "\t\tAbweichungTief: " + abweichungTiefHidden);
            Console.WriteLine("AbweichungOutput: " + abweichung + "\t\tAbweichungHoch: " + abweichungHoch + "\t\tAbweichungTief: " + abweichungTief);
        }

        public void MutierenRT(double pLernrate, int pDurchgaenge)
        {
            double abweichungHochInput = 0;
            double abweichungTiefInput = 0;
            double abweichungInput = 0;
            double abweichungHochHidden = 0;
            double abweichungTiefHidden = 0;
            double abweichungHidden = 0;
            double abweichungHoch = 0;
            double abweichungTief = 0;
            double abweichung = 0;

            for (int j = 0; j < pDurchgaenge; j++)
            {
                var line = d.ErzeugeTestdaten();

                for (int i = 0; i < inputNodes.Length; i++)
                {
                    //var line = d.ErzeugeTestdaten();

                    InputNode[] inputNodesHoch = new InputNode[16];

                    for (int a = 0; a < inputNodesHoch.Length; a++)
                    {
                        inputNodesHoch[a] = new InputNode(inputNodes[a].GetWeight());
                    }

                    inputNodesHoch[i].ModifyWeight(1 + pLernrate);

                    InputNode[] inputNodesTief = new InputNode[16];

                    for (int a = 0; a < inputNodesTief.Length; a++)
                    {
                        inputNodesTief[a] = new InputNode(inputNodes[a].GetWeight());
                    }

                    inputNodesTief[i].ModifyWeight(1 - pLernrate);

                    abweichungHochInput = Test(line, inputNodesHoch, hiddenNodes, outputNode);
                    abweichungInput = Test(line, inputNodes, hiddenNodes, outputNode);
                    abweichungTiefInput = Test(line, inputNodesTief, hiddenNodes, outputNode);

                    if (abweichungInput >= abweichungHochInput)
                    {
                        if (abweichungHochInput >= abweichungTiefInput)
                        {
                            inputNodes = inputNodesTief;
                        }
                        else
                        {
                            inputNodes = inputNodesHoch;
                        }
                    }
                    else if (abweichungInput >= abweichungTiefInput)
                    {
                        inputNodes = inputNodesTief;
                    }
                }

                for (int i = 0; i < hiddenNodes.Length; i++)
                {
                    //var line = d.ErzeugeTestdaten();

                    Node[] hiddenNodesHoch = new Node[4];

                    for (int a = 0; a < hiddenNodesHoch.Length; a++)
                    {
                        hiddenNodesHoch[a] = new Node(hiddenNodes[a].GetWeight());
                    }

                    hiddenNodesHoch[i].ModifyWeight(1 + pLernrate);

                    Node[] hiddenNodesTief = new Node[4];

                    for (int a = 0; a < hiddenNodesTief.Length; a++)
                    {
                        hiddenNodesTief[a] = new Node(hiddenNodes[a].GetWeight());
                    }

                    hiddenNodesTief[i].ModifyWeight(1 - pLernrate);

                    abweichungHochHidden = Test(line, inputNodes, hiddenNodesHoch, outputNode);
                    abweichungHidden = Test(line, inputNodes, hiddenNodes, outputNode);
                    abweichungTiefHidden = Test(line, inputNodes, hiddenNodesTief, outputNode);

                    if (abweichungHidden >= abweichungHochHidden)
                    {
                        if (abweichungHochHidden >= abweichungTiefHidden)
                        {
                            hiddenNodes = hiddenNodesTief;
                        }
                        else
                        {
                            hiddenNodes = hiddenNodesHoch;
                        }
                    }
                    else if (abweichungHidden >= abweichungTiefHidden)
                    {
                        hiddenNodes = hiddenNodesTief;
                    }
                }

                Node outputNodeHoch = new Node(outputNode.GetWeight());
                Node outputNodeTief = new Node(outputNode.GetWeight());

                outputNodeHoch.ModifyWeight(1 + pLernrate);
                outputNodeTief.ModifyWeight(1 - pLernrate);

                abweichungHoch = Test(line, inputNodes, hiddenNodes, outputNodeHoch);
                abweichung = Test(line, inputNodes, hiddenNodes, outputNode);
                abweichungTief = Test(line, inputNodes, hiddenNodes, outputNodeTief);

                if (abweichung >= abweichungHoch)
                {
                    if (abweichungHoch >= abweichungTief)
                    {
                        outputNode = outputNodeTief;
                    }
                    else
                    {
                        outputNode = outputNodeHoch;
                    }
                }
                else if (abweichung >= abweichungTief)
                {
                    outputNode = outputNodeTief;
                }
            }

            Console.WriteLine("AbweichungInput: " + abweichungInput + "\t\tAbweichungHoch: " + abweichungHochInput + "\t\tAbweichungTief: " + abweichungTiefInput);
            Console.WriteLine("AbweichungHidden: " + abweichungHidden + "\t\tAbweichungHoch: " + abweichungHochHidden + "\t\tAbweichungTief: " + abweichungTiefHidden);
            Console.WriteLine("AbweichungOutput: " + abweichung + "\t\tAbweichungHoch: " + abweichungHoch + "\t\tAbweichungTief: " + abweichungTief);
        }

        /*
		 * ----------------------------------------------Ende des eigentlichen Modells, von hier Komfortfunktionen----------------------------------------------
		 */

        public void WriteWeightsToFile()
        {
            var fs = File.Open(GetFilename(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var sw = new StreamWriter(fs);

            string inputNodesString = "";

            for (int i = 0; i < inputNodes.Length; i++)
            {
                inputNodesString += inputNodes[i].GetWeight().ToString() + " ";
            }

            sw.WriteLine(inputNodesString);

            string hiddenNodesString = "";

            for (int i = 0; i < hiddenNodes.Length; i++)
            {
                hiddenNodesString += hiddenNodes[i].GetWeight().ToString() + " ";
            }

            sw.WriteLine(hiddenNodesString);

            sw.WriteLine(outputNode.GetWeight().ToString());

            sw.Close();
            fs.Close();
        }

        public void ImportWeightsFromFile(string pFilename)
        {
            var fs = File.OpenRead(pFilename);
            var sr = new StreamReader(fs);

            var line = sr.ReadLine();

            for (int i = 0; i < inputNodes.Length; i++)
            {
                var index = line.IndexOf(' ');
                inputNodes[i] = new InputNode(double.Parse(line.Substring(0, index)));

                line = line.Substring(index + 1);
            }

            line = sr.ReadLine();

            for (int i = 0; i < hiddenNodes.Length; i++)
            {
                var index = line.IndexOf(' ');
                hiddenNodes[i] = new Node(double.Parse(line.Substring(0, index)));

                line = line.Substring(index + 1);
            }

            line = sr.ReadLine();

            outputNode = new Node(double.Parse(line));
        }

        /**<summary>Hilfsmethode, die einen Dateinamen des Formats "NeuNet%Uhrzeit%.weights" zurückgibt.</summary>
		 * <returns>Einen <c>String</c>, der den passenden Dateinamen enthält.</returns>
		 */
        public string GetFilename()
        {
            //Speichert einen aktuellen Zeitstempel und entfernt die Datumskomponente.
            string time = DateTime.Now.ToString();
            int index = time.IndexOf(" ");
            time = time.Remove(0, index + 1);
            //Ersetzt für die Kompatibilität die Doppelpunkte durch Unterstriche.
            time = time.Replace(':', '_');
            string fileName = @"NeuNet" + @time + @".weights";

            return fileName;
        }
    }
}

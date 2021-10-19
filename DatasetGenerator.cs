using System;
using System.IO;
using System.Threading;

namespace NN
{
    internal class DatasetGenerator
    {
        private readonly Random r;
        private readonly int seed;

        //Seed ermöglicht dem Anwender das Verhindern von "Übertrainieren" nach einer Unterbrechung des Lernprozesses
        internal DatasetGenerator(int pSeed)
        {
            r = new Random(pSeed);
            seed = pSeed;
        }

        /**<summary>Generiert einen <c>double</c>-Wert [0;1[. 
		 * Ist pHigh wahr, so ist der generierte Wert garantiert mindestens
		 * 0,5. Ist pHigh falsch, so ist der generierte Wert garantiert kleiner als 0,5.</summary>
		 * <returns>Einen <c>double</c>-Wert zwischen 0 und (exkl.) 1</returns>
		 * <param name="pHigh">Wert nahe 0 (false) oder 1 (true).</param>
		 */
        private double GeneriereZahl(bool pHigh)
        {
            double rueckgabe;

            if (pHigh)
            {
                rueckgabe = r.NextDouble();

                //Wenn die erste Zufallsgeneration in der gegenteiligen Umgebung liegt,
                //so wird sie solange überschrieben, bis die gewünschte Umgebung erreicht ist.
                if (rueckgabe < 0.5)
                    rueckgabe += 0.5;
            }
            else
            {
                rueckgabe = r.NextDouble();

                if (rueckgabe >= 0.5)
                    rueckgabe -= 0.5;
            }

            return rueckgabe;
        }

        /**<summary>Erzeugt mithilfe von <see cref="GeneriereZahl(bool)" /> ein 17 Felder großes <c>double</c>-Array, welches
		 * je nach übergebenem Parameter ein Muster enthält oder nicht.</summary>
		 * <param name="pLinieVorhanden">Gibt an, ob das Muster enthalten sein soll (true), oder nicht (false)</param>
		 * <returns>Ein <c>double</c>-Array, entweder mit Muster oder ohne.</returns>
		 */
        private double[] ErzeugeArray(bool pLinieVorhanden)
        {
            double[] rueckgabe = new double[17];

            /*Die ersten sechszehn Stellen des Arrays werden zunächst nahe 0 gefüllt, damit kein Muster entstehen kann, wo keins
			 sein soll. Die siebzehnte Stelle wird nachher angeben ob das Muster enthalten ist, oder nicht.*/
            for (int i = 0; i < 16; i++)
            {
                rueckgabe[i] = GeneriereZahl(false);
            }

            if (pLinieVorhanden)
            {
                //Das Muster soll innerhalb der 16 "Pixel" an einer zufälligen Stelle liegen,
                //daher ist die maximale Anfangsstelle für das fünf Zeichen lange Muster an der Stelle 11. (Next(int) ist exklusiv)
                int position = r.Next(12);

                rueckgabe[position] = GeneriereZahl(true);
                rueckgabe[position + 1] = GeneriereZahl(false);
                rueckgabe[position + 2] = GeneriereZahl(true);
                rueckgabe[position + 3] = GeneriereZahl(false);
                rueckgabe[position + 4] = GeneriereZahl(true);
            }

            return rueckgabe;
        }

        /**<summary>Erzeugt mithilfe von <see cref="ErzeugeArray(bool)"/> ein <c>double</c>-Array, in dem zufällig ein Muster
		 * enthalten ist oder nicht. Ob ein Muster enthalten ist, wird in der 17. Stelle festgehalten; 1 steht für wahr, 0 für falsch.</summary>
		 * <returns>Ein <c>double</c>-Array, welches zufällig ein Muster enthält oder nicht und die Information über diesen Umstand.</returns>
		 */
        public double[] ErzeugeTestdaten()
        {
            double[] rueckgabe;
            double hatLinie = double.Parse(r.Next(2).ToString());

            rueckgabe = ErzeugeArray(hatLinie == 1);

            //Information über Enthalt eines Musters wird in die 17. Stelle eingefügt.
            rueckgabe[16] = hatLinie;

            return rueckgabe;
        }

        /*
		 * ----------------------------------------------Ende des eigentlichen Modells, von hier Komfortfunktionen----------------------------------------------
		 */

        /**<summary>Diese Methode berechnet die in <c>pZeilen</c> spezifizierte Anzahl von Datensätzen und schreibt diese in eine Datei des Formats "NeuNet%Uhrzeit%.dataset". Gleichzeitig läuft in einem zweiten Thread eine Timerklasse, die in der Konsole die bisher abgelaufene Zeit seit dem Methodenaufruf angibt und mit
		 * Abschluss der Methode mit hoher Genauigkeit die gesamte verstrichene Zeit anzeigt.</summary>
		 * <param name="pZeilen">Gibt die Anzahl der Zeilen bzw. Datensätze an, die in der Datei enthalten sein sollen.</param>
		 */
        public void WriteToFile(int pZeilen)
        {
            Timer timer = new Timer();
            Thread t1 = new Thread(() => timer.Start());
            t1.Start();

            string fileName = GetFilename();
            FileStream fs = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < pZeilen; i++)
            {
                double[] temp = ErzeugeTestdaten();
                string line = "";

                for (int j = 0; j < 17; j++)
                {
                    line = line + temp[j] + "\t";
                }

                sw.WriteLine(line);
            }

            sw.Close();
            fs.Close();

            timer.Stop();
        }

        /**<summary>Eine Hilfsmethode für <see cref="ThreadedWriteToFile(int)"/>, die im Grunde funktioniert wie <see cref="WriteToFile(int)"/>, aber den in <c>pPath</c> übergebenen Dateinamen für die Ausgabe nutzt.</summary>
		 * <param name="pZeilen">Gibt die Anzahl der Zeilen bzw. Datensätze an, die in der Datei enthalten sein sollen.</param>
		 * <param name="pPath">Gibt den Dateipfad für die resultierende Datei an.</param>
		 */
        private void WriteToFile(int pZeilen, string pPath)
        {
            FileStream fs = File.Open(pPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < pZeilen; i++)
            {
                double[] temp = ErzeugeTestdaten();
                string line = "";

                for (int j = 0; j < 17; j++)
                {
                    line = line + temp[j] + "\t";
                }

                sw.WriteLine(line);
            }

            sw.Close();
            fs.Close();
        }

        /**<summary>Eine Hilfsmethode für <see cref="ThreadedWriteToFileArray(int)"/>, die ein <c>String</c>-Array zurückgibt, welches mit der in <c>pLines</c> angegebenen Anzahl an Datensätzen gefüllt ist.</summary>
		 * <param name="pLines">Gibt die Anzahl der zu berechnenden Datensätze an.</param>
		 * <returns>Ein mit der Anzahl von <c>pLines</c> Datensätzen gefülltes <c>String</c>-Array.</returns>
		 */
        private string[] WriteToArray(int pLines)
        {
            string[] arrayReturn = new string[pLines];
            for (int i = 0; i < pLines; i++)
            {
                double[] temp = ErzeugeTestdaten();
                string testdatenZeile = "";

                for (int j = 0; j < temp.Length; j++)
                {
                    testdatenZeile = testdatenZeile + temp[j] + " ";
                }

                arrayReturn[i] = testdatenZeile;
            }

            return arrayReturn;
        }

        /**<summary>Startet <see cref="ThreadedWriteToFileArray(int, string, int)"/> so oft mit maximal 5 Mio. als <c>pZeilen</c>, bis die Gesamtanzahl in <c>pZeilen</c> erreicht ist.</summary>
		 * <param name="pZeilen">Gibt die Anzahl der zu berechnenden Datensätze an.</param>
		 * <remarks>Wie bei <see cref="ThreadedWriteToFile(int)"/> ist auch hier keine Inhaltsgleichheit zu <see cref="WriteToFile(int)"/> gewährleistet.
		 * Diese Methode ist bei 10 000 000 Datensätzen ca. 10% schneller als <see cref="ThreadedWriteToFile(int)"/> und ca. 55% schneller als <see cref="WriteToFile(int)"/>.
		 * Bei 1 000 000 Datensätzen ist sie ca. 7% schneller als <see cref="ThreadedWriteToFile(int)"/> und ca. 52% schneller als <see cref="WriteToFile(int)"/>.</remarks>
		 */
        public void ThreadedWriteToFileArray(int pZeilen)
        {
            Timer timer = new Timer();
            String fileName = GetFilename();

            Thread t = new Thread(() => timer.Start());
            t.Start();

            int offset = 0;

            while (pZeilen > 0)
            {
                if (pZeilen > 5000000)
                {
                    ThreadedWriteToFileArray(5000000, fileName, offset);
                    pZeilen -= 5000000;
                    offset++;
                }
                else
                {
                    ThreadedWriteToFileArray(pZeilen, fileName, offset);
                    pZeilen = 0;
                }
            }

            timer.Stop();
        }

        /**<summary>Hilfsmethode / arbeitende Methode für <see cref="ThreadedWriteToFileArray(int)"/>. Erstellt eine Datei, die <c>pZeilen</c>
		 * Datensätze enthält. Hierfür werden 8 Threads gestartet, die <see cref="WriteToArray(int)"/> ausführen.</summary>
		 * <param name="pFileName">Gibt den Dateinamen an, dessen Datei die berechneten Datensätze angehangen werden sollen.</param>
		 * <param name="pSeedOffset">Gibt an, welcher Durchlauf für die gleiche Datei dies ist (Einziger Zweck: keine Doppelungen in den Daten).</param>
		 * <param name="pZeilen">Gibt die Anzahl der zu berechnenden und anzuhängenden Zeilen an.</param>
		 */
        private void ThreadedWriteToFileArray(int pZeilen, string pFileName, int pSeedOffset)
        {
            //Erstellt acht Arrays, die jeweils eine Länge von einem Achtel von pZeilen haben.
            string[] arrayToWrite0 = new string[pZeilen / 8];
            string[] arrayToWrite1 = new string[pZeilen / 8];
            string[] arrayToWrite2 = new string[pZeilen / 8];
            string[] arrayToWrite3 = new string[pZeilen / 8];
            string[] arrayToWrite4 = new string[pZeilen / 8];
            string[] arrayToWrite5 = new string[pZeilen / 8];
            string[] arrayToWrite6 = new string[pZeilen / 8];
            string[] arrayToWrite7 = new string[pZeilen / 8];

            //Erstellt die acht Threads, pSeedOffset ermöglicht 
            Thread t0 = new Thread(() => arrayToWrite0 = new DatasetGenerator(seed + pSeedOffset).WriteToArray(pZeilen / 8));
            Thread t1 = new Thread(() => arrayToWrite1 = new DatasetGenerator(seed + 1 + pSeedOffset).WriteToArray(pZeilen / 8));
            Thread t2 = new Thread(() => arrayToWrite2 = new DatasetGenerator(seed + 2 + pSeedOffset).WriteToArray(pZeilen / 8));
            Thread t3 = new Thread(() => arrayToWrite3 = new DatasetGenerator(seed + 3 + pSeedOffset).WriteToArray(pZeilen / 8));
            Thread t4 = new Thread(() => arrayToWrite4 = new DatasetGenerator(seed + 4 + pSeedOffset).WriteToArray(pZeilen / 8));
            Thread t5 = new Thread(() => arrayToWrite5 = new DatasetGenerator(seed + 5 + pSeedOffset).WriteToArray(pZeilen / 8));
            Thread t6 = new Thread(() => arrayToWrite6 = new DatasetGenerator(seed + 6 + pSeedOffset).WriteToArray(pZeilen / 8));
            Thread t7 = new Thread(() => arrayToWrite7 = new DatasetGenerator(seed + 7 + pSeedOffset).WriteToArray(pZeilen / 8));

            t0.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();

            while (t0.IsAlive || t1.IsAlive || t2.IsAlive || t3.IsAlive || t4.IsAlive || t5.IsAlive || t6.IsAlive || t7.IsAlive) { }

            StreamWriter sw = File.AppendText(pFileName); //new StreamWriter(fs);

            for (int i = 0; i < arrayToWrite0.Length; i++)
            {
                sw.WriteLine(arrayToWrite0[i]);
                sw.WriteLine(arrayToWrite1[i]);
                sw.WriteLine(arrayToWrite2[i]);
                sw.WriteLine(arrayToWrite3[i]);
                sw.WriteLine(arrayToWrite4[i]);
                sw.WriteLine(arrayToWrite5[i]);
                sw.WriteLine(arrayToWrite6[i]);
                sw.WriteLine(arrayToWrite7[i]);
            }

            arrayToWrite0 = new string[0];
            arrayToWrite1 = new string[0];
            arrayToWrite2 = new string[0];
            arrayToWrite3 = new string[0];
            arrayToWrite4 = new string[0];
            arrayToWrite5 = new string[0];
            arrayToWrite6 = new string[0];
            arrayToWrite7 = new string[0];

            //Löscht die bereits geschriebenen Arrays
            GC.Collect();

            sw.Close();
        }

        /**<summary>Diese Methode berechnet mithilfe der Methode <see cref="WriteToFile(int, string)"/> und acht voneinander separaten Instanzen des <see cref="DatasetGenerator"/>s in acht Threads die in <c>pZeilen</c> spezifizierte Anzahl von Datensätzen und schreibt diese zunächst in acht Dateien des Formats "NeuNet%Uhrzeit%.dataset%Threadnummer%", um sie
		 * anschließend in eine Datei des Formats "NeuNet%Uhrzeit%.dataset" zu übertragen. Die vorig geschriebenen Dateien mit dem Suffix 1 bzw. 2 etc werden nach dem Kopieren vernichtet. Gleichzeitig läuft in einem neunten Thread eine Timerklasse, die in der Konsole die bisher abgelaufene Zeit seit dem Methodenaufruf angibt und mit
		 * Abschluss der Methode mit hoher Genauigkeit die gesamte verstrichene Zeit anzeigt.</summary>
		 * <param name="pZeilen">Gibt die Anzahl der Zeilen bzw. Datensätze an, die in der Datei enthalten sein sollen.</param>
		 * <remarks>Die Ausgaben der Methoden <see cref="WriteToFile(int)"/> und <see cref="ThreadedWriteToFile(int)"/> sind insofern nicht inhaltsgleich, dass bei <see cref="ThreadedWriteToFile(int)"/> ab dem zweiten Achtel der Datensätze ein anderer Seed verwendet wird.</remarks>
		 */
        public void ThreadedWriteToFile(int pZeilen)
        {
            Timer timer = new Timer();

            string fileName = GetFilename();

            //Die berechnenden Threads erhalten durch 1 unterschiedliche Seeds, da sie sonst
            //identische Dateien erstellen würden. -> Diskrepanz zu WriteToFile()
            Thread t0 = new Thread(() => new DatasetGenerator(seed).WriteToFile(pZeilen / 8, fileName + "0"));
            Thread t1 = new Thread(() => new DatasetGenerator(seed + 1).WriteToFile(pZeilen / 8, fileName + "1"));
            Thread t2 = new Thread(() => timer.Start());
            Thread t3 = new Thread(() => new DatasetGenerator(seed + 2).WriteToFile(pZeilen / 8, fileName + "2"));
            Thread t4 = new Thread(() => new DatasetGenerator(seed + 3).WriteToFile(pZeilen / 8, fileName + "3"));
            Thread t5 = new Thread(() => new DatasetGenerator(seed + 4).WriteToFile(pZeilen / 8, fileName + "4"));
            Thread t6 = new Thread(() => new DatasetGenerator(seed + 5).WriteToFile(pZeilen / 8, fileName + "5"));
            Thread t7 = new Thread(() => new DatasetGenerator(seed + 6).WriteToFile(pZeilen / 8, fileName + "6"));
            Thread t8 = new Thread(() => new DatasetGenerator(seed + 7).WriteToFile(pZeilen / 8, fileName + "7"));

            t0.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();

            while (t0.IsAlive || t1.IsAlive || t3.IsAlive || t4.IsAlive || t5.IsAlive || t6.IsAlive || t7.IsAlive || t8.IsAlive) { }

            FileStream fs1 = File.OpenRead(fileName + "0");
            FileStream fs2 = File.OpenRead(fileName + "1");
            FileStream fs3 = File.OpenRead(fileName + "2");
            FileStream fs4 = File.OpenRead(fileName + "3");
            FileStream fs5 = File.OpenRead(fileName + "4");
            FileStream fs6 = File.OpenRead(fileName + "5");
            FileStream fs7 = File.OpenRead(fileName + "6");
            FileStream fs8 = File.OpenRead(fileName + "7");

            StreamReader sr1 = new StreamReader(fs1);
            StreamReader sr2 = new StreamReader(fs2);
            StreamReader sr3 = new StreamReader(fs3);
            StreamReader sr4 = new StreamReader(fs4);
            StreamReader sr5 = new StreamReader(fs5);
            StreamReader sr6 = new StreamReader(fs6);
            StreamReader sr7 = new StreamReader(fs7);
            StreamReader sr8 = new StreamReader(fs8);

            //FileStream fsFinal = File.OpenWrite(fileName);
            StreamWriter swFinal = File.AppendText(fileName); //new StreamWriter(fsFinal);

            //Kopiert jeweils eine Zeile auf einmal aus den jeweiligen Einzeldateien in die finale, kumulierte Datei.
            while (!sr1.EndOfStream)
            {
                swFinal.WriteLine(sr1.ReadLine());
            }

            sr1.Close();
            fs1.Close();
            File.Delete(fileName + "0");

            while (!sr2.EndOfStream)
            {
                swFinal.WriteLine(sr2.ReadLine());
            }

            sr2.Close();
            fs2.Close();
            File.Delete(fileName + "1");

            while (!sr3.EndOfStream)
            {
                swFinal.WriteLine(sr3.ReadLine());
            }

            sr3.Close();
            fs3.Close();
            File.Delete(fileName + "2");

            while (!sr4.EndOfStream)
            {
                swFinal.WriteLine(sr4.ReadLine());
            }

            sr4.Close();
            fs4.Close();
            File.Delete(fileName + "3");

            while (!sr5.EndOfStream)
            {
                swFinal.WriteLine(sr5.ReadLine());
            }

            sr5.Close();
            fs5.Close();
            File.Delete(fileName + "4");

            while (!sr6.EndOfStream)
            {
                swFinal.WriteLine(sr6.ReadLine());
            }

            sr6.Close();
            fs6.Close();
            File.Delete(fileName + "5");

            while (!sr7.EndOfStream)
            {
                swFinal.WriteLine(sr7.ReadLine());
            }

            sr7.Close();
            fs7.Close();
            File.Delete(fileName + "6");

            while (!sr8.EndOfStream)
            {
                swFinal.WriteLine(sr8.ReadLine());
            }

            sr8.Close();
            fs8.Close();
            File.Delete(fileName + "7");

            swFinal.Close();
            //fsFinal.Close();

            timer.Stop();
        }

        /**<summary>Hilfsmethode, die einen Dateinamen des Formats "NeuNet%Uhrzeit%.dataset" zurückgibt.</summary>
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
            string fileName = @"NeuNet" + @time + @".dataset";

            return fileName;
        }

        public double[] ReadLine(string pFileName)
        {
            var fs = File.OpenRead(pFileName);
            var sr = new StreamReader(fs);
            double[] rueckgabe = new double[17];

            if (!sr.EndOfStream)
            {
                var temp = sr.ReadLine();

                for (int i = 0; i < 17; i++)
                {
                    var index = temp.IndexOf(' ');
                    rueckgabe[i] = double.Parse(temp.Substring(0, index));
                    temp = temp.Substring(index + 1);
                }

                return rueckgabe;
            }

            return default;
        }

        //Gehört auch zu public void ThreadedWriteToFileDoubleArray(int pZeilen)
        /*public void WriteDoubleArrayToFile(double[][] pArray0, double[][] pArray1, string pFileName)
		{
			FileStream fs = File.OpenWrite(pFileName);
			StreamWriter sw = new StreamWriter(fs);

			for (int i = 0; i < pArray0.Length; i++)
			{
				for (int j = 0; j < pArray0[i].Length; j++)
				{
					sw.Write(pArray0[i][j] + " ");
				}
				sw.WriteLine();

				for (int j = 0; j < pArray1[i].Length; j++)
				{
					sw.Write(pArray1[i][j] + " ");
				}
				sw.WriteLine();
			}

			pArray0 = new double[0][];
			pArray1 = new double[0][];

			sw.Close();
			fs.Close();
		}*/

        //gehört zu public void ThreadedWriteToFileDoubleArray(int pZeilen)
        /*private double[][] WriteToDoubleArray(int pLines)
		{
			double[][] arrayReturn = new double[pLines][];

			for(int i = 0; i < pLines; i++)
			{
				arrayReturn[i] = ErzeugeTestdaten();
			}

			return arrayReturn;
		}*/

        //Funktioniert bis auf eine Zeile gegen Ende der Datei einwandfrei, dauert nur leider doppelt so lange wie das einzeln gethreadete WriteToFile() xD
        /*public void ThreadedWriteToFileDoubleArray(int pZeilen)
		{
			var timer = new Timer();

			double[][] arrayToWrite0 = new double[pZeilen / 4][];
			double[][] arrayToWrite1 = new double[pZeilen / 4][];

			Thread t0 = new Thread(() => arrayToWrite0 = new DatasetGenerator(seed).WriteToDoubleArray(pZeilen / 2));
			Thread t1 = new Thread(() => arrayToWrite1 = new DatasetGenerator(seed + 1).WriteToDoubleArray(pZeilen / 2));
			Thread t2 = new Thread(() => timer.Start());

			t0.Start();
			t1.Start();
			t2.Start();

			bool finished = false;
			while (!finished)
			{
				if (!t0.IsAlive && !t1.IsAlive)
				{
					finished = true;
				}
			}

			var fileName = GetFilename();
			double[][] arrayToWrite2 = new double[pZeilen / 4][];
			double[][] arrayToWrite3 = new double[pZeilen / 4][];

			Thread t3 = new Thread(() => WriteDoubleArrayToFile(arrayToWrite0, arrayToWrite1, fileName));
			t0 = new Thread(() => arrayToWrite2 = new DatasetGenerator(seed + 2).WriteToDoubleArray(pZeilen / 2));
			t1 = new Thread(() => arrayToWrite3 = new DatasetGenerator(seed + 3).WriteToDoubleArray(pZeilen / 2));

			t3.Start();
			t0.Start();
			t1.Start();

			finished = false;
			while (!finished)
			{
				if (!t0.IsAlive && !t1.IsAlive && !t3.IsAlive)
				{
					finished = true;
				}
			}

			t3 = new Thread(() => WriteDoubleArrayToFile(arrayToWrite2, arrayToWrite3, fileName));

			t3.Start();

			finished = false;
			while (!finished)
			{
				if (!t3.IsAlive)
				{
					finished = true;
				}
			}

			timer.Stop();
		}*/
    }
}

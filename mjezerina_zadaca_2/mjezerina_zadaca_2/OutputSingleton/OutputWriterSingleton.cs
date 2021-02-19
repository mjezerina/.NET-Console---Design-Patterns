using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mjezerina_zadaca_2.OutputSingleton
{
    class OutputWriterSingleton
    {
        private static OutputWriterSingleton instance;
        private static object syncLock = new object();

        private OutputWriterSingleton() { }

        public static OutputWriterSingleton Instance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new OutputWriterSingleton();
                    }
                }
            }
            return instance;
        }


        public void IspisGreskeGasenjePrograma(string poruka)
        {
            Console.WriteLine(poruka);
            Console.WriteLine("Pritisnite bilo koju tipku za izlaz!");
            Console.ReadLine();
            Environment.Exit(1);
        }


        public void ispisGreske(string greska)
        {
            Console.WriteLine(greska);
        }

    }
}

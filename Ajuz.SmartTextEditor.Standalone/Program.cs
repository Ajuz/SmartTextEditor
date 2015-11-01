using Ajuz.SmartTextEditor.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.Standalone
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputStreamReader = new StreamReader(
                Console.OpenStandardInput(),
                Console.InputEncoding);

            var outputStreamWriter = new StreamWriter(
                Console.OpenStandardOutput(),
                Console.OutputEncoding);

            var autocompleteAppication = new AutocompleteApplication(
                new DictionaryWordsInputReader(inputStreamReader),
                new BeginningsOfWordsInputReader(inputStreamReader),
                new AutocompleteOutputWriter(outputStreamWriter));

            autocompleteAppication.SearchAutocomplete();
        }
    }
}

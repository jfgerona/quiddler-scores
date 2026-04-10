using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    internal class Card
    {
        //Members
        public string letter { get; init; }
        //Constructor
        internal Card(string letter)
        {
            this.letter = letter;
        }
    }
}

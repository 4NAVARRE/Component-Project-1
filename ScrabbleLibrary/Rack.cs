using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    internal class Rack : IRack
    {

        public Rack(Bag bag)
        {

        }
        public uint TotalPoints { get; init; }

        public uint AddTiles()
        {
            throw new NotImplementedException();
        }

        public bool PlayWord(string candidate)
        {
            throw new NotImplementedException();
        }

        public uint TestWord(string candidate)
        {
            throw new NotImplementedException();
        }
    }
}

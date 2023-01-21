using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    public interface IRack
    {
        public uint TotalPoints { get; init; }
        public uint TestWord(string candidate);
        public bool PlayWord(string candidate);
        public uint AddTiles();
        public string ToString();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    public interface IBag
    {
        public string Author{ get; init; }
        public uint TileCount{ get; init; }

        public IRack GenerateRack();
        public string ToString();
    }
}

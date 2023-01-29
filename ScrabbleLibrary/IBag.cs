using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    public interface IBag
    {
        public string Author{ get; }
        public uint TileCount{ get; }

        public IRack GenerateRack();
        public string ToString();
    }
}

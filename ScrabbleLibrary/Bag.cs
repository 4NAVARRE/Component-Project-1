using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleLibrary
{
    
    public class Bag : IBag
    {


        string tileName;
        uint value;
        uint amount;

        public enum letterEnum { a = 1, b = 3, c = 3, d = 2, e = 1, f = 4, g = 2, h = 4, i = 1, j = 8, k = 5, l = 1, m = 3, n = 1, o = 1, p = 3, q = 10, r = 1, s = 1, t = 1, u = 1, v = 4, w = 4, x = 8, y = 4, z = 10 }
        Dictionary<letterEnum, int> letterMap = new Dictionary<letterEnum, int>() {
        { letterEnum.a, 9},{ letterEnum.b, 2},{ letterEnum.c, 2},{ letterEnum.d, 4},
        { letterEnum.e, 12},{ letterEnum.f, 2}, { letterEnum.g, 3}, { letterEnum.h, 2},
        { letterEnum.i, 9}, {letterEnum.j, 1}, { letterEnum.k, 1}, { letterEnum.l, 4},
        {letterEnum.m, 2}, { letterEnum.n, 6}, { letterEnum.o, 8}, { letterEnum.p, 2}, 
        {letterEnum.q, 1}, { letterEnum.r, 6}, { letterEnum.s, 4}, { letterEnum.t, 6},
        {letterEnum.u, 4}, { letterEnum.v, 2}, { letterEnum.w, 2}, { letterEnum.x, 1},
        {letterEnum.y, 2}, { letterEnum.z, 1}};

        string IBag.Author { get; init; }
        uint IBag.TileCount { get; init; } = 98;

        public Bag()
        {
            
        }



        public IRack GenerateRack()
        {
            throw new NotImplementedException();
        }
    }
}

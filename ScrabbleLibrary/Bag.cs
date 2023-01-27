﻿using System;
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

        //public enum letterEnum { a=0, b=1, c=2, d=3, e=4, f=5, g=6, h=7, i=8, j=9, k=10, l=11, m=12, n=13, o=14, p=15, q=16, r=17, s=18, t=19, u=20, v=21, w=22, x=23, y=24, z=25}
        internal Dictionary<char, int> letterMap;

        string IBag.Author { get {
                return "Author: Hongseok Kim & Stanislav Kovalenko - January 28, 2023";
            } init { } }

        //get iterates through letterMap to calculate the #of tiles
        uint IBag.TileCount
        {
            get
            {
                uint count = 0;
                foreach (var tile in letterMap)
                {
                    count += (uint)tile.Value;
                }
                return count;
            }
            init { }
        }
        public Bag()
        {
            letterMap = new Dictionary<char, int>() {
        { 'a', 9},{ 'b', 2},{ 'c', 2},{ 'd', 4},
        { 'e', 12},{ 'f', 2}, { 'g', 3}, { 'h', 2},
        { 'i', 9}, {'j', 1}, { 'k', 1}, { 'l', 4},
        {'m', 2}, { 'n', 6}, { 'o', 8}, { 'p', 2},
        {'q', 1}, { 'r', 6}, { 's', 4}, { 't', 6},
        {'u', 4}, { 'v', 2}, { 'w', 2}, { 'x', 1},
        {'y', 2}, { 'z', 1}};
        }

        //get a ran
        public char GetARandomTile()
        {
            Random r = new Random();
            List<KeyValuePair<char, int>> list = new List<KeyValuePair<char, int>>();
            //getting the list of tiles with count value >0
            foreach (var tile in letterMap)
                if (tile.Value > 0)
                    list.Add(tile);

            int rInt = r.Next(0, list.Count);
            char selectedTile = letterMap.ElementAt(rInt).Key;
            //decrement the tile count for selected tile
            letterMap[selectedTile]--;
            return selectedTile;
        }

            public IRack GenerateRack()
        {
            return new Rack(this);
        }

        public override string ToString() {
            StringBuilder bagStatus = new("");
            foreach(var tile in letterMap)
            {
                string status = $"{tile.Key}({tile.Value})\t";
                bagStatus.Append(status);
            }
            return bagStatus.ToString() + "\n";
        }

    }
}

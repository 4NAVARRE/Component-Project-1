using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static ScrabbleLibrary.Bag;

namespace ScrabbleLibrary
{
    internal class Rack : IRack
    {
        private List<char> rackList;
        IBag bag;
        public Rack(Bag bag)
        {
            rackList = new();
            this.bag = bag;
            //List<char> list = new List<char>();
            for (int i = 0; i < 7; i++)
            {              
                    //0~25 represents each alphabet starting from 0 = A
                    Bag tempBag = (Bag) bag;
                    char newTile = tempBag.GetARandomTile();
                    //GetARandomTile function will return '?' character if there are no tiles available in the bag
                    if (newTile == '?')
                        break;

                    if (bag.letterMap[newTile] >= 0)
                    {
                        rackList.Add(newTile);
                        //decrement  the tile count for randomly selected alphabet
                        bag.letterMap[newTile]--;
                        break;
                    }               

            }
        }
        private uint _totalPoints;
        public uint TotalPoints
        {
            get
            {
                return _totalPoints;
            }
            init
            {
                _totalPoints = 0;
            }
        }

        public uint AddTiles()
        {
            //filling up the rack after some tiles have been removed by creating a word from the rack 
            while(rackList.Count < 7) { 
                if(bag.TileCount <= 0)                
                    break;
                
                Bag convertedBag = (Bag)bag;
                //GetARandomTile function was implemented in Bag class, so have to convert it from IBag type
                //to BAg
                rackList.Add(convertedBag.GetARandomTile());
            }
            return (uint)rackList.Count;
        }

        public uint GetCharacterPoint(char character)
        {
            switch (character)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'l':
                case 'n':
                case 'o':
                case 'r':
                case 's':
                case 't':
                case 'u':
                    return 1;
                case 'd':
                case 'g':
                    return 2;
                case 'b':
                case 'c':
                case 'm':
                case 'p':
                    return 3;
                case 'f':
                case 'h':
                case 'v':
                case 'w':
                case 'y':
                    return 4;
                case 'k':
                    return 5;
                case 'j':
                case 'x':
                    return 8;
                case 'q':
                case 'z':
                    return 10;
                default:
                    throw new NotImplementedException($"The alphabet {character} was not implemented.");
            }
        }
        public bool PlayWord(string candidate)
        {
            uint score = TestWord(candidate);   //dbomqqq  d o o m
            if (score > 0)
            {
                foreach (var cha in candidate)  
                {
                    if (rackList.Contains(cha))
                    {
                        rackList.Remove(cha);
                    }
                }
                _totalPoints += score;
                return true;
            }
            else
            {
                return false;
            }
        }

        public uint TestWord(string candidate)
        {
            //To handle the cases when a word contains duplicated character ("Coffee", "doom", etc)
            //and there is only one character in the tileset
            var tempHolder = rackList;
            uint score = 0;

            Microsoft.Office.Interop.Word.Application wordObject = new();
            if (!wordObject.CheckSpelling(candidate))
            {
                return 0;
            }
                         
            foreach (char cha in candidate)
            {
                if (tempHolder.Contains(cha))   
                tempHolder.Remove(cha);
                else
                    return 0;
            }
            
            foreach (char character in candidate)
            {
                score += GetCharacterPoint(character);
            }
            _totalPoints += score;
            return score;
        }

        public override string ToString()
        {
            string tmp = null;
            foreach (char character in rackList)
            {
                tmp = tmp + character;
            }
            return tmp;
        }
    }
}

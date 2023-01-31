/***
 * Author : Stanislav Kovalenko &  Hongseok Kim 
 * Date : 27/01/2023
 * File : Bag.cs
 * Description : A class that implements IBag interface to represent a bag
 * filled with alphabet tiles.
 */
using System.Text;

namespace ScrabbleLibrary
{

    public class Bag : IBag
    {
        //a private uint variable which indicates the total number of tiles in the Bag.
        //this value will be updated every time there are changes to the letterMap
        //dictionary
        private uint _tileCount;      
               
        //An internal <char, int> pair dictionary, whose char Key represents the type of tiles
        //and int Value represents the # of tiles in the bag
        internal Dictionary<char,uint> letterMap;

        //Summary : Implemented read-only property that returns the Authors information
        string IBag.Author {
            get {
                return "Author: Hongseok Kim & Stanislav Kovalenko - January 28, 2023";
            } 
        }

        //Summary : Implemented read-only property that returns the # of tiles in the  bag
        uint IBag.TileCount
        {
            get
            {
               return _tileCount;
            }
        }

        //Summary : A public constructor. This will initialize the letterMap dictionary 
        //and private _tileCount property
        public Bag()
        {
            letterMap = new Dictionary<char, uint>() {
        { 'a', 9},{ 'b', 2},{ 'c', 2},{ 'd', 4},
        { 'e', 12},{ 'f', 2}, { 'g', 3}, { 'h', 2},
        { 'i', 9}, {'j', 1}, { 'k', 1}, { 'l', 4},
        {'m', 2}, { 'n', 6}, { 'o', 8}, { 'p', 2},
        {'q', 1}, { 'r', 6}, { 's', 4}, { 't', 6},
        {'u', 4}, { 'v', 2}, { 'w', 2}, { 'x', 1},
        {'y', 2}, { 'z', 1}};

            _tileCount = 0;
            foreach (var tile in letterMap)
            {
                _tileCount += (uint)tile.Value;
            }
        }

        //Summary : A public function that gets a random tile which has more than 0 count
        //from the letterMap dictionary
        //Returns : A char value which indicates a tile
        public char GetARandomTile()
        {       
            if (_tileCount == 0)
            {
                //GetARandomTile function will return '?' character to indicate
                //that there are no tiles available to the caller
                return '?';
            }
               

            Random r = new Random();
            List<KeyValuePair<char, uint>> nonZeroTiles = new List<KeyValuePair<char, uint>>();
            //getting the list of tiles with count value >0
            foreach (var tile in letterMap)
                if (tile.Value > 0)
                    nonZeroTiles.Add(tile);

            //get a random tiles from nonZeroTiles and decrement the count 
            //for selected tile
            int rInt = r.Next(0, nonZeroTiles.Count);
            char selectedTile =nonZeroTiles.ElementAt(rInt).Key;

            letterMap[selectedTile]--;
            _tileCount--;
            return selectedTile;
        }

        //Summary : A public function creates a new IRack type object (each IRack object represents a player)
        //which references the Bag object
        //Returns : A new IRack type object
        public IRack GenerateRack()
        {
            return new Rack(this);
        }

        //Summary : Get the current status of this Bag object as string that shows the count of each tiles
        //in {tile.Key}({tile.Value}) format.
        //Returns : A string
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

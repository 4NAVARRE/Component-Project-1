/***
 * Author : Stanislav Kovalenko &  Hongseok Kim 
 * Date : 28/01/2023
 * File : Bag.cs
 * Description : An internal class that implements IRack interface to represent a players assigned with rack slots
 * filled with alphabet tiles.
 */

using Microsoft.Office.Interop.Word;

namespace ScrabbleLibrary
{
    internal class Rack : IRack
    {        
        private uint _totalPoints; //total points obtained by  the player
        private List<char> rackList; //rack slots which will hold up to 7 tiles
        private IBag bag; //reference to the Bag object
        private Bag convertedBag;
        //Internal constructor which can only be called by GenerateRack() method defined in Bag class
        internal Rack(Bag bag)
        {
            rackList = new();
            this.bag = bag;          
            convertedBag = (Bag)bag;
            _totalPoints= 0;

            //Assign the rackList with 7 tiles selected from the IBag object
            while(rackList.Count < 7) 
            {
                char newTile = bag.GetARandomTile();
                if (newTile == '?')
                    break;
                rackList.Add(newTile);   
            }
        }
             
        //A public implemented readonly property that returns the total points obtained 
        //by the player
        uint IRack.TotalPoints
        {
            get
            {
                return _totalPoints;
            }           
        }

        //Summary : A public function implemented from the IRack interface that 
        //adds tiles to empty slots in rack(List<char>) from the Bag.
        //Returns : The # of tiles in the rack after execution.
        uint IRack.AddTiles()
        {
            //filling up the rack until the rackList is filled with 7 tiles or the # of total tiles in the bag
            //reaches 0
            while(rackList.Count < 7) { 
                if(bag.TileCount <= 0)                
                    break;
                
                Bag convertedBag = (Bag)bag;
                rackList.Add(convertedBag.GetARandomTile());
            }
            return (uint)rackList.Count;
        }

        //Summary : A private function that returns the score of char input
        //Returns : The uint score value of char input
        private uint GetCharacterPoint(char character)
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

        //Summary : A public function that test string input and removes
        //the tiles from the rack if valid
        //Returns : A bool value indicating the validity of the input 
        bool IRack.PlayWord(string candidate)
        {
            uint score = 0;
            //instead of calling TestWord to get the score,
            //placed a for loop to calculate the score (TestWord takes too long for execution)
            foreach (char character in candidate)
            {
                score += GetCharacterPoint(character);
            }
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

                //filling up the empty slots                
                while (rackList.Count < 7 && bag.TileCount > 0)
                {
                    rackList.Add(convertedBag.GetARandomTile());
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //Summary : A public function that test the potential score of string input
        //Returns : A uint value that indicating the score 
        uint IRack.TestWord(string candidate)
        {
            //To handle the cases when a word contains duplicated character ("Coffee", "doom", etc)
            //and there is only one character in the tileset, create a copy of current rack
            List<char> tempHolder = new();
            foreach (var cha in rackList){
                tempHolder.Add(cha);
            }
            
            uint score = 0;
            
            if (!convertedBag.wordObject.CheckSpelling(candidate))
            {
                return 0;
            }
                         
            //validating if the input word can be created from the rack
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
            return score;
        }

        //Summary : A public function that returns the list of tiles(char) in the rack(List<char>)
        //as a concatenated string.
        //Returns : A string
        public override string ToString()
        {
            string tmp = null;
            foreach (char character in rackList)
            {
                tmp += character;
            }
            return tmp;
        }
    }
}

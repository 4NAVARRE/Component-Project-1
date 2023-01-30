/***
 * Author : Stanislav Kovalenko &  Hongseok Kim 
 * Date : 27/01/2023
 * File : IRack.cs
 * Description : A interface that provides implementation for Rack class
 */
namespace ScrabbleLibrary
{
    public interface IRack
    {
        //Summary : A public read-only property that returns the total score obtained
        //by the player
        //Returns : A uint value that indicating the score 
        public uint TotalPoints { get; }

        //Summary : A public function that test the potential score of string input
        //Returns : A uint value that indicating the score 
        public uint TestWord(string candidate);

        //Summary : A public function that test string input and removes
        //the tiles from the rack if valid
        //Returns : A bool value indicating the validity of the input 
        public bool PlayWord(string candidate);

        //Summary : A public function that adds tiles to empty slots in rack(List<char>) from the Bag.
        //Returns : The # of tiles in the rack after execution.
        public uint AddTiles();

        //Summary : A public function that returns the list of tiles(char) in the rack(List<char>)
        //as a concatenated string.
        //Returns : A string
        public string ToString();
    }
}

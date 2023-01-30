/***
 * Author : Stanislav Kovalenko &  Hongseok Kim 
 * Date : 27/01/2023
 * File : IBag.cs
 * Description : A interface that provides implementation for Bag class
 */

namespace ScrabbleLibrary
{
    public interface IBag
    {
        //Summary : A public read-only property that returns the Authors information
        //Returns : A string value that stating the Author information
        public string Author{ get; }

        //Summary : A public read-only property that returns the # of tiles in the  bag
        //Returns :  unsigned integer value of count of tiles
        public uint TileCount{ get; }

        //Summary : Create a new IRack implemented object (each IRack object represents a player)
        //Returns :  new IRack object (player)
        public IRack GenerateRack();

        //Summary : Get the current status  of the IBag implemented object
        //Returns : A string
        public string ToString();
    }
}

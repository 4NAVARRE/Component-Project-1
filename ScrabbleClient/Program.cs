/***
 * Author : Stanislav Kovalenko &  Hongseok Kim 
 * Date : 27/01/2023
 * File : Program.cs
 * Description : The client program to play the Scrabble game 
 * with ScrabbleLibrary
 */
using ScrabbleLibrary;
using System.ComponentModel;
using System.Numerics;

namespace INFO5060_Project1
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Creating an instance of the Bag class
            IBag storage = new Bag();
            Console.WriteLine($"Testing ScrabbleLibrary [{storage.Author}]");

            // Displaying the number of tiles in the bag
            Console.WriteLine($"Bag initialized with the following {storage.TileCount} tiles..");
            Console.WriteLine(storage.ToString());

            // Prompting the user to enter the number of players
            Console.Write("Enter the numbers of player (1-8): ");

            // Declaring a variable to store the number of players
            int playersAmount = -1;
            // Loop to validate the input
            while ((playersAmount < 0) || (playersAmount > 8))
            {
                try
                {
                    playersAmount = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invaid input");
                }
            }

            // List to store instances of the IRack class
            List<IRack> players = new List<IRack>();

            //string[] players = new string[playersAmount];
            // Loop to generate racks for the specified number of players
            for (int i = 0; i < playersAmount; i++)            
                players.Add(storage.GenerateRack());

            // Boolean flag to continue the game           
            bool continueGameFlag = true;

            Console.WriteLine($"Racks for {playersAmount} were populated.");
            //while the game is going
            while (continueGameFlag)
            {   
                int playerTurn;
                //this for loop ends with the round
                for (playerTurn = 0; playerTurn < playersAmount; playerTurn++)
                {
                    Console.WriteLine($"Bag now contains the following {storage.TileCount} tiles..");
                    Console.WriteLine(storage.ToString());
                    continueGameFlag = Game(players[playerTurn], playerTurn);

                    if (!continueGameFlag)
                        break;                    
                }
                //when round is over
                if (playerTurn == playersAmount)
                {
                    while (true)
                    {
                        Console.Write("Would you like each player to take another turn? (y/n): ");
                        string? choice = Console.ReadLine();
                        if (choice == "y")
                            break;
                        else if (choice == "n")
                        {
                            continueGameFlag = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                            continue;
                        }
                    }
                }
            } 
            Console.WriteLine("\nRetiring the game.\n\n"+
               "The final scores are...\n\n"+
               "-----------------------------------------------------------------------------");                 
            for(int i =  0; i < players.Count; i++)               
                Console.WriteLine($"Player {i+1}: {players[i].TotalPoints} points");
            Console.WriteLine("-----------------------------------------------------------------------------\n");
            storage.Dispose();
            return;
         }

        public static void BuildHeader(int value)
        {
            Console.WriteLine("-----------------------------------------------------------------------------\n" +
            $"                                Player {value}\n" +
            "----------------------------------------------------------------------------- ");
        }

        public static bool Game(IRack player, int playerNumber)
        {
            // String variable to store user's choice
            string? choice;
            // Call the BuildHeader method with the player number incremented by 1 as an argument
            BuildHeader(playerNumber+1);
            // Infinite loop to continue the game until the user decides to give up
            while (true)
            {
                // Display the letters in the player's rack
                // Ask the user if they want to test a word for its points value
                // Read the user's response and store it in the `choice` variable
                Console.WriteLine($"Yor rack contains [{player}].");
                Console.Write("Test a word for its points value? (y/n): ");
                choice = Console.ReadLine();

                // Switch statement to handle the user's response
                switch (choice.ToLower())
                {
                    case "y":
                        Console.Write($"Enter a word using the letters [{player}]: ");
                        string? word;
                        while (true) {
                            word = Console.ReadLine();
                            //The function TestWord() takes long to execute, so minimize the # of execution
                            if ((word != null && word != string.Empty))
                                break;
                          }
                        uint score = player.TestWord(word);
                        if (score > 0)
                        {
                            Console.WriteLine($"The word [{word}] is worth {score} points.");
                            Console.WriteLine($"Do you want to play the word [{word}]? (y/n): ");
                            choice = Console.ReadLine();
                            if (choice == "y")
                            {
                                // Call the PlayWord method on the `player` object to play the word
                                player.PlayWord(word);

                                // Display the word played, the score, and the updated rack
                                Console.WriteLine("\t\t------------------------------");
                                Console.WriteLine(String.Format("\t\tWord Played:{0,10}", word));
                                Console.WriteLine(String.Format("\t\tTotal Points:{0,8}", score));
                                Console.WriteLine(String.Format("\t\tRack now contains:{0,10}", $"[{player}]"));
                                Console.WriteLine("\t\t------------------------------\n");

                                // Return `true` to indicate that a word has been played
                                return true;
                            }
                        }
                        else  // If the word has a score of 0
                        {
                            // Inform the user that the word is worth 0 points
                            Console.WriteLine($"The word [{word}] is worth 0 points.");
                        }
                        break;
                    case "n":
                        Console.Write($"Are you giving up? (y/n): ");
                        choice = Console.ReadLine();

                        if (choice == "y")                        
                            return false;                  
                        
                        else if (choice == "n")                      
                            continue;      
                        
                        else                       
                            Console.WriteLine("Wrong Input");       
                        
                        break;
                    default:                        
                        Console.WriteLine("Wrong Input");
                        break;                       
                }
            }
        }
    }
}
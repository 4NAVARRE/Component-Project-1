using ScrabbleLibrary;
using System.Numerics;

namespace INFO5060_Project1
{
    class Program
    {
        public static void Main(string[] args)
        {
            IBag storage = new Bag();
            Console.WriteLine($"Testing ScrabbleLibrary [{storage.Author}]");

            Console.WriteLine($"Bag initialized with the following {storage.TileCount} tiles..");
            Console.WriteLine(storage.ToString());

            Console.Write("Enter the numbers of player (1-8): ");

            int playersAmount = -1;
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

            List<IRack> players = new List<IRack>();

            //string[] players = new string[playersAmount];

            for (int i = 0; i < playersAmount; i++)            
                players.Add(storage.GenerateRack());            

            // 1 for game is going, 0 when changing player, -1 when game is over
            bool continueGameFlag = true;
            //while the game is going
            while (continueGameFlag)
            {
                Console.WriteLine($"Racks for {playersAmount} were populated.\nBag now contains the following {storage.TileCount} tiles..");
                Console.WriteLine(storage.ToString());

                int playerTurn;
                //this for loop ends with the round
                for (playerTurn = 0; playerTurn < playersAmount; playerTurn++)
                {                    
                    continueGameFlag = game(players[playerTurn], playerTurn);

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
            return;
         }




        public static void buildHeader(int value)
        {
            Console.WriteLine("-----------------------------------------------------------------------------\n" +
            $"                                Player {value}\n" +
            "----------------------------------------------------------------------------- ");
        }

        public static bool game(IRack player, int playerNumber)
        {
            string? choice;
            buildHeader(playerNumber+1);
            while (true)
            {
                Console.WriteLine($"Yor rack contains [{player}].");
                Console.Write("Test a word for its points value? (y/n): ");
                choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "y":
                        Console.Write($"Enter a word using the letters [{player}]: ");
                        string? word = Console.ReadLine();
                        //The function TestWord() takes long to execute, so minimize the # of execution
                        uint score = player.TestWord(word);
                        if (score > 0)
                        {
                            Console.WriteLine($"The word [{word}] is worth {score} points.");
                            Console.WriteLine($"Do you want to play the word [{word}]? (y/n): ");
                            choice = Console.ReadLine();
                            if (choice == "y")
                            {
                                //play method goes here
                                player.PlayWord(word);

                                Console.WriteLine("\t\t------------------------------");
                                Console.WriteLine(String.Format("\t\tWord Played:{0,10}", word));
                                Console.WriteLine(String.Format("\t\tTotal Points:{0,8}", score));
                                Console.WriteLine(String.Format("\t\tRack now contains:{0,10}", $"[{player}]"));
                                Console.WriteLine("\t\t------------------------------\n");
                                return true;
                            }
                        }
                        else
                        {
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
using ScrabbleLibrary;

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
            {
                players.Add(storage.GenerateRack());
            }



            Console.WriteLine($"Racks for {playersAmount} were populated.\nBag now contains the following {storage.TileCount} tiles..");

            

            
            // 1 for game is going, 0 when changing player, -1 when game is over
            while (true)
            {
                for (int i = 0; i < playersAmount; i++)
                {
                    players[i] = game(players[i]);
                    if (players[i] == null)
                    {
                        return;
                    }
                }
            }


    }

        public static void buildHeader(int value)
        {
            Console.WriteLine("-----------------------------------------------------------------------------\n" +
            $"Player {value}\n" +
            "---------------------------------------------------------------------------- - ");
        }

        public static IRack game(IRack player)
        {
            string choice;
            buildHeader(1);
            while (true)
            {
                Console.WriteLine($"Yor rack contains [{player}].");
                Console.Write("Test a word for its points value? (y/n): ");
                choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "y":
                        Console.Write($"Enter a word using the letters [{player}]: ");
                        choice = Console.ReadLine();
                        if (player.TestWord(choice) > 0)
                        {
                            Console.WriteLine($"The word [{choice}] is worth {player.TestWord(choice)} points.");
                            Console.WriteLine($"Do you want to play the word [{choice}]? (y/n): ");
                            choice = Console.ReadLine();
                            if (choice == "y")
                            {
                                //play method goes here
                                return player;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"The word [{choice}] is worth 0 points.");
                        }
                        break;
                    case "n":
                        Console.Write($"Are you giving up? (y/n): ");
                        choice = Console.ReadLine();
                        if (choice == "y")
                        {
                            return null;
                            Console.WriteLine("Game is over.\nScore");
                        }
                        else if (choice == "n")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Wrong Input");
                            break;
                        }
                }
            }
            return null;
        }
    }
}
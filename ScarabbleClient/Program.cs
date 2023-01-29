using ScrabbleLibrary;

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

Console.WriteLine("-----------------------------------------------------------------------------\n" +
"Player 1\n" + 
"---------------------------------------------------------------------------- - ");

string choice;


Console.WriteLine($"Yor rack contains [{players[0]}].");
while (true)
{
    Console.Write("Test a word for its points value? (y/n): ");
    choice = Console.ReadLine();

    if (choice == "y")
    {
        Console.Write($"Enter a word using the letters [{players[0]}]:");
        choice = Console.ReadLine();
        if (players[0].PlayWord(players[0].ToString()))
        {
            Console.WriteLine($"The word [{choice}] is worth {players[0].TestWord(choice)} points.");
            Console.WriteLine($"Do you want to play the word [{players[0].ToString()}]? (y/n): ");
            choice= Console.ReadLine();
            //"If  you see these changes say hi :)";
            if (choice == "y")
            {
                break;
            }
        }
        else
        {
            Console.WriteLine($"The word [{players[0]}] is worth 0 points.");
        }

    }
    else
    {
        break;
    }
}



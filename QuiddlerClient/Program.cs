using System;
using System.Collections.Generic;
using System.Text;
using QuiddlerLibrary;

namespace QuiddlerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            //Title
            Console.WriteLine(deck.About);
            Console.WriteLine($"\nDeck initialized with the following {deck.CardCount} cards...");

            //Show deck
            Console.WriteLine(deck.ToString());

            //Add number of Players and cards per player
            int numPlayers = askNumPlayers();
            int numCards = askNumCards();
            deck.CardsPerPlayer = numCards;
            List<IPlayer> playerList = new List<IPlayer>();
            for(int i = 0; i < numPlayers; i++)
            {
                playerList.Add(deck.NewPlayer());
            }

            Console.WriteLine($"\nCards were dealt to {numPlayers} player(s).");
            Console.WriteLine($"The top card which was '{deck.TopDiscard}' was moved to the discard pile.\n");

            //Start Game
            bool retire = false;
            do
            {
                for (int p = 0; p < playerList.Count; p++)
                {
                    string answer = "";
                    bool isValid = true;
                    int pNum = p + 1;
                    Console.WriteLine("‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐");
                    Console.WriteLine($"Player {pNum}   ({playerList[p].TotalPoints} points)");
                    Console.WriteLine("‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐");
                    Console.WriteLine($"\nThe deck now contains the following {deck.CardCount} cards...");
                    Console.WriteLine(deck.ToString());
                    Console.WriteLine($"\nYour cards are {playerList[p].ToString()}");
                    do
                    {
                        Console.Write($"Do you want the top card in the discard pile which is '{deck.TopDiscard}'? (y/n): ");
                        answer = Console.ReadLine();
                        if(answer == "y")
                        {
                            playerList[p].PickupTopDiscard();
                            Console.WriteLine($"Your cards are {playerList[p].ToString()}.");
                            isValid = true;
                        }
                        else if(answer == "n")
                        {
                            Console.WriteLine($"The dealer dealt '{playerList[p].DrawCard()}' to you from the deck.");
                            Console.WriteLine($"The deck contains {deck.CardCount} cards.");
                            Console.WriteLine($"Your cards are {playerList[p].ToString()}.");
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }while (!isValid);

                    do
                    {
                        // testing the word validity for it's point value
                        Console.Write($"Test a word for its points value? (y/n): ");
                        answer = Console.ReadLine();
                        if (answer == "y")
                        {
                            string word = "";
                            Console.Write($"Enter a word using {playerList[p].ToString()} leaving a space between cards: ");
                            word = Console.ReadLine();
                            int point = playerList[p].TestWord(word);
                            Console.WriteLine($"The word [{word}] is worth {point} points.");
                            if (point > 0)
                            {
                                Console.Write($"Do you want to play the word [{word}]? (y/n): ");
                                answer = Console.ReadLine();
                                if (answer == "y")
                                {
                                    playerList[p].PlayWord(word);
                                }
                                isValid = true;
                            }
                            else
                            {
                                isValid = false;
                            }
                        }
                        else if (answer == "n")
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    } while (!isValid);

                    Console.WriteLine($"Your cards are {playerList[p].ToString()} and you have {playerList[p].TotalPoints} points.");
                    do
                    {
                        if(playerList[p].CardCount > 0)
                        {
                            Console.Write("Enter a card from your hand to drop on the discard pile: ");
                        }
                        else
                        {
                            break;
                        }
                        answer = Console.ReadLine();
                    } while (!playerList[p].Discard(answer));
                    Console.WriteLine($"Your cards are {playerList[p].ToString()}.\n");
                    do
                    {
                        if (p == playerList.Count - 1)
                        {
                            Console.Write("\nWould you like each player to take another turn? (y/n): ");
                            answer = Console.ReadLine();
                            Console.WriteLine();
                            if(answer == "y")
                            {
                                p = 1;
                                isValid = true;
                            }
                            else if(answer == "n")
                            {
                                retire = true;
                                isValid = true;
                            }
                            else
                            {
                                isValid = false;
                            }
                        }//end if
                    } while (!isValid);
                }//end forloop
            } while (!retire);

            Console.WriteLine("\nRetiring the game.\n");
            Console.WriteLine("The final scores are...");
            Console.WriteLine("‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐");

            for(int i = 1; i <= playerList.Count; i++)
            {
                Console.WriteLine($"Player {i}: {playerList[i-1].TotalPoints} points");
            }

            deck.Dispose(); //calling the dispose method 
        }//end main

        //Helper methods
        static int askNumPlayers()
        {
            int numPlayer = 0;
            do
            {
                Console.Write("How many players are there? (1-8): ");
                Int32.TryParse(Console.ReadLine(), out numPlayer);
            } while (numPlayer < 1 || numPlayer > 8);
            return numPlayer;
        }// end method
        static int askNumCards()
        {
            int numCards = 0;
            Console.Write("How many cards will be dealt to each player? (3‐10): ");
            Int32.TryParse(Console.ReadLine(), out numCards);
            return numCards;
        }//end method
       
    }
}
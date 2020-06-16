using System;
using System.Linq;

namespace BlackJack1B
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("*********************");
			Console.WriteLine("Welcome to Black Jack 1B!");
			while (true)
			{
				Console.WriteLine("0 - Exit");
				Console.WriteLine("1 - Play");
				Console.Write("Your choice: ");
				var answer = Console.ReadLine();
				if (answer != "1" && answer != "0")
				{
					Console.WriteLine("Invalid Option");
				}
				switch (answer)
				{
					case "0":
						return;
					case "1":
						Game game = new Game(); //create new game, deal initial 2 cards to each player						
						Console.Write("How many players will play this game? "); //partially implemented multiplayer game
						var numOfPlayers = Console.ReadLine();						
						if (Convert.ToInt32(numOfPlayers) > 1) //if more than one, calls a different constructor of Game
						{
							game = new Game(Convert.ToInt32(numOfPlayers));
						}
						for (int i = 1; i < game.Players.Count; i++) //requests players names
						{
							Console.Write($"Player {i}'s name:");
							game.Players[i].Name = Console.ReadLine();
						}
						for (int i = 1; i < game.Players.Count; i++) //shows the cards of all players except the dealer
						{
							Console.WriteLine($"{game.Players[i].Name}'s cards: ");
							foreach (Card card in game.Players[i].Hand)
							{
								Console.WriteLine($"{card.Face}, {card.Value} ");
							}
							Console.WriteLine($"{game.Players[i].Name}'s total: {game.Players[i].GetSumOfAllCards()}"); //gives the sum of the hand for all players except dealer
						}
						Console.WriteLine($"Dealer's first card: {game.Dealer.Hand.ElementAt(0).Face}, {game.Dealer.Hand.ElementAt(0).Value}"); //dealer's first hand
						for (int i = 1; i < game.Players.Count; i++)
						{
							if (game.Dealer.HasBlackJack() && !game.Players[i].HasBlackJack()) //evaluates if the dealer has blackjack with each player
							{
								Console.WriteLine($"Sorry, dealer has BlackJack!. Dealer's hand:");
								foreach (var card in game.Dealer.Hand)
								{
									Console.WriteLine($"{card.Face}, {card.Value} ");
								}
								Console.WriteLine($"Dealer's sum: {game.Dealer.GetSumOfAllCards()}");
								Console.WriteLine($"{game.Players[i].Name}, on the other hand, got :" + game.Players[i].GetSumOfAllCards());
								foreach (var card in game.Players[i].Hand)
								{
									Console.WriteLine($"{card.Face}, {card.Value} ");
								}
								game.Players[i].Score--;
								Console.WriteLine($"{game.Players[i].Name}'s score: {game.Players[i].Score}.");
								break;
							}
							if (!game.Dealer.HasBlackJack() && game.Players[i].HasBlackJack()) //evaluates if each player has blackjack against dealer
							{
								Console.WriteLine($"{game.Players[i].Name} has BlackJack!. Dealer's hand:");
								foreach (var card in game.Dealer.Hand)
								{
									Console.WriteLine($"{card.Face}, {card.Value} ");
								}
								Console.WriteLine($"Dealer's sum: {game.Dealer.GetSumOfAllCards()}");
								Console.WriteLine($"{game.Players[i].Name}'s sum: {game.Players[i].GetSumOfAllCards()}, Hand:");
								foreach (var card in game.Players[i].Hand)
								{
									Console.WriteLine($"{card.Face}, {card.Value} ");
								}

								game.Players[i].Score--;
								Console.WriteLine($"{game.Players[i].Name}'s score: {game.Players[i].Score}.");
								break;
							}							
						}
						var condition = true; //setup break of while loop
						while (condition)
						{
							Console.Write($"Hit or stay? h/s: ");
							var answ = Console.ReadLine();
							if (answ != "h" && answ != "s")
							{
								{
									Console.WriteLine("Invalid Option");
								}
							}
							switch (answ)
							{
								case "h":
									for (int i = 1; i < game.Players.Count; i++) //hits all players first
									{
										game.HitPlayer(game.Players[i]);

										Console.WriteLine($"{game.Players[i].Name}'s cards: ");
										foreach (Card card in game.Players[i].Hand)
										{
											Console.WriteLine($"{card.Face}, {card.Value} ");
										}
										Console.WriteLine($"{game.Players[i].Name}'s total: ");
										Console.WriteLine($"{game.Players[i].GetSumOfAllCards()}");

										if (game.Players[i].IsBusted())
										{
											game.Players[i].Score--;
											Console.WriteLine($"Sorry, {game.Players[i].Name} busted. sum of Cards: {game.Players[i].GetSumOfAllCards()}");
											Console.WriteLine($"{game.Players[i].Name}'s score: {game.Players[i].Score}.");
											condition = false;
											break;
										}
										while (game.Players[i].HasBlackJack() && !game.Dealer.HasBlackJack())
										{
											game.HitDealer();
											while (!game.DealerWins(game.Players[i]))
											{
												Console.WriteLine($"{game.Players[i].Name} has BlackJack!. Dealer's hand:");
												foreach (var card in game.Dealer.Hand)
												{
													Console.WriteLine($"{card.Face}, {card.Value} ");
												}
												game.Players[i].Score++;
												condition = false;
											}
											break;											
										}																			
										///decide if dealer hits or not to be implemented										
									}
									game.HitDealer();
									for (int i = 1; i < game.Players.Count; i++)
									{
										if (game.Dealer.HasBlackJack() && !game.Players[i].HasBlackJack())
										{
											Console.WriteLine($"Sorry, dealer has BlackJack!. Dealer's hand:");
											foreach (var card in game.Dealer.Hand)
											{
												Console.WriteLine($"{card.Face}, {card.Value} ");
											}
											Console.WriteLine($"Dealer's sum: {game.Dealer.GetSumOfAllCards()}");
											Console.WriteLine($"{game.Players[i].Name}, on the other hand, got :" + game.Players[i].GetSumOfAllCards());
											foreach (var card in game.Players[i].Hand)
											{
												Console.WriteLine($"{card.Face}, {card.Value} ");
											}
											game.Players[i].Score--;
											Console.WriteLine($"{game.Players[i].Name}'s score: {game.Players[i].Score}.");
											break;
										}
									}
									Console.WriteLine($"Dealer's first card: {game.Dealer.Hand.ElementAt(0).Face}, {game.Dealer.Hand.ElementAt(0).Value}, Dealer's second card: {game.Dealer.Hand.ElementAt(1).Face}, {game.Dealer.Hand.ElementAt(1).Value}");

									break;
								case "s":
									for (int i = 1; i < game.Players.Count; i++)
									{
										if (game.DealerWins(game.Players[i]))
										{
											foreach (var player in game.Players)
											{
												Console.WriteLine($"{player.Name}'s hand:");
												foreach (var card in player.Hand)
												{
													Console.WriteLine($"{card.Face}, {card.Value}");
												}
												Console.WriteLine($"Sum: {player.GetSumOfAllCards()}");
												
											}
											Console.WriteLine($"{game.Players[i].Name} lost. Sum: {game.Players[i].GetSumOfAllCards()}. Dealer: {game.Dealer.GetSumOfAllCards()}");
											game.Players[i].Score--;
											break;
										}
										Console.WriteLine($"{game.Players[i].Name} won. Sum: {game.Players[i].GetSumOfAllCards()}. Dealer: {game.Dealer.GetSumOfAllCards()}");
										game.Players[i].Score++;
										Console.WriteLine($"{game.Players[i].Name}'s score: {game.Players[i].Score}.");

										break;
									}
									condition = false;
										break;
								default:
									Console.WriteLine("Sorry not a valid option");
									break;
							}
						}
						break;
					default:
						break;
				}
			}
		}		
	}
}

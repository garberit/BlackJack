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
						Game game = new Game();
						Console.Write("How many players will play this game? ");
						var numOfPlayers = Console.ReadLine();
						int num;
						if (Int32.TryParse(numOfPlayers, out num))
						{
							Console.WriteLine("Please enter a number");
						}
						if (Convert.ToInt32(numOfPlayers) > 1)
						{
							game = new Game(Convert.ToInt32(numOfPlayers));
						}
						for (int i = 1; i < game.Players.Count; i++)
						{
							Console.Write($"Player {i}'s name:");
							game.Players[i].Name = Console.ReadLine();
						}
						for (int i = 1; i < game.Players.Count; i++)
						{
							Console.WriteLine($"{game.Players[i].Name}'s cards: ");
							foreach (Card card in game.Players[i].Hand)
							{
								Console.WriteLine($"{card.Face}, {card.Value} ");
							}
							Console.WriteLine($"{game.Players[i].Name}'s total: ");
							Console.WriteLine($"{game.Players[i].GetSumOfAllCards()}");
						}
						Console.WriteLine($"Dealer's first card: {game.Dealer.Hand.ElementAt(0).Face}, {game.Dealer.Hand.ElementAt(0).Value}");
						DealerHasBlackJack(game);
						var condition = true;
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
									for (int i = 1; i < game.Players.Count; i++)
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
											condition = false;
											break;
										}
										if (game.Players[i].HasBlackJack())
										{
											game.Players[i].Score++;

											Console.WriteLine($"You have BlackJack!. Dealer's hand:");
											foreach (var card in game.Dealer.Hand)
											{
												Console.WriteLine($"{card.Face}, {card.Value} ");
											}
											break;
										}																			
										///decide if dealer hits or not to be implemented										
									}
									game.HitDealer();
									DealerHasBlackJack(game);
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

		private static void DealerHasBlackJack(Game game)
		{
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
					break;
				}
			}
		}
	}
}

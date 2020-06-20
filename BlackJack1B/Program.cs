using System;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace BlackJack1B
{
	class Program
	{
		static void Main(string[] args)
		{	
			string fn = "Invalid Option";
			TextWriter errStream = new StreamWriter(fn);

			Console.WriteLine("*********************");
			Console.WriteLine("Welcome to Black Jack 1B!");
			
			string numOfPlayers = "";
			Int16 h;		
					
			while (!Int16.TryParse(numOfPlayers, out h))
				{
					Console.Write("How many players will play this game? ");
					numOfPlayers = Console.ReadLine();
					if (!Int16.TryParse(numOfPlayers, out h))
					{
						throw new ArgumentException("Must specify a number");
					}
				}
				
				Players players = new Players();

			// adds new players, skips dealer at index 0 
			for (int i = 1;  i <  Int16.Parse(numOfPlayers) + 1; i++)
			{
				Player player = new Player();
				Console.Write($"Player {i}'s name:");
				player.Name = Console.ReadLine();
				players.Add(player);
			}

			var answer = new ConsoleKeyInfo();
			while (answer.KeyChar != '0')
			{
				Console.WriteLine("0 - Exit");
				Console.WriteLine("1 - Play");
				Console.Write("Your choice: ");
				answer = Console.ReadKey();
				if (answer.KeyChar != '1' && answer.KeyChar != '0')
				{
					Console.SetError(errStream);
				}
				switch (answer.KeyChar)
				{
					case '0':
						return;
					case '1':
						//create new game, deal initial 2 cards to each player	
						Game game = new Game(players);						

						//shows the cards of all players except the dealer
						for (int i = 1; i < game.Players.Count; i++) 
						{
							Console.WriteLine($"\r\n{game.Players[i].Name}'s cards: ");
							Console.WriteLine(game.Players[i].Hand.ToString());
							//gives the sum of the hand for all players except dealer
							Console.WriteLine($"{game.Players[i].Name}'s total: {game.Players[i].GetSumOfAllCards()}"); 
						}
						//dealer's first card
						Console.WriteLine($"Dealer's first card: {game.Dealer.Hand.ElementAt(0).Face}, {game.Dealer.Hand.ElementAt(0).Value}");

						//check blackjack
						if (game.Dealer.HasBlackJack())
						{
							for (int i = 1; i < game.Players.Count; i++)
							{
								if (!game.Players[i].HasBlackJack())
								{
									Console.WriteLine($"Sorry {game.Players[i].Name}, Dealer has BlackJack.");
									DealerWon(game.Players[i], game);
									break;
								}
								if (game.Players[i].HasBlackJack())
								{
									Console.WriteLine($"{game.Players[i].Name} and Dealer has BlackJack. Score remains same.");
									
									break;
								}

							}
							
							break;
						}

						//check other players for blackjack
						if (game.Players.Any(player => player.HasBlackJack()))
						{
							for (int i = 1; i < game.Players.Count; i++)
							{
								if (game.Players[i].HasBlackJack())
								{
									Console.WriteLine($" {game.Players[i].Name} has BlackJack.");
									PlayerWon(game.Players[i], game);
									break;
								}
							}
						}

						//check if anyone's left
						if (!game.CheckAnyoneLeft())
						{
							break;
						}


						//time for hit or stay
						for (int i = 1; i < game.Players.Count; i++)
						{
							Player player = game.Players[i];						
							if (!player.IsDealer)
							{
								var answ = '#';
								while (answ != 's')
								{
									Console.Write($"\r\nHit or stay? h/s: ");
									answ = Console.ReadLine()[0];
									if (answ != 'h' && answ != 's' )
									{
										Console.SetError(errStream);
									}
									switch (answ)
									{
										case 'h':
											game.HitPlayer(player);
											Console.WriteLine($"\r\n{player.Name}'s cards: ");
											Console.WriteLine(player.Hand.ToString());
											Console.WriteLine($"{player.Name}'s total: " + player.GetSumOfAllCards());

											if (player.IsBusted())
											{
												Console.WriteLine($"Sorry, {player.Name} busted.");
												player.Score--;
												Console.WriteLine($"{player.Name}'s score: {player.Score}. GAME OVER!");
												answ = 's';
											}
											if (player.HasBlackJack())
											{
												PlayerWon(player, game);
											}
											break;
										case 's':
											//players played, not dealer
											if (game.CheckAnyoneLeft())
											{
												//already implements hit dealer until 17 in game.hitdealer

												while (game.Dealer.GetSumOfAllCards() <= 17)
												{
													game.HitDealer();
												}

												for (int j = 1; j < game.Players.Count; j++)
												{
													if (game.Dealer.IsBusted())
													{
														PlayerWon(game.Players[j], game);
													}
													if (game.DealerWins(game.Players[j]))
													{
														DealerWon(game.Players[j], game);
														break;
													}
													if (!game.DealerWins(game.Players[j]))
													{
														PlayerWon(game.Players[j], game);
														break;
													}


												}
											}
											break;
										default:
											Console.WriteLine("Invalid Option");
											break;
									}
								}
							}
						}

											
						break;
					default:
						Console.WriteLine("Invalid Option");
						break;
				}

			}

			
				
		
		}
		private static void PlayerWon(Player player, Game game)
		{
			Console.WriteLine($"{player.Name} wins. Dealer's hand:");
			Console.WriteLine(game.Dealer.Hand.ToString());
			Console.WriteLine($"Dealer's sum: {game.Dealer.GetSumOfAllCards()}");
			player.Score++;
			Console.WriteLine($"{player.Name}'s cards: ");
			Console.WriteLine(player.Hand.ToString());
			Console.WriteLine($"{player.Name}'s total: " + player.GetSumOfAllCards());
		}

		private static void DealerWon(Player player, Game game)
		{
			Console.WriteLine($"Dealer wins. Dealer's hand:");
			Console.WriteLine(game.Dealer.Hand.ToString());
			Console.WriteLine($"Dealer's sum: {game.Dealer.GetSumOfAllCards()}");
			player.Score--;
			Console.WriteLine($"{player.Name}'s cards: ");
			Console.WriteLine(player.Hand.ToString());
			Console.WriteLine($"{player.Name}'s total: " + player.GetSumOfAllCards());
		}

	}

}

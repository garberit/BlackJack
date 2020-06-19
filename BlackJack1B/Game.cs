using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack1B
{
	class Game : IGame
	{
		public Deck GameDeck { get; set; }
		public Players Players { get; set; }
		public Player Dealer { get; set; }			

		public Game()
		{
			GameDeck = new Deck();
			InitializeNewGame();
		}
		public Game(int numOfPlayers)
		{
			GameDeck = new Deck();
			InitializeNewGame(numOfPlayers);
		}
		public Game(Players players)
		{
			GameDeck = new Deck();
			InitializeNewGame(players);
		}

		public void InitializeNewGame(int NumberOfPlayers = 1)
		{			
			GameDeck.Shuffle();
			Players = new Players();
			Dealer = new Player();
			Dealer.Name = "Dealer";
			Players.Add(Dealer);
			for (int i = 0; i < NumberOfPlayers; i++)
			{
				Players.Add(new Player());
			}
			DealFirstCards();			
		}
		public void InitializeNewGame(Players players)
		{
			GameDeck.Shuffle();
			Players = new Players();
			Dealer = new Player();
			Dealer.Name = "Dealer";
			Dealer.IsDealer = true;
			Players.Add(Dealer);

			foreach (Player player in players)
			{
				player.Hand = new Cards();
				Players.Add(player);
			}
			DealFirstCards();
		}

		public void DealFirstCards()
		{
			for (int i = 0; i < 2; i++)
			{
				foreach (Player player in Players)
				{
					player.Hand.Push(GameDeck.DrawCard());
				}
			}			
		}

		public void HitPlayer(Player player)
		{
			player.Hand.Push(GameDeck.Cards.Pop());
		}

		public void HitDealer()
		{
			if (Dealer.GetSumOfAllCards() <= 17)
			{
				Dealer.Hand.Push(GameDeck.Cards.Pop());
			}
		}

		public bool DealerWins(Player player)
		{
			bool answer = false;
			if (Dealer.IsBusted() && !player.IsBusted())
			{
				answer = false;
			}
			if (!Dealer.IsBusted() && player.IsBusted())
			{
				return true;
			}
			if (Dealer.GetSumOfAllCards() > player.GetSumOfAllCards())
			{
				return true;
			}
			return answer;
		}

		public bool IsPush(Player player)
		{
			return player.GetSumOfAllCards() == Dealer.GetSumOfAllCards() ? true : false;
		}

		public bool CheckAnyoneLeft()
		{
			int playerCount = Players.Count - 1;
			foreach (Player player in Players)
			{
				if (player.IsDealer)
				{
					continue;
				}

				if (player.IsBusted() || player.HasBlackJack())
				{
					playerCount--;
				}
			}

			return playerCount > 0 ? true : false;
		}

	}
}

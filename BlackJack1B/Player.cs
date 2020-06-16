using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace BlackJack1B
{
	class Player
	{
		public double Score { get; set; }
		public string Name { get; set; }
		public Cards Hand { get; set; }
		public bool IsDealer { get; set; }

		public Player()
		{
			Score = 0;
			Hand = new Cards();
			if (IsDealer == true) //see if can remove the ==true
			{
				Name = "Dealer";
			}
		}

		public int GetSumOfAllCards()
		{
			int sum = 0;
			foreach (var card in Hand)
			{	
				switch (card.Value)
				{
					case 2:
						sum += 2;
						break;
					case 3:
						sum += 3;
						break;
					case 4:
						sum += 4;
						break;
					case 5:
						sum += 5;
						break;
					case 6:
						sum += 6;
						break;
					case 7:
						sum += 7;
						break;
					case 8:
						sum += 8;
						break;
					case 9:
						sum += 9;
						break;
					case 10:
						sum += 10;
						break;					
					case 1:
						if (sum >= 11)
						{
							sum += 1;
						}
						else
						{
							sum += 11;
						}
						break;
					default:
						break;
				}			
			}
			return sum;
		}


		public bool HasBlackJack()
		{		
			return Hand.Count == 2 && GetSumOfAllCards() == 21 ? true : false;
		}
	

		public bool IsBusted()
		{
			return GetSumOfAllCards() > 21 ? true : false;
		}
	}
}

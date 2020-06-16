using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BlackJack1B
{
	class Deck : IDeck
	{
		public Cards Cards { get; set; }


        public Deck(int numberOfDecks = 1)
        {
            Cards = new Cards();

            for (int i = 0; i < numberOfDecks; i++)
            {
                foreach (string face in Enum.GetNames(typeof(Face)))
                {
                    foreach (int value in Enum.GetValues(typeof(Value)))
                    {
                        Card c = new Card()
                        {
                            Face = face,
                            Value = value
						};
                        Cards.Push(c);
                    };
                }
            }
            
        }

        public void Shuffle()
		{
            Random rnd = new Random();

            List<Card> cardList = Cards.ToList();

            for (int n = cardList.Count - 1; n > 0; --n)
            {
                int k = rnd.Next(n + 1);
                Card temp = cardList[n];
                cardList[n] = cardList[k];
                cardList[k] = temp;
            }

            Cards.Clear();

            foreach (Card card in cardList)
            {
                Cards.Push(card);
            };
        }

        public Card DrawCard()
		{
            return Cards.Pop();
		}
	}
}

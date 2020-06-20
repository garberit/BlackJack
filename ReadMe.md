# This is just a homework project

It works but wouldn't sell it to a casino just yet :p

Single player works, multi player still buggy.

Game class can be considered a factory. Sort of.

## Classes:

### [Card](https://github.com/garberit/BlackJack1B/blob/master/BlackJack1B/Card.cs)
Properties of the card itslef which are face (string) and value (int) derived from the enums in Enums class.

### [Cards](https://github.com/garberit/BlackJack1B/blob/master/BlackJack1B/Cards.cs)
Stack of card. 
ToString override method to reduce foreach occurance in Program

### [Deck](https://github.com/garberit/BlackJack1B/blob/master/BlackJack1B/Deck.cs)
Cards property. 
Initializes a deck, shuffle method for shuffling the cards and draws a card from the deck.

### [Enums](https://github.com/garberit/BlackJack1B/blob/master/BlackJack1B/Enums.cs)
Container for face and value enums.

### [Game](https://github.com/garberit/BlackJack1B/blob/master/BlackJack1B/Game.cs)
Acts as the facory. instantiates Deck, Players and Player(for dealer).
Implements several types of game instantiations, depending on the number of players. 
InitializeNewGame creates the deck and shuffles the cards.
DealFirstCards si the frst round of 2 cards for each player.
HitPlayer pops a card from the deck and adds to the player's hand.
HitDealer only runs on specific terms preset by game owner/
IsPush checks for tie.
CheckAnyoneLeft to verify everyone played their turn before the dealer plays. (Dealer plays last)

### [Player](https://github.com/garberit/BlackJack1B/blob/master/BlackJack1B/Player.cs)
Player properties- name, score, the stack of cards(hand) and if it is the dealer.
This class gets the sum of values of the hand with conditioning based on Ace. HasBlackJack checks for blackjack and is busted checks if sume is more than 21.

### [Players](https://github.com/garberit/BlackJack1B/blob/master/BlackJack1B/Players.cs)
List of Player.
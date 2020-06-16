using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack1B
{
	interface ICard
	{
		public string Face { get; set; }
		public int Value { get; set; }
	}
}

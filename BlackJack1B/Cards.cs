using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack1B
{
	class Cards : Stack<Card>
	{
		public string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = this.Count - 1; i >= 0; i--)
			{
				sb.Append(this.ToArray()[i].Face + " " + this.ToArray()[i].Value);
				if (i > 0)
				{
					sb.Append(" - ");
				}
			}
			return sb.ToString();
		}
	}
}

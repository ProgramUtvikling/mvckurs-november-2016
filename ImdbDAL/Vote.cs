using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ImdbDAL
{
	public class Vote
	{
		public int VoteId { get; set; }
		public int Value { get; set; }
		public Movie Movie { get; set; }
	}
}

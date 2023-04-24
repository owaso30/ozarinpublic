using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ozarin.Shared
{
	public class ScorePalam
	{
		public int? rank { get; set; }
		public int? score { get; set; }
		public int r_startreturn { get; set; }
		public int? chipsprice { get; set; }
		public int? yakitoriprice { get; set; }
		public int? tobiprice { get; set; }
		public int? yakumanprice { get; set; }
		public int? chipschange { get; set; }
		public int? yakitoristate { get; set; }
		public int? tobistate { get; set; }
		public int? yakumanstate { get; set; }
		public int f_bonusbyranking { get; set; }
		public int b_bonusbyranking { get; set; }
		public int? halfcalc()
		{
			return this.score - this.r_startreturn + (this.chipsprice * (this.chipschange ?? 0)) + (this.yakitoriprice * (this.yakitoristate ?? 0)) +
					(this.tobiprice * (this.tobistate ?? 0)) + (this.yakumanprice * (this.yakumanstate ?? 0));
		}
	}
}

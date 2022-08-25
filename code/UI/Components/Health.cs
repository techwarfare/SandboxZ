using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Sandbox.UI.Components
{
	public class Health : Panel
	{
		public Label HealthLabel;

		public Health()
		{
			HealthLabel = Add.Label( "100", "healthval" );
		}

		public override void Tick()
		{
			var player = Local.Pawn;
			if ( player != null )
			{
				HealthLabel.Text = $"{player.Health.CeilToInt()}";
			}
		}
	}
}

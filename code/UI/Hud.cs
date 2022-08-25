using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Components;

namespace SandboxZ.UI
{
	[UseTemplate]
	public partial class Hud : HudEntity<RootPanel>
	{
		public Hud()
		{
			if ( !IsClient )
				return;
			Log.Info( "Started HUD" );
			RootPanel.StyleSheet.Load( "/ui/hud.scss" );

			RootPanel.AddChild<Health>();
		}
	}
}

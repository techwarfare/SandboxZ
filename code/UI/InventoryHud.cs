using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

namespace SandboxZ.UI;

public class InventoryHud : HudEntity<RootPanel>
{
	public bool visible = false;
	public InventoryHud()
	{
		if ( !IsClient )
			return;
		Log.Info( "Loading Inventory hud" );
		//Used to center and style the inventory
		RootPanel.StyleSheet.Load( "/ui/inventoryhud.scss" );
		RootPanel.Style.Display = DisplayMode.None;
		//Player inventory
		RootPanel.AddChild<InventoryPanel>();
		//Other inventory
		//RootPanel.AddChild<InventoryPanel>();
	}
	
	public void ShowInventory()
	{
		Log.Info( "Show inventory!!" );
		
		if (visible)
		{
			RootPanel.Style.Display = DisplayMode.None;
		} else if ( !visible )
		{
			RootPanel.Style.Display = DisplayMode.Flex;
		}
		visible = !visible;
	}
}
/// <summary>
/// Basically one large panel that contains smaller panels
/// </summary>
public class InventoryPanel : Panel
{
	List<InventorySlot> items;
	public InventoryPanel()
	{
		items = new List<InventorySlot>();
		for ( int i = 0; i < 32; i++ )
		{
			items.Add( new InventorySlot(i) );
			items[i].Style.Height = 192;
			items[i].Style.Width = 192;
			items[i].Style.BackgroundImage = Texture.Load("/ui/icons/empty.png");
			AddChild( items[i] );
		}
		this.AddEventListener( "onclick", e => Log.Info( "Clicked" + e ) );
	}
}
public class InventorySlot : Panel
{
	public int slotId;
	public Image slotImage;
	public Label slotLabel;
	public Label slotWeight;
	public Label slotCount;
	public Label slotQuality;

	public InventorySlot(int slotId)
	{
		Add.Label( slotId.ToString(), "slotId" );
		slotLabel = Add.Label( "Empty", "slotLabel" );
		slotWeight = Add.Label( "W", "slotWeight" );
		slotWeight.Style.FontColor = Color.White;
		slotQuality = Add.Label( "Q", "slotQuality" );
		slotQuality.Style.FontColor = Color.Green;
		slotCount = Add.Label( "C", "slotCount" );
		slotCount.Style.FontColor = Color.Average(new Color[] {Color.Blue, Color.Green} );
	}
}

using Sandbox;
using SandboxZ.Classes;
using Sandbox.UI.Construct;
using SandboxZ.UI;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace SandboxZ;

/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players.
/// </summary>
public partial class SandboxZ : Sandbox.Game
{
	//0 for third or first person, 1 for first person
	public static int CameraMode = 0;

	public InventoryHud inventoryHud { get; protected set; }
	public SandboxZ()
	{
		if ( IsClient )
		{
			_ = new Hud();
			inventoryHud = new InventoryHud();
		}
	}
	/// <summary>
	/// A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		// Create a pawn for this client to play with
		var pawn = new Pawn();
		client.Pawn = pawn;

		// Get all of the spawnpoints
		var spawnpoints = Entity.All.OfType<SpawnPoint>();

		// chose a random one
		var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

		// if it exists, place the pawn there
		if ( randomSpawnPoint != null )
		{
			var tx = randomSpawnPoint.Transform;
			tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
			pawn.Transform = tx;
		}
	}

	public void ShowInventory()
	{
		inventoryHud.ShowInventory();
	}

	public override void Simulate( Client cl )
	{
		base.Simulate(cl);
		if ( Input.Pressed( InputButton.Menu ) )
		{
			if ( !IsClient )
				return;
			inventoryHud.ShowInventory();
		}
	}

	PlayerDataCollection[] CheckForPlayerData( Client cl )
	{
		var clientId = cl.PlayerId;
		if ( !FileSystem.Data.FileExists( "playerdata.json" ) )
			return null;

		PlayerDataCollection[] fileData = FileSystem.Data.ReadJson<PlayerDataCollection[]>( "playerdata.json" );

		Log.Info( fileData );

		return fileData;
	}

	void CreatePlayerData(Client cl )
	{
		long clientId = cl.PlayerId;
		bool createdFile = false;
		if ( !FileSystem.Data.FileExists( "playerdata.json" ) )
		{
			FileSystem.Data.WriteAllText( "playerdata.json", "" );
			createdFile = true;
		}

		PlayerData newPlayerData = new PlayerData(cl);
		Log.Info( "Creating playerdata " + newPlayerData.PlayerName );
		if ( createdFile )
		{
			PlayerDataCollection PlayerDataCollection = new PlayerDataCollection(1, newPlayerData);
			var collectionArray = new PlayerDataCollection[1];
			collectionArray[0] = PlayerDataCollection;
			FileSystem.Data.WriteJson( "playerdata.json", collectionArray );
		} else if ( !createdFile )
		{
			PlayerDataCollection[] fileContents = FileSystem.Data.ReadJson<PlayerDataCollection[]>( "playerdata.json" );
			var playerCount = fileContents.Count();
			var playerDataCollection = new PlayerDataCollection( (playerCount + 1), newPlayerData );
			fileContents.Append( playerDataCollection );

			FileSystem.Data.WriteJson( "playerdata.json", fileContents );
		}
	}
}

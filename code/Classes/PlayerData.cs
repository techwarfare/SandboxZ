using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace SandboxZ.Classes
{
	public class PlayerData
	{
		public string PlayerName { get; set; }
		public long PlayerIdentifier { get; set; }

		public PlayerData( Client cl )
		{
			this.PlayerName = cl.Name;
			this.PlayerIdentifier = cl.PlayerId;
		}
	}

	public class PlayerDataCollection
	{
		public int PlayerId { get; set; }
		public PlayerData PlayerData { get; set; }

		public PlayerDataCollection(int id, PlayerData playerData)
		{
			PlayerId = id;
			PlayerData = playerData;
		}
	}
}

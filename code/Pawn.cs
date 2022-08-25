using Sandbox;
using SandboxZ.Classes;
using SandboxZ.UI;
using System;
using System.Linq;

namespace SandboxZ;

partial class Pawn : Player
{
	int currentCamera = 0;

	PlayerData PlayerData;
	//Override base inventory with custom inventory
	//new public Inventory Inventory { get; private set; }
	/// <summary>
	/// Called when the entity is first created 
	/// </summary>
	public override void Spawn()
	{
		base.Spawn();

		if ( SandboxZ.CameraMode == 0 )
			CameraMode = new ThirdPersonCamera();
		else if ( SandboxZ.CameraMode == 1 )
		{
			currentCamera = 1;
			CameraMode = new FirstPersonCamera();
		}
		
		Controller = new WalkController();
		Animator = new StandardPlayerAnimator();

		SetupPhysicsFromModel( PhysicsMotionType.Keyframed );
		SetModel( "models/citizen/citizen.vmdl" );

		this.Health = 100;
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
	}

	public void ChangeCamera()
	{
		if (currentCamera == 0 )
		{
			currentCamera = 1;
			CameraMode = new FirstPersonCamera();
		} else if (currentCamera == 1 )
		{
			//Block changing if first person only
			if (SandboxZ.CameraMode == 1) return; 
			currentCamera = 0;
			CameraMode = new ThirdPersonCamera();
		}
	}

	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		if ( Input.Pressed( InputButton.Score ) )
		{
			if ( !IsClient )
				return;
			ChangeCamera();
		}
	}
}

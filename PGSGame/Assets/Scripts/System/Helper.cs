using UnityEngine;
using System.Collections;

namespace Helper 
{

	#region Reference Cache

	public class PlayerInput
	{
		public static string Horizontal = "Horizontal";
		public static string Vertical = "Vertical";
		public static string Jump = "Jump";

		public static string RightX = "RightX";
		public static string RightY = "RightY";
	}

	public class GameTag
	{
		public static string Untagged				= "Untagged";
		public static string Respawn				= "Respawn";
		public static string Finish					= "Finish";
		public static string EditorOnly				= "EditorOnly";
		public static string MainCamera				= "MainCamera";
		public static string Player					= "Player";
		public static string GameController			= "GameController";

		public static string PlayerCamera			= "PlayerCamera";

	}

	public class Resource
	{
		public static string AnimatorController = "System/PlayerAnimator";
	}

	public static class AnimatorConditions
	{
		public static string Speed = "Speed";
		public static string Direction = "Direction";
		public static string Grounded = "Grounded";
		public static string AirVelocity = "AirVelocity";

	}

	#endregion

	#region Finite State Machine Enumerations

	public enum CameraState
	{
		Normal
		,Target
	}

	public enum SpeedState
	{
		Walk
		,Run
		,Sprint
	}

	#endregion


	#region Object Structures

	public struct CameraTargetObject
	{
		private Vector3 position;
		private Transform xForm;

		public Vector3 Position
		{
			get{return position;}
			set{position = value;}
		}

		public Transform XForm
		{
			get{return xForm;}
			set{xForm = value;}
		}
		public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
		{
			position = pos;
			xForm = transform;
			xForm.name = camName;
			xForm.localPosition = Vector3.zero;
			xForm.localPosition = position;

		}

	}

	public struct CameraMountPoint
	{
		private Vector3 position;
		private Transform xForm;
		
		public Vector3 Position
		{
			get{return position;}
			set{position = value;}
		}
		
		public Transform XForm
		{
			get{return xForm;}
			set{xForm = value;}
		}
		public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
		{
			position = pos;
			xForm = transform;
			xForm.name = camName;
			xForm.localPosition = Vector3.zero;
			xForm.localPosition = position;
		}
		
	}


	#endregion


}

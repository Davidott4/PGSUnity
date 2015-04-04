using UnityEngine;
using System.Collections;

using Helper;

///<summary>
/// Summary
/// This Script is attached to the player and includes all dependancies that are required in order 
/// for the character controller system to function. No scripts included in this instance should ever be absent
/// 
/// INCLUDED:
/// 
/// PlayerCamera.cs - Gocerns the bahaviour of the camera and is the only active on the instance.
/// PlayerMotor.cs - Governs the bahavior of the player's movement & animation
/// </summary>

[RequireComponent(typeof(NetworkView))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCamera))]

[AddComponentMenu("APP/PlayerCharacter")]

public class PlayerCharacter : MonoBehaviour 
{




	#region Public Fields & Properties




	#endregion

	#region Private Fields & Properties

	private CharacterController _controller;
	private Animator _animator;
	private RuntimeAnimatorController _animatorController;

	#endregion

	#region Getters & Setters

	/// <summary>
	/// Get the animator Component.
	/// </summary>
	/// <value>The animator.</value>

	public Animator Animator
	{
		get { return this._animator;}
	}

	/// <summary>
	/// Gets the CharacterController component.
	/// </summary>
	/// <value>The cntroller.</value>
	public CharacterController Controller
	{
		get {return this._controller;}
	}

	#endregion

	#region System Methods


	private void Awake()
	{
		_animator = this.GetComponent<Animator> ();
		_controller = this.GetComponent<CharacterController> ();
	}
	// Use this for initialization
	void Start () 
	{
		//Ensure network component exits.
		if (GetComponent<NetworkView>() != null) 
		{
			if (GetComponent<NetworkView>().isMine || Network.peerType == NetworkPeerType.Disconnected)
			{
				_animatorController = Resources.Load(Resource.AnimatorController) as RuntimeAnimatorController;
				_animator.runtimeAnimatorController = _animatorController;

				_controller.center = new Vector3(0f, 1f, 0f);
				_controller.height = 1.8f;
			}
			else
			{
				enabled = false;
			}
		}
		else
		{
			Debug.Log ("The game has started without a NetworkViewComponent. Pleae Attach one");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	#endregion
}

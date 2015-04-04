using UnityEngine;
using System.Collections;

using Helper;

/// <summary>
/// This script is included with the PlayerCharacter script includes & governs the behavior of this instances 
/// ability to move around the environment and animate its model based on input
/// </summary>

public class PlayerMotor : MonoBehaviour {



	#region Public Fields & properties

	public float walkSpeed = 3f;
	public float runSpeed = 5f;
	public float sprintSpeed = 7f;
	public float _rotationSpeed = 100f;
	public float jumpHeight = 10f;
	public float gravity = 20f;

	#endregion

	#region private Fields & properties

	private float _vertical =0f;
	private float _horizontal = 0f;
	private float _moveSpeed;

	private float _airVelocity = 0f;

	private Transform _myXForm;

	private Vector3 _moveDirection = Vector3.zero;

	private CharacterController _controller;

	private Animator _animator;

	private SpeedState _speedState = SpeedState.Run;

	private PlayerCharacter _pc;
	private PlayerCamera _camera;
	private CameraState _cameraState;
	
	#endregion

	#region Getters and Setters

	/// <summary>
	/// Gets or sets the movespeed.
	/// </summary>
	/// <value>The movespeed.</value>

	public float Movespeed
	{
		get {return _moveSpeed;}
		set {_moveSpeed = value;}
	}
	
	#endregion

	#region System Methods

	// Use this for initialization
	void Start () 
	{
		if (GetComponent<NetworkView>().isMine || Network.peerType == NetworkPeerType.Disconnected)
		{
			//Cache references to the child component of this gameObject
			_myXForm	= this.GetComponent<Transform>();
			_pc			= this.GetComponent<PlayerCharacter>();
			_camera 	= this.GetComponent<PlayerCamera>();

			_animator 	= _pc.Animator;
			_controller = _pc.Controller;

			_animator.SetBool(AnimatorConditions.Grounded, true);
		}
		else
		{
			enabled = false;
		}

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		CalculateSpeed();

		_cameraState = _camera.CameraState;
		switch (_cameraState) 
		{
		case CameraState.Normal:

			//Allow the player to rotate to the camera
			if ( Input.GetAxis(PlayerInput.RightX) > 0.1f ||  Input.GetAxis(PlayerInput.RightX) < 0.1f  )
			{
				_myXForm.Rotate(0f, Input.GetAxis(PlayerInput.RightX) * _rotationSpeed * Time.deltaTime, 0f );

			}

			//Crap Hacky Solution that needs to be reworked

			if ( Input.GetAxis("MouseX") > 0.1f ||  Input.GetAxis("MouseX") < 0.1f  )
			{
				_myXForm.Rotate(0f, Input.GetAxis("MouseX") * _rotationSpeed * Time.deltaTime, 0f );
				
			}

			if (_controller.isGrounded == true)
			{
				_moveDirection = Vector3.zero;
				_airVelocity = 0;

				_animator.SetFloat(AnimatorConditions.AirVelocity, _airVelocity);
				_animator.SetBool(AnimatorConditions.Grounded, true);

				//Cache the values returned by human input into float variables
				_horizontal = Input.GetAxis(PlayerInput.Horizontal);
				_vertical = Input.GetAxis(PlayerInput.Vertical);

				// Set the cached input valuse as the conditions for the animator FSM
				_animator.SetFloat(AnimatorConditions.Direction, _horizontal);
				_animator.SetFloat(AnimatorConditions.Speed, _vertical);

				if (Input.GetButtonDown(PlayerInput.Jump))
				{
					Jump();
				}
			}
			else
			{
				_moveDirection.x = Input.GetAxis(PlayerInput.Horizontal) * _moveSpeed;
				_moveDirection.z = Input.GetAxis(PlayerInput.Vertical) * _moveSpeed;
				_moveDirection = _myXForm.TransformDirection(_moveDirection);

				_animator.SetBool(AnimatorConditions.Grounded, false);
			}
			
			break;
			
		case CameraState.Target:
			
			break;
			
			
		}
	
	}

	private void CalculateSpeed()
	{
		switch (_speedState)
		{
		case SpeedState.Walk:
			_moveSpeed= walkSpeed;
			break;

		case SpeedState.Run:
			_moveSpeed= walkSpeed;
			break;

		case SpeedState.Sprint:
			_moveSpeed= walkSpeed;
			break;

		}
	}

	private void Jump()
	{
		_moveDirection.y = jumpHeight;
		_airVelocity -=Time.time;
	}

	#endregion
}

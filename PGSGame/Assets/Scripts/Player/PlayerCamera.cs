using UnityEngine;
using System.Collections;
using Helper;

public class PlayerCamera : MonoBehaviour {


	#region Public Fields & Properties
	
	
	
	
	#endregion
	
	#region Private Fields & Properties

	private Vector3 _cameraNormalPosition = new Vector3(0.0f, 1.75f, -2.25f);
	[SerializeField]
	private float sensitivity = 5f;
	[SerializeField]
	private float minimumAngle = -40f;
	[SerializeField]
	private float maximumAngle = 60f;

	private float rotationY;
	private Transform _camera;
	private Transform _player;
	private PlayerCharacter _pc;
	private CameraState _state = CameraState.Normal;
	private CameraTargetObject _cameraTargetObject;
	private CameraMountPoint _cameraMountPoint;

	#endregion
	
	#region Getters & Setters

	public CameraState CameraState 
	{
			get{ return _state;}
	}

	#endregion
	
	#region Custom Methods
	// Use this for initialization
	void Start () 
	{

		if (GetComponent<NetworkView>().isMine || Network.peerType == NetworkPeerType.Disconnected)
		{
			_pc = this.GetComponent<PlayerCharacter>();

			_camera = GameObject.FindGameObjectWithTag(GameTag.PlayerCamera).transform;
			Debug.Log(_camera.position);
			_player = this.transform;
			_cameraTargetObject = new CameraTargetObject();
			_cameraTargetObject.Init
			(
				"Camera Target",
				new Vector3(0f,1f,0f),
				new GameObject().transform,
				_player.transform
					);

			//Create Empty Object at Runtime for Camera to sit on
			_cameraMountPoint = new CameraMountPoint();
			_cameraMountPoint.Init
				(
					"Camera Mount",
					_cameraNormalPosition,
					new GameObject().transform,
					_cameraTargetObject.XForm
					);

			_camera.parent = _cameraTargetObject.XForm.parent;
		}
		else
		{
			enabled = false;
		}
	}
	
	private void LateUpdate()
	{
		switch (_state) 
		{
			case CameraState.Normal:

			RotateCamera();
			_camera.position = _cameraMountPoint.XForm.position;

			break;

			case CameraState.Target:

			break;


		}

	}

	private void RotateCamera()
	{
		rotationY -= Input.GetAxis(PlayerInput.RightY) * sensitivity;
		rotationY = Mathf.Clamp(rotationY, minimumAngle, maximumAngle);

		_cameraTargetObject.XForm.localEulerAngles = new Vector3(-rotationY, _cameraTargetObject.XForm.localEulerAngles.y,0);
	}


	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour 
{
	private CharacterController controller;
	public float speed = 10f;
    public Transform bestScoreText;
    public Transform scoreText;

	public PlayerMotor motor;
	private Transform lookAt;
	private Vector3 startOffset;
	private Vector3 moveVector;
//	private float minimum = 0f;
//	private float maximum = 0f;

	//public Vector3 playerRelative;

	public bool needAcc;
	public Vector3 tempPos;
    public float cameraTurnSpeed;


    private void Awake()
    {
        if (Screen.width == 1125 && Screen.height == 2436)
        {
            Camera.main.fieldOfView = 85;
            bestScoreText.position = new Vector3(bestScoreText.position.x, bestScoreText.position.y - 65, bestScoreText.position.z);
            scoreText.position = new Vector3(scoreText.position.x, scoreText.position.y - 65, scoreText.position.z);
        }
        else if (Screen.width == 2048 && Screen.height == 2732)
        { 
            Camera.main.fieldOfView = 70;
        }
    }

	// Use this for initialization
	void Start () 
	{
        controller = GetComponent<CharacterController> ();
		lookAt = GameObject.FindGameObjectWithTag ("Player").transform;
		startOffset = transform.position - lookAt.position;

		needAcc = motor.needCamAcc;
	}
	
	// Update is called once per frame
	void Update () 
	{



//		if (motor.moveVector.y > speed)
//		{
//			speed = motor.moveVector.y + 5f;
//		}
//		else
//		{
//			speed = 10f;
//		}
		if (motor.buttonEnabled == true)
		{
			transform.position = lookAt.position + startOffset;
		}
		else
		{
			if (!lookAt)
			{
				return;
			}
			//Vector3 playerRelative = lookAt.InverseTransformPoint(transform.position);
			tempPos = transform.position;

			if (motor.needCamAcc == true)
			{
				tempPos.y = lookAt.position.y;
				this.transform.position = Vector3.Lerp (this.transform.position, tempPos, Time.deltaTime);
			}



			controller.Move ((Vector3.up * speed) * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0,0,0), cameraTurnSpeed * Time.deltaTime);
		}
	}

}

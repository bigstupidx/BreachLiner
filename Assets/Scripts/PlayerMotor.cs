using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class PlayerMotor : MonoBehaviour {

    public static PlayerMotor instance;

	private const float ACC_VALUE = 125f; 

	public GameObject playButtonUI;
    public Image tutorial;

	public GameObject saveMe;
	public Slider slider;

	public bool buttonEnabled = true;
	public bool isPaused = false;

	//public float thrustForce;
	public Rigidbody rb;

	private Animator animator;

	private CharacterController controller;
	public Vector3 moveVector;

	public bool gameOver = false;
	public bool isRight = false;
	public bool isLeft = true;
	public bool flagR = false;
	public bool flagL = false;

	public bool needBounce = false;

	private float speed = 20.0f;
	public float verticalVelocity = 20f;
	private float gravity = 10f;
	public float horizontalVelocity = 0f;
	private Vector3 horizontalPosition;
	public Vector3 tempPos;

	public bool needCamAcc = false;
	public Transform cam;
	public Vector3 cameraRelative;

	public GameObject explosionPrefab;

	private AudioSource explosionAudio;
	private ParticleSystem explosionParticles;

	private GameManager gm;
	private AudioManager am;
    private bool isTriggered;
//	public float lookTimer = 0f;
//	public float timerDuration = 2f;
//	public float startingAmount = 1f;
//	public float currentAmount;

	public bool isLookedAt = false;

	// Use this for initialization
	void Start () {
        instance = this;
		controller = GetComponent<CharacterController> ();
		controller.detectCollisions = false;

		animator = GetComponentInParent<Animator> ();
		rb = GetComponent<Rigidbody> ();

		cam = Camera.main.transform;
		gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameManager> ();
		am = GameObject.FindGameObjectWithTag ("AM").GetComponent<AudioManager> ();

		//controller.isTrigger = true;


		explosionParticles = Instantiate (explosionPrefab).GetComponent<ParticleSystem> ();
		//explosionAudio = explosionParticles.GetComponent<AudioSource> ();

		explosionParticles.gameObject.SetActive (false);

//		currentAmount = startingAmount;
		isLookedAt = false;
        isTriggered = false;

    }

	void OnEnable()
	{
		gameOver = false;
	}


    public void ToggleTutorial()
    {
        
        tutorial.gameObject.SetActive(!tutorial.gameObject.activeInHierarchy);
        //AnimationController.instance.ToggleTutorialImage();

    }

	// Update is called once per frame
	void Update () 
	{
		tempPos = transform.position;

		if (am.reloadScore == true)
		{
			am.reloadScore = false;
		}


		if (buttonEnabled == true)
		{
			controller.Move ((Vector3.up * 20f) * Time.deltaTime);
		}
		else
		{
            gm.ActivateScoreUI();

            ToggleTutorial();
            playButtonUI.SetActive (false);
			Vector3 cameraRelative = cam.InverseTransformPoint (transform.position);

			if (cameraRelative.y > 0)
			{
				needCamAcc = true;
			}
			else
			{
				needCamAcc = false;
			}

//			//rb.AddForce (Vector3.down * thrustForce, ForceMode.Acceleration);
			animator.SetBool ("goRight", false);
			animator.SetBool ("goLeft", false);
			animator.SetBool ("BounceRight", false);
			animator.SetBool ("BounceLeft", false);
//			moveVector = Vector3.zero;
//			if(horizontalVelocity == 0)
			if (Input.GetMouseButtonDown (0) && transform.position.x < 0)
			{
				verticalVelocity = speed;
				animator.SetBool ("goRight", true);
//				moveVector.x = 400f;
				//animator.SetBool ("goRight", false);



				horizontalVelocity = 50f;
				flagR = true;
				flagL = false;

				isLeft = false;
				isRight = true;

				//GoRight ();
			}
			if (Input.GetMouseButtonDown (0) && transform.position.x > 0)
			{
				verticalVelocity = speed;
				animator.SetBool ("goLeft", true);
//				moveVector.x = -400f;
				//animator.SetBool ("goLeft", false);



				horizontalVelocity = -50f;
				flagL = true;
				flagR = false;

				isLeft = true;
				isRight = false;

				//GoLeft ();
			}
//			animator.SetBool ("goRight", false);
//			animator.SetBool ("goLeft", false);
			//StartCoroutine (ExecuteAfterTime (.3f));
			//verticalVelocity = speed; //maybebecause not enough bonus speed gained

			if (needBounce == true)
			{
				horizontalVelocity = -horizontalVelocity;
				//		flagL = !flagL;
				//		flagR = !flagR;

				if (horizontalVelocity > 0)
				{
					GoRight ();
				}
				else
				if (horizontalVelocity < 0)
				{
					GoLeft ();
				}
				needBounce = false;
			}


			if (horizontalVelocity == 0 && verticalVelocity > 5)
			{
				verticalVelocity -= gravity * Time.deltaTime;
			}

			if (verticalVelocity <= 5)
			{
				verticalVelocity = 5;
			}

			if (transform.position.x >= 0 && flagR == true)
			{
				if (horizontalVelocity <= 0)
				{
					horizontalVelocity = 0;
//					tempPos.x = 10;
//					transform.position = tempPos;
					flagR = false;
				}
				else
				{
					horizontalVelocity -= (ACC_VALUE * Time.deltaTime);
				}

			}
			else
			if (transform.position.x <= 0 && flagL == true)
			{
				if (horizontalVelocity >= 0)
				{
					horizontalVelocity = 0;
//					tempPos.x = -10;
//					transform.position = tempPos;
					flagL = false;
				}
				else
				{
					horizontalVelocity += (ACC_VALUE * Time.deltaTime);
				}

			}

			if (transform.position.x > 10)
			{
				tempPos.x = 10;
				transform.position = Vector3.Lerp (transform.position, tempPos, Time.deltaTime);
			}

			if (transform.position.x < -10)
			{
				tempPos.x = -10;
				transform.position = Vector3.Lerp (transform.position, tempPos, Time.deltaTime);
			}

			//x - left & right
			moveVector.x = horizontalVelocity;



			//y - up & down
			moveVector.y = verticalVelocity;

			//z - Forward & back
			moveVector.z = 0f;


			//controller.Move ((Vector3.up * speed ) * Time.deltaTime);
			controller.Move (moveVector * Time.deltaTime);
		}
	}

//	IEnumerator ExecuteAfterTime (float time)
//	{
//		yield return WaitForSeconds (time);
//
//		SceneManager.LoadScene ("Game");
//	}
//
//	IEnumerator ExecuteAfterTime2 (float time)
//	{
//		yield return new WaitForSeconds (time);
//		isLeft = true;
//
//	}

	public void GoRight()
	{
		verticalVelocity = speed;
		animator.SetBool ("BounceRight", true);
		//				moveVector.x = 400f;
		//animator.SetBool ("goRight", false);



		horizontalVelocity = 50f;
		flagR = true;
		flagL = false;

		isLeft = false;
		isRight = true;
	}

	public void GoLeft()
	{
		verticalVelocity = speed;
		animator.SetBool ("BounceLeft", true);
		//				moveVector.x = -400f;
		//animator.SetBool ("goLeft", false);



		horizontalVelocity = -50f;
		flagL = true;
		flagR = false;

		isLeft = true;
		isRight = false;
	}

	public void enableDisableButton(bool whatever)
	{
		buttonEnabled = whatever;
	}

//	public void Bounce()
//	{
//		if (needBounce == true)
//		{
//			horizontalVelocity = -horizontalVelocity;
////		flagL = !flagL;
////		flagR = !flagR;
//
//			if (horizontalVelocity > 0)
//			{
//				GoRight ();
//			}
//			else
//			if (horizontalVelocity < 0)
//			{
//				GoLeft ();
//			}
//		}
//		needBounce = false;
//	}

	public void OnDeath()
	{
		explosionParticles.transform.position = transform.position;
		explosionParticles.gameObject.SetActive (true);

		explosionParticles.Play ();
		//explosionAudio.Play ();

		gameObject.SetActive (false);


		if (am.allowChance == true && Application.internetReachability != NetworkReachability.NotReachable)
		{
			saveMe.SetActive (true);
			isLookedAt = true;
		}
		else
		{
			gm.EndTheGame ();
            
            
		}
			

	}

//	public void SetUI()
//	{
//		slider.value = currentAmount;
//	}

	public void OnSaveMe()
	{
		isLookedAt = false;
		saveMe.SetActive (false);
        gm.mainMusicPlayer.Stop();
        UnityAds.instance.ShowAd("rewardedVideo");

	}

    public void SecondChance()
    {
        Debug.Log("Second Chance");
        am.reloadScore = true;
        am.allowChance = false;
        SceneManager.LoadScene("Game");
    }

    void Vibrate()
    {
        Handheld.Vibrate();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            isTriggered = true;
            StartCoroutine(TriggerTimer());
            //cols = 0;
            if (other.gameObject.tag == "pink")
            {
                //cols++;
                gm.IncreaseScore();
                other.GetComponent<WhenCollision>().OnHit(gm.scoreSound);
            }
            else if (other.gameObject.tag == "red1")
            {
                //cols++;
                //OnHit();
                print("die");
                Vibrate();
                Invoke("Vibrate", 0.5f);
                OnDeath();
                other.GetComponent<WhenCollision>().OnHit(gm.crashSound);
            }
            else if (other.gameObject.tag == "blue1")
            {
                print("Bounce");
                Vibrate();
                needBounce = true;
                //			Destroy (this.gameObject);
                tempPos = transform.position;
                
                other.GetComponent<WhenCollision>().ExecuteAfter1(.1f);
                //other.GetComponent<WhenCollision>().OnHit(gm.bounceSound);
                //this.gameObject.SetActive(false);
            }

            //		if (cols > 1)
            //		{
            //			Destroy (blueGo);
            //		}
            //
            //		if (this.gameObject.tag == "blue" && cols == 1)
            //		{
            //			
            //			Destroy (other.gameObject);
            //		}
            //		else if (this.gameObject.tag == "pink" && cols == 1)
            //		{
            //			
            //			Destroy (gameObject);
            //		}
        }

    }

    IEnumerator TriggerTimer()
    {
        yield return new WaitForSeconds(0.1f);
        isTriggered = false;
    }
}

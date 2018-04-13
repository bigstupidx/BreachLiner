using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhenCollision : MonoBehaviour {

	//private int cols = 0;
	//private GameObject blueGo;
	//private GameObject pinkGo;
	public PlayerMotor motor;
    public Material redMaterial;
    public GameObject flare;
	private AudioSource aud;
	private GameManager gm;
	private SpawnManager sm;

	private GameObject red1;
	private GameObject red2;
	private GameObject red3;
//	public Text tempScoreText;
                             //	public Text highScoreText;

	public int tempScore;
	public int highScore;

	public GameObject go;

	public Vector3 tempPos;

	void Awake()
	{
		motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
		aud = GameObject.FindGameObjectWithTag ("SM").GetComponent<AudioSource> ();

		gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameManager> ();
	}

	void Start()
	{
		//blueGo = GameObject.FindGameObjectWithTag ("blue");
		//pinkGo = GameObject.FindGameObjectWithTag ("pink");
//		motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
//		aud = GameObject.FindGameObjectWithTag ("SM").GetComponent<AudioSource> ();
//
//		gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameManager> ();

//		red1 = GameObject.FindGameObjectWithTag ("red1");
//		red2 = GameObject.FindGameObjectWithTag ("red2");
//		red3 = GameObject.FindGameObjectWithTag ("red3");
	}
    

	public void OnHit()
	{
		gameObject.SetActive(false);
		aud.Play ();
	}

	IEnumerator ExecuteAfterTime1 (float time)
	{
		yield return new WaitForSeconds (time);

        GetComponent<MeshRenderer>().material = redMaterial;
        gameObject.tag = "red1";
        flare.SetActive(true);
		//go = Instantiate (red1) as GameObject;
		//go.transform.SetParent (transform);
		//go.transform.position = tempPos;
		//this.gameObject.SetActive(false);
		//Destroy (this.gameObject);
	}

    public void ExecuteAfter1(float time)
    {
        StartCoroutine(ExecuteAfterTime1(time));
    }

}

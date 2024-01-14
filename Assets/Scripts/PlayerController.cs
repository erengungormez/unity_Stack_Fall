using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    bool carpma;
    float currentTime;
    bool invincible;
    private Obstacle[] obstacles;

    [SerializeField] AudioClip win, death, idestory, destory, bounce;

    public int currentObstacleNumber;
    public int totalObstacleNumber;


    public Image InvectableSlider;
    public GameObject InvictableOBJ;
    public GameObject gameOverUI;
    public GameObject finishUI;



    [SerializeField] public GameObject fireShield;


    

    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }
    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();


        // Nesnenin i�indeki t�m Obstacle bile�enlerini al�r.
        obstacles = gameObject.GetComponentsInChildren<Obstacle>();

        // obstacles dizisi null m� diye kontrol edilebilir.
        if (obstacles == null && obstacles.Length == 0)
        {
            Debug.LogWarning("ObstacleController: Hi� Obstacle bile�eni bulunamad� veya dizi bo�.");
        }

        currentObstacleNumber = 0;
    }

    void Start()
    {
        totalObstacleNumber = FindObjectsOfType<ObstacleController>().Length;
    }

    void Update()
    {

        if(playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0)) // E�er farenin sol t�k�na t�klarsan
            {
                carpma = true;
            }
            if (Input.GetMouseButtonUp(0)) // E�er elimizi sol t�ktan �ekersek 
            {
                carpma = false;
            }


            if (invincible)
            {
                currentTime -= Time.deltaTime * .35f;

                if (!fireShield.activeSelf)
                {
                    fireShield.SetActive(true);
                    ParticleSystem particleSystem = fireShield.GetComponent<ParticleSystem>();
                    particleSystem.Play();
                }
            }
            else
            {
                if (fireShield.activeSelf)
                {
                    fireShield.SetActive(false);
                }


                if (carpma)
                {
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;

                }

            }


            if (currentTime>= 0.15f || InvectableSlider.color == Color.red)
            {
                InvictableOBJ.SetActive(true) ;
            }
            else
            {
                InvictableOBJ.SetActive(false);      
            }




            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
                Debug.Log("invincible: " + currentTime);
                InvectableSlider.color  = Color.red;
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                Debug.Log("---------");
                InvectableSlider.color= Color.white;

            }

            if (InvictableOBJ.activeInHierarchy)
            {
                InvectableSlider.fillAmount = currentTime / 1;
            }



        }

       /* if (playerState == PlayerState.Prepare)
        {
            if (Input.GetMouseButton(0))
            {
                playerState = PlayerState.Playing;
            }
        }
       */
        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }

    }

    public void shatterObstacle() // Par�alanan obstaclelar
    {


        if (invincible)
        {
            ScoreManager.intance.addScore(1);

        }
        else
        {
            ScoreManager.intance.addScore(2);

        }

    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Playing)
        {
            // A�a�� y�nde hareket
            if (carpma)
            {
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            } 
        }
        
        
    }

    private void OnCollisionEnter(Collision collision) // OnCollisionEnter metodu, bu nesne bir ba�ka nesne ile �arp��t���nda �a�r�l�r. 
    {   
        // Yukar� y�nde hareket
        if (!carpma) 
        { 
            rb.velocity = new Vector3(0,50 * Time.deltaTime *5 ,0);  // "Time.deltaTime" her bilgisaayrda ayn� frame de �al��mas� i�indir.
        }
        else // Top �arparsa
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane") // E�er �arpan collision tag'� "enemy" ismindeyse
                {
                   // Destroy(collision.transform.parent.gameObject); // T�m collisionlar� yok et
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    shatterObstacle();
                    SoundManager.instance.playSoundFX(death, 0.5f);
                    currentObstacleNumber++;    


                }
            }
            else //�arpm�yosa
            {
                if (collision.gameObject.tag == "enemy") // E�er �arpan collision tag'� "enemy" ismindeyse
                {
                    // Destroy(collision.transform.parent.gameObject); // T�m collisionlar� yok et
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    shatterObstacle();
                    SoundManager.instance.playSoundFX(idestory, 0.5f);
                    currentObstacleNumber++;

                }

                else if (collision.gameObject.tag == "plane") // E�er �arpan collision tag'� "plane" ismindeyse
                {
                    Debug.Log("Game Over");
                    gameOverUI.SetActive(true);
                    playerState = PlayerState.Finish;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    ScoreManager.intance.ResetScore();
                    SoundManager.instance.playSoundFX(death, 0.5f);

                }
            }

        }


        FindObjectOfType<GameUI>().LevelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);




        if (collision.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            SoundManager.instance.playSoundFX(win, 0.5f);
            finishUI.SetActive(true);
            finishUI.transform.GetChild(0).GetComponent<Text>().text = "Level" + PlayerPrefs.GetInt("Level" , 1);

        }


    }

    private void OnCollisionStay(Collision collision) // OnCollisionStay metodu, bu nesnenin bir ba�ka nesne ile �arp��t��� s�rece her karede �a�r�l�r.
    {
        // Yukar� y�nde hareket
        if (!carpma || collision.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.instance.playSoundFX(bounce, 0.5f);

        }
    }
}

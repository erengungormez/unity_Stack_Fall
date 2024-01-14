using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] obstacleModel;
    [HideInInspector]
    public GameObject[] obstaclePrefab = new GameObject[4];

    public GameObject winPrefab;
    private GameObject temp1obstacle,temp2obstacle;

    private int level =1, addNumber =7;

    float obstacleNumber;

    public Material plateMat, baseMat;
    public MeshRenderer playerMeshRenderer;


    

    void Awake()
    {

        level = PlayerPrefs.GetInt("Level", 1);

        randomObstaclegenerator(); // Engellerin rastgele se�ilmesini sa�layan fonksiyonu �a��r�r.

        float randomNumber = Random.value;
                
        for (obstacleNumber = 0; obstacleNumber > -level - addNumber; obstacleNumber -= 0.5f) // Belirli bir ko�ula kadar engel seti olu�turur.
        {

            if (level <= 20)
            {
                temp1obstacle = Instantiate(obstaclePrefab[Random.Range(0, 2)]); // Rastgele bir engel prefab'� se�er ve klonlar.
            }
            if (level > 20 && level<50)
            {
                temp1obstacle = Instantiate(obstaclePrefab[Random.Range(1, 3)]); 
            }
            if (level >= 50 && level <= 100)
            {
                temp1obstacle = Instantiate(obstaclePrefab[Random.Range(2, 4)]); 
            }
            if (level > 100)
            {
                temp1obstacle = Instantiate(obstaclePrefab[Random.Range(3, 4)]); 
            }

            temp1obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0); // Engelin pozisyonunu ayarlar.
            temp1obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0); // Engelin d�n�� a��s�n� ayarlar.



            if (Mathf.Abs(obstacleNumber) >= level * .3 && Mathf.Abs(obstacleNumber) <= level * .6f) // E�er engel numaras�n�n mutlak de�eri, oyun seviyesinin %30'undan b�y�k ve ayn� zamanda %60'�ndan k���k veya e�itse:

            {
                temp1obstacle.transform.eulerAngles= new Vector3(0, obstacleNumber * 8, 0);    // Engelin d�n���n� ayarla: Yatay eksende d�n�� miktar�, engel numaras�n�n 8 kat�.

                temp1obstacle.transform.eulerAngles += Vector3.up * 180;    // Daha sonra, engeli 180 derece �evir.

            }
            else if (Mathf.Abs(obstacleNumber) > level*0.8f) // E�er engel numaras�n�n mutlak de�eri, oyun seviyesinin %80'inden b�y�kse:
            {
                temp1obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);    // Engelin d�n���n� ayarla: Yatay eksende d�n�� miktar�, engel numaras�n�n 8 kat�.


                if (randomNumber>0.75f)   // E�er rastgele say� 0.75'ten b�y�kse, engeli bir kez daha 180 derece �evir.
                {
                    temp1obstacle.transform.eulerAngles += Vector3.up * 180;

                }

            }

            temp1obstacle.transform.parent = FindObjectOfType<RotateManager>().transform; // RotateManager s�n�f�n� bul ve transform(y�n) de�erlerini bul, templeobstacle'daki t�m transformalara e�itle

        }

        temp2obstacle = Instantiate(winPrefab);
        temp2obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0); // Engelin pozisyonunu ayarlar.

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            plateMat.color = Random.ColorHSV(0,1,0.5f,1,1,1);
            baseMat.color = plateMat.color + Color.gray;
            playerMeshRenderer.material.color = baseMat.color;
        }
    }

    public void randomObstaclegenerator()
    {
        int random = Random.Range(0,5); // Circle,Flower,Hex,Spike,Square'den bir tanesini random se�mek i�in.

        switch (random)
        {
            case 0: // 0. olas�l�kta

                // "Circle" kategorisi i�in prefab'lar� obstaclePrefab'e kopyala   
                for (int i = 0; i < 4; i++) 
                {
                    obstaclePrefab[i] = obstacleModel[i]; 
                }
                break;

            case 1:
                // "Flower"  kategorisi i�in prefab'lar� obstaclePrefab'e kopyala
                for (int i = 0; i < 4; i++) 
                {
                    obstaclePrefab[i] = obstacleModel[i+4]; //8'e artan Model objelerini Prefab'a kopyala
                }
                break;

            case 2:
                // "Hex" kategorisi i�in prefab'lar� obstaclePrefab'e kopyala
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i + 8]; //12'ye artan Model objelerini Prefab'a kopyala
                }
                break;

            case 3:
                // "Spike" kategorisi i�in prefab'lar� obstaclePrefab'e kopyala
                for (int i = 0; i < 4; i++) 
                {
                    obstaclePrefab[i] = obstacleModel[i + 12]; //16 'ya artan Model objelerini Prefab'a kopyala 
                }
                break;

            case 4:
                // "Square" engel kategorisi i�in prefab'lar� obstaclePrefab'e kopyala
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i + 16]; //20'ye artan Model objelerini Prefab'a kopyala      
                }
                break;

            default:
                break;
        }
 
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }

}



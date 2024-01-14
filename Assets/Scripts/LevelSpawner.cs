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

        randomObstaclegenerator(); // Engellerin rastgele seçilmesini saðlayan fonksiyonu çaðýrýr.

        float randomNumber = Random.value;
                
        for (obstacleNumber = 0; obstacleNumber > -level - addNumber; obstacleNumber -= 0.5f) // Belirli bir koþula kadar engel seti oluþturur.
        {

            if (level <= 20)
            {
                temp1obstacle = Instantiate(obstaclePrefab[Random.Range(0, 2)]); // Rastgele bir engel prefab'ý seçer ve klonlar.
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
            temp1obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0); // Engelin dönüþ açýsýný ayarlar.



            if (Mathf.Abs(obstacleNumber) >= level * .3 && Mathf.Abs(obstacleNumber) <= level * .6f) // Eðer engel numarasýnýn mutlak deðeri, oyun seviyesinin %30'undan büyük ve ayný zamanda %60'ýndan küçük veya eþitse:

            {
                temp1obstacle.transform.eulerAngles= new Vector3(0, obstacleNumber * 8, 0);    // Engelin dönüþünü ayarla: Yatay eksende dönüþ miktarý, engel numarasýnýn 8 katý.

                temp1obstacle.transform.eulerAngles += Vector3.up * 180;    // Daha sonra, engeli 180 derece çevir.

            }
            else if (Mathf.Abs(obstacleNumber) > level*0.8f) // Eðer engel numarasýnýn mutlak deðeri, oyun seviyesinin %80'inden büyükse:
            {
                temp1obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);    // Engelin dönüþünü ayarla: Yatay eksende dönüþ miktarý, engel numarasýnýn 8 katý.


                if (randomNumber>0.75f)   // Eðer rastgele sayý 0.75'ten büyükse, engeli bir kez daha 180 derece çevir.
                {
                    temp1obstacle.transform.eulerAngles += Vector3.up * 180;

                }

            }

            temp1obstacle.transform.parent = FindObjectOfType<RotateManager>().transform; // RotateManager sýnýfýný bul ve transform(yön) deðerlerini bul, templeobstacle'daki tüm transformalara eþitle

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
        int random = Random.Range(0,5); // Circle,Flower,Hex,Spike,Square'den bir tanesini random seçmek için.

        switch (random)
        {
            case 0: // 0. olasýlýkta

                // "Circle" kategorisi için prefab'larý obstaclePrefab'e kopyala   
                for (int i = 0; i < 4; i++) 
                {
                    obstaclePrefab[i] = obstacleModel[i]; 
                }
                break;

            case 1:
                // "Flower"  kategorisi için prefab'larý obstaclePrefab'e kopyala
                for (int i = 0; i < 4; i++) 
                {
                    obstaclePrefab[i] = obstacleModel[i+4]; //8'e artan Model objelerini Prefab'a kopyala
                }
                break;

            case 2:
                // "Hex" kategorisi için prefab'larý obstaclePrefab'e kopyala
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i + 8]; //12'ye artan Model objelerini Prefab'a kopyala
                }
                break;

            case 3:
                // "Spike" kategorisi için prefab'larý obstaclePrefab'e kopyala
                for (int i = 0; i < 4; i++) 
                {
                    obstaclePrefab[i] = obstacleModel[i + 12]; //16 'ya artan Model objelerini Prefab'a kopyala 
                }
                break;

            case 4:
                // "Square" engel kategorisi için prefab'larý obstaclePrefab'e kopyala
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



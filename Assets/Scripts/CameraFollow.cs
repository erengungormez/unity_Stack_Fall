using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 cameraPos;     // Kameranýn güncellenmiþ konumunu tutan vektör

    private Transform player, win;     // Oyuncu ve kazanan nesnenin Transform bileþenleri


    private float cameraOffset =4f;     // Kamera ile oyuncu arasýndaki baþlangýç mesafesi



    private void Awake()     // Oyun baþladýðýnda çalýþan fonksiyon

    {         
        player = FindObjectOfType<PlayerController>().transform;   // Oyuncu nesnesini bulup player deðiþkenine atama


    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (win == null) // Eðer win deðiþkeni (null) "boþ" ise
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();    // "win(Clone)" adýndaki nesneyi bulup win deðiþkenine atama
        }


        if (transform.position.y > player.position.y && transform.position.y > win.position.y + cameraOffset)
        // Eðer kamera yüksekliði oyuncu yüksekliðinden büyük ve kamera, kazanan nesnenin belirli bir mesafesinden yüksekse
        {
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);   // Kamera konumunu güncelleme

            transform.position = new Vector3(transform.position.x, cameraPos.y, -8.47f);  // Kamera pozisyonunu güncelleme
        }


    }
}

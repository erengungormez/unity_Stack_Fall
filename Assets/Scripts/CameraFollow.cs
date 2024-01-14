using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 cameraPos;     // Kameran�n g�ncellenmi� konumunu tutan vekt�r

    private Transform player, win;     // Oyuncu ve kazanan nesnenin Transform bile�enleri


    private float cameraOffset =4f;     // Kamera ile oyuncu aras�ndaki ba�lang�� mesafesi



    private void Awake()     // Oyun ba�lad���nda �al��an fonksiyon

    {         
        player = FindObjectOfType<PlayerController>().transform;   // Oyuncu nesnesini bulup player de�i�kenine atama


    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (win == null) // E�er win de�i�keni (null) "bo�" ise
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();    // "win(Clone)" ad�ndaki nesneyi bulup win de�i�kenine atama
        }


        if (transform.position.y > player.position.y && transform.position.y > win.position.y + cameraOffset)
        // E�er kamera y�ksekli�i oyuncu y�ksekli�inden b�y�k ve kamera, kazanan nesnenin belirli bir mesafesinden y�ksekse
        {
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);   // Kamera konumunu g�ncelleme

            transform.position = new Vector3(transform.position.x, cameraPos.y, -8.47f);  // Kamera pozisyonunu g�ncelleme
        }


    }
}

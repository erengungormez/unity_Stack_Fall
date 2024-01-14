using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Obstacle[] obstacles = null;


    public void ShatterAllObstacles() // T�m engelleri par�alayan metot
    {
        if (transform.parent != null)
        {
            transform.parent = null;  // E�er bu nesne bir ebeveyn objeye ba�l�ysa, ba�lant�s�n� kopar.
        }


        foreach (Obstacle item in obstacles) // Engellerin �zerinde d�nerek her birini par�alayan metodu �a��r.
        {
            item.Shatter();
        }

        StartCoroutine(RemoveAllShatterParts()); // T�m par�alar� kald�rmak i�in belirli bir s�re bekleyen ve ard�ndan bu ObstacleController nesnesini yok eden bir coroutine ba�lat.


    }

    IEnumerator RemoveAllShatterParts() // Par�alanm�� t�m k�s�mlar� kald�ran coroutine
    {
        yield return new WaitForSeconds(1); // 1 saniye bekle
        Destroy(gameObject); // Bu nesneyi yok et
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

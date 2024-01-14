using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Obstacle[] obstacles = null;


    public void ShatterAllObstacles() // Tüm engelleri parçalayan metot
    {
        if (transform.parent != null)
        {
            transform.parent = null;  // Eðer bu nesne bir ebeveyn objeye baðlýysa, baðlantýsýný kopar.
        }


        foreach (Obstacle item in obstacles) // Engellerin üzerinde dönerek her birini parçalayan metodu çaðýr.
        {
            item.Shatter();
        }

        StartCoroutine(RemoveAllShatterParts()); // Tüm parçalarý kaldýrmak için belirli bir süre bekleyen ve ardýndan bu ObstacleController nesnesini yok eden bir coroutine baþlat.


    }

    IEnumerator RemoveAllShatterParts() // Parçalanmýþ tüm kýsýmlarý kaldýran coroutine
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

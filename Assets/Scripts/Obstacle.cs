using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rigidbody; // Rigidbody bileþeni, nesnenin fiziksel özelliklerini kontrol eder
    private MeshRenderer meshRenderer; // MeshRenderer bileþeni, nesnenin görüntüsünü kontrol eder
    private Collider collider; // Collider bileþeni, nesnenin çarpýþma özelliklerini kontrol eder
    private ObstacleController obstacleController;



    private void Awake()
    {
        // Nesnenin Rigidbody, MeshRenderer ve Collider bileþenlerine eriþim saðlanýr.

        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        obstacleController = transform.parent.GetComponent<ObstacleController>();
    }


    void Start()
    {

    }

    void Update()
    {

    }

    public void Shatter() // Engeli kýrmak (parçalamak) için çaðrýlan metot.
    {
        rigidbody.isKinematic = false;     // Nesnenin kinematik özelliði kapatýlýr, bu sayede fiziksel etkileþimlere izin verilir.

        collider.enabled = false;    // Nesnenin çarpýþma özelliði kapatýlýr, bu sayede çarpýþmalara tepki verilmez.

        Vector3 forcePoint = transform.parent.position;     // Kýrýlma efektini oluþturmak için kullanýlacak kuvvet noktasý belirlenir.


        // Parent , X pozisyonu ve engelin merkez X pozisyonu alýnýr.

        float parentXpos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subdir = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;

        // Kuvvetin uygulanacaðý yönde birim vektör hesaplanýr.
        Vector3 dir = (Vector3.up * 1.5f + subdir).normalized;

        // Rastgele bir kuvvet ve tork deðeri belirlenir.
        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        // Kuvvet ve tork, belirtilen noktaya Impulse kuvvetiyle uygulanýr.
        rigidbody.AddForceAtPosition(dir*force, forcePoint, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.left * torque);
       
        
        rigidbody.velocity = Vector3.down; // Nesnenin düþme hareketi baþlatýlýr.


    }



}



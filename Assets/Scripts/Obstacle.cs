using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rigidbody; // Rigidbody bile�eni, nesnenin fiziksel �zelliklerini kontrol eder
    private MeshRenderer meshRenderer; // MeshRenderer bile�eni, nesnenin g�r�nt�s�n� kontrol eder
    private Collider collider; // Collider bile�eni, nesnenin �arp��ma �zelliklerini kontrol eder
    private ObstacleController obstacleController;



    private void Awake()
    {
        // Nesnenin Rigidbody, MeshRenderer ve Collider bile�enlerine eri�im sa�lan�r.

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

    public void Shatter() // Engeli k�rmak (par�alamak) i�in �a�r�lan metot.
    {
        rigidbody.isKinematic = false;     // Nesnenin kinematik �zelli�i kapat�l�r, bu sayede fiziksel etkile�imlere izin verilir.

        collider.enabled = false;    // Nesnenin �arp��ma �zelli�i kapat�l�r, bu sayede �arp��malara tepki verilmez.

        Vector3 forcePoint = transform.parent.position;     // K�r�lma efektini olu�turmak i�in kullan�lacak kuvvet noktas� belirlenir.


        // Parent , X pozisyonu ve engelin merkez X pozisyonu al�n�r.

        float parentXpos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subdir = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;

        // Kuvvetin uygulanaca�� y�nde birim vekt�r hesaplan�r.
        Vector3 dir = (Vector3.up * 1.5f + subdir).normalized;

        // Rastgele bir kuvvet ve tork de�eri belirlenir.
        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        // Kuvvet ve tork, belirtilen noktaya Impulse kuvvetiyle uygulan�r.
        rigidbody.AddForceAtPosition(dir*force, forcePoint, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.left * torque);
       
        
        rigidbody.velocity = Vector3.down; // Nesnenin d��me hareketi ba�lat�l�r.


    }



}



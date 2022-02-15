using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bullet;
    public float shootSpeed=1000f;
    public GameObject Senserdir;
    private GameObject hitBullet;
    private Vector3 direction;
    private Rigidbody rb;
    bool once;
    int a;

    void Update()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, 5, Senserdir.transform.forward, out hit);
        Debug.DrawRay(transform.position, Senserdir.transform.forward , Color.red, hit.distance);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("enemy") && !once )
            {
                Shoot();
                Debug.Log("Shoot");
                once = true;
                Invoke(nameof(againShoot),0.5f);
            }
        }
        else
        {
            Debug.Log("else");
        }
    }
    
    public void Shoot()
    {
        hitBullet =  Instantiate(bullet,transform.position,transform.rotation);
        rb = hitBullet.GetComponent<Rigidbody>();
        direction = (BuildingAltitudePicking.instance.enemy[a].transform.position - transform.position);
        //BuildingAltitudePicking.instance.enemy[a].SetActive(false);
        GameObject parent = BuildingAltitudePicking.instance.enemy[a];
        Destroy(parent);

        rb.AddForce(direction * shootSpeed, ForceMode.Acceleration) ;
        a++;
    }

    void againShoot()
    {
        once = false;
    }
}

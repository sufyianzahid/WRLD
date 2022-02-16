using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bullet,ShootByTag;
    public float shootSpeed=1000f;
    public GameObject Senserdir;
    private GameObject hitBullet;
    private Vector3 direction;
    private Rigidbody rb;
    bool once;
   
    void Update()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, 5, Senserdir.transform.forward, out hit);
        Debug.DrawRay(transform.position, Senserdir.transform.forward , Color.red, hit.distance);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("enemy") && !once )
            {
                ShootByTag = hit.collider.gameObject;
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
        direction = (ShootByTag.transform.position - transform.position);
        //BuildingAltitudePicking.instance.enemy[a].SetActive(false);
        GameObject parent = ShootByTag;
        Destroy(parent);
        rb.AddForce(direction * shootSpeed, ForceMode.Acceleration) ;
    }

    void againShoot()
    {
        once = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public bool isRight;
    private Vector3 velo;
    public Vector3 pos;
    public float speed; 

    public Rigidbody rb; 
    void Start()
    {
        velo = isRight ? new Vector3(0,0,1) : new Vector3(0, 0, -1);
    }

    void Update()
    {
        if (transform.position.z > 17 || transform.position.z < -7)
        {
            this.transform.position = pos;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + speed * velo * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SoundManager.Instance.Play("OnWoodLand");
            collision.transform.parent = this.transform;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public bool isRight;
    public float speed;
    private Vector3 velo;
    public Vector3 pos; 
    public int delta;

    [SerializeField] private Rigidbody rb; 

    private void Start()
    {
        velo = isRight ? new Vector3(0,0,1) : new Vector3(0,0, -1);
        transform.rotation = Quaternion.Euler(0, isRight ? 0 : 180, 0);
        //rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // if out of bound, return back to init pos 
        if (transform.position.z < -5  - delta*speed || transform.position.z > 15   + delta*speed)
            transform.position = pos;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + speed * velo * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            GameManager.Instance.setGameOver(true);
    }
}

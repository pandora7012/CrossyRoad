using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class Player : MonoBehaviour
{
    public bool isJumping = false;
    private Vector2 touchDown;
    private Vector2 touchUp;
    private Vector2 drs;
    float angle;

    [SerializeField]private int pos = 5; 

    [SerializeField]private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        inputHandle();
    }

    private void FixedUpdate()
    {
        
    }


    private void inputHandle()
    {
        
        if (isJumping)
            return;
        // get pos touchdown 
        if (Input.GetMouseButtonDown(0))
            touchDown = Input.mousePosition;

        // get pos touch up and handle
        if (Input.GetMouseButtonUp(0))
        { 
            isJumping = true;
            transform.parent = null; 
            touchUp = Input.mousePosition;
            drs = touchUp - touchDown;
            angle= Mathf.Atan2(drs.y, drs.x);

            //check if just touch 
            if ( drs.x < 100 && drs.x > -100 
                 && drs.y < 100 && drs.y > -100)
            {
                goForward();
                return; 
            }

            // swift handle
             
            if (Mathf.Abs(angle) < 0.75)
                goRight();
            else if (angle >= 0.75 && angle < 2.25)
            {
                goForward();
            }
            else if (Mathf.Abs(angle) >= 2.25 && Mathf.Abs(angle) <= 3.5)
                goLeft();
            else
                goBack();
        }
    }

    private void goForward()
    {
        bool[] vs = GameManager.Instance.map[pos + 1];
        // check if front have obticle 
        if ( vs[(int) transform.position.z] )
        {
            rb.DOJump(transform.position, 0.5f, 1, 0.3f).OnComplete(() =>
            {
                isJumping = false;
            });
            return;
            
        }
        Vector3 temp = this.transform.position + new Vector3(1, 0, 0);
        rb.DOJump(temp, 1f, 1, 0.3f).OnComplete(() =>
        {
            isJumping = false;
            if (pos == 5)
            {
                Observer.Forward?.Invoke();
                GameManager.Instance.RemoveMap();
            }
            pos = Mathf.Min(pos + 1, 5);
            
            
        });
        
    }

    private void goLeft()
    {
        bool[] vs = GameManager.Instance.map[pos];
        // check if left have obticle 
        if (this.transform.position.z == 8 || vs[(int) transform.position.z+1])
        {
            rb.DOJump(transform.position, 0.5f, 1, 0.3f).OnComplete(() =>
            {
                isJumping = false;

            });
            return;
        }
        Vector3 temp = this.transform.position + new Vector3(0, 0, 1);
        rb.DOJump(temp, 1f, 1, 0.3f).OnComplete(() =>
        {
            isJumping = false;
            transform.rotation = Quaternion.identity;
        });
        
    }

    private void goRight()
    {
        bool[] vs = GameManager.Instance.map[pos];
        if (this.transform.position.z == 0 || vs [ (int) transform.position.z -1 ])
        {
            rb.DOJump(transform.position, 0.5f, 1, 0.3f).OnComplete(() =>
            {
                isJumping = false;
            });
            return;
        }
        Vector3 temp = this.transform.position - new Vector3(0, 0, 1);
        rb.DOJump(temp, 1f, 1, 0.3f).OnComplete(() =>
        {
            isJumping = false;
            transform.rotation = Quaternion.identity;
        }); 
    }


    private void goBack()
    {
        bool[] vs = GameManager.Instance.map[pos-1];
        if (this.transform.position.z == 0 || vs[(int)transform.position.z])
        {
            rb.DOJump(transform.position, 0.5f, 1, 0.3f).OnComplete(() =>
            {
                isJumping = false;
            });
            return;
        }
        Vector3 temp = this.transform.position - new Vector3(1, 0, 0);
        rb.DOJump(temp, 1f, 1, 0.3f).OnComplete(() =>
        {
            isJumping = false;
            transform.rotation = Quaternion.identity;
            pos = Mathf.Min(pos - 1, 5);
        });
    }

    private void resetPos()
    {
        touchDown = Vector3.zero;
        touchUp = Vector3.zero;
    }
}

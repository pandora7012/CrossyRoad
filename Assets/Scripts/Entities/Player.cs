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

    void Update()
    {
        if (!GameManager.Instance.GameOver)
        {
            inputHandle();
            checkGameOver();
        }
        
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
        Vector3 temp = RoundVector(this.transform.position);

        // check if front have obticle 
        if ( vs[ (int) temp.z] )
        {
            rb.DOJump(transform.position, 0.25f, 1, 0.15f).OnComplete(() =>
            {
                isJumping = false;
            });
            return;
        }

        //jump 
        temp += new Vector3(1, 0, 0);
        rb.DOJump(temp, 0.5f, 1, 0.2f).OnComplete(() =>
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
        Vector3 temp = RoundVector(this.transform.position);

        // check if  have obticle 
        if (temp.z == 8 || vs[(int) temp.z+1])
        {
            rb.DOJump(transform.position, 0.25f, 1, 0.15f).OnComplete(() =>
            {
                isJumping = false;

            });
            return;
        }

        // jump 
        temp += new Vector3(0, 0, 1);
        rb.DOJump(temp, 0.5f, 1, 0.2f).OnComplete(() =>
        {
            isJumping = false;
            transform.rotation = Quaternion.identity;
        });
        
    }

    private void goRight()
    {
        bool[] vs = GameManager.Instance.map[pos];
        Vector3 temp = RoundVector(this.transform.position);

        // check if have obticle 
        if (temp.z == 0 || vs [ (int) temp.z -1 ])
        {
            rb.DOJump(transform.position, 0.25f, 1, 0.15f).OnComplete(() =>
            {
                isJumping = false;
            });
            return;
        }

        //jump 
        temp -= new Vector3(0, 0, 1);
        rb.DOJump(temp, 0.5f, 1, 0.2f).OnComplete(() =>
        {
            isJumping = false;
            transform.rotation = Quaternion.identity;
        }); 
    }


    private void goBack()
    {
        bool[] vs = GameManager.Instance.map[pos-1];
        Vector3 temp = RoundVector(this.transform.position);

        //check obticle 
        if (this.transform.position.z == 0 || vs[(int)temp.z])
        {
            rb.DOJump(transform.position, 0.25f, 1, 0.15f).OnComplete(() =>
            {
                isJumping = false;
            });
            return;
        }

        temp -= new Vector3(1, 0);
        // jump 
        rb.DOJump(temp, 0.5f, 1, 0.2f).OnComplete(() =>
        {
            isJumping = false;
            transform.rotation = Quaternion.identity;
            pos = Mathf.Min(pos - 1, 5);
        });
    }

    

    private void checkGameOver()
    {
        if (!(transform.position.z <= 8 && transform.position.z >= 0) || transform.position.y < 0)
            GameManager.Instance.setGameOver(true);
    }

    private Vector3 RoundVector(Vector3 temp)
    {
        temp.z = Mathf.Round(temp.z);
        temp.x = Mathf.Round(temp.x);
        temp.y = 0.8f;
        return temp; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothness;
    public Vector3 targetPos = new Vector3(-1, 3.5f, 4);


    // Update is called once per frame
    void Update()
    { 
        if (!GameManager.Instance.GameOver)
        {
            targetPos.x += Time.deltaTime * 0.5f;
            targetPos.z = player.transform.position.z;
            checkGameOver();
        }
        else
        {
            targetPos.z -= Time.deltaTime * 0.2f;
        }
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        targetPos.x = Mathf.Max(player.transform.position.x, targetPos.x);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothness);
    }

    public void checkGameOver()
    {
        if (targetPos.x - 3.5 > player.transform.position.x)
            GameManager.Instance.setGameOver(true);
    }
}

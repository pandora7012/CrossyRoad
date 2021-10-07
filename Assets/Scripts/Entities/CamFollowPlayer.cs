using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothness;
    private Vector3 targetPos = new Vector3(-1, 3.5f, 4);

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (GameManager.Instance.state != GameManager.State.OnPlay)
            return;
        FollowPlayer();
        
    }

    [System.Obsolete]
    private void FollowPlayer()
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
        targetPos.x = Mathf.Max(player.transform.position.x, targetPos.x);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothness);
    }

    [System.Obsolete]
    public void checkGameOver()
    {
        if (targetPos.x - 3.5 > player.transform.position.x)
            GameManager.Instance.setGameOver(true);
    }

    public void Clear()
    {
        targetPos = new Vector3(-1, 3.5f, 4);
        this.transform.position = targetPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothness; 

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 targetPos = new Vector3( Mathf.Max(player.transform.position.x, this.transform.position.x), 1, player.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos + offset, smoothness);
    }
}

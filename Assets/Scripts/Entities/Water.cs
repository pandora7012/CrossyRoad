using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public List<Log> LogPolling = new List<Log>();
    [SerializeField] private GameObject LogPrefab;
    [SerializeField] private Vector3 pos;
    [SerializeField] private bool isRight;
    void Awake()
    {
        // add empty array to map
        bool[] vp = new bool[9];
        GameManager.Instance.map.Add(vp);

        //log polling 
        float rand = Random.Range(0, 100);
        isRight = rand > 50 ? true : false;
        pos = this.transform.position;
        pos.y = 0;
        pos.z = isRight ? -5 : 15;

        float speed = 2;
        // instance log 
        for (int i = 0; i < 5; i++)
        {
            Log lg = Instantiate(LogPrefab, pos, Quaternion.identity).GetComponent<Log>();
            lg.isRight = isRight;
            lg.pos = pos;
            lg.transform.parent = this.transform;
            lg.gameObject.SetActive(false);
            LogPolling.Add(lg);
            lg.speed = speed;

        }
        StartCoroutine("setLogActive");
    }


    private IEnumerator setLogActive()
    {
        foreach (Log i in LogPolling)
        {
            i.gameObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(3,4f));
        }
    }


}

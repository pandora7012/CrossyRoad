using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private List<GameObject> pref;

    private Vector3 thisPos;

    private bool[] vs = new bool[9];

    private void Awake()
    {
        thisPos = transform.position;

        //init things out playground 
        GameObject obticle = Instantiate(pref[Random.Range(0, 3)], new Vector3(thisPos.x, 0.5f, -1), Quaternion.identity);
        obticle.transform.parent = this.transform;
        obticle = Instantiate(pref[Random.Range(0, 3)], new Vector3(thisPos.x, 0.5f, 9), Quaternion.identity);
        obticle.transform.parent = this.transform;

        // init things on playground
        for (int i = 0; i <= 8; i++)
        {
            float rd = Random.Range(0, 100);
            if (rd > 20 || this.transform.position.x <= 0)
            {
                vs[i] = false;
                continue;
            }
            vs[i] = (true); 
            obticle = Instantiate(pref[Random.Range(0, 3)], new Vector3(thisPos.x, 0.5f, i), Quaternion.identity);
            obticle.transform.parent = this.transform;
        }
        // add texture out bound 
        for (int i = -6; i <= -2; i++)
        {
            float rd = Random.Range(0, 100);
            if (rd > 50)
                continue;
            obticle = Instantiate(pref[Random.Range(0, 3)], new Vector3(thisPos.x, 0.5f, i), Quaternion.identity);
            obticle.transform.parent = this.transform;
        }
        for (int i = 10; i <= 15; i++)
        {
            float rd = Random.Range(0, 100);
            if (rd > 50)
                continue;
            obticle = Instantiate(pref[Random.Range(0, 3)], new Vector3(thisPos.x, 0.5f, i), Quaternion.identity);
            obticle.transform.parent = this.transform;
        }

        GameManager.Instance.map.Add(vs);
    }


}

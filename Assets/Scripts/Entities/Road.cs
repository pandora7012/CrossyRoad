using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    [SerializeField] private GameObject[] cars;
    [SerializeField] private bool isRight;
    [SerializeField] private Vector3 pos;
    [SerializeField] private float speed;
    [SerializeField] private int value;

    private List<Car> carPolling = new List<Car>();

    void Start()
    {
        
        // add empty array to map
        bool[] vp = new bool[9];
        GameManager.Instance.map.Add(vp);

        // car polling 
        float rand = Random.Range(0, 100);
        isRight = rand > 50 ? true : false;
        pos = this.transform.position;
        pos.y = 0.45f;
        pos.z = isRight ? -5 : 15;
        speed = Random.Range(2, 4);

        for (int i = 0; i < value / speed; i++)
        {
            
            Car car = Instantiate(cars[(int)Random.Range(0, 4)], pos, Quaternion.identity).GetComponent<Car>();
            car.transform.parent = this.transform;
            car.isRight = isRight;
            car.speed = speed;
            car.gameObject.SetActive(false);
            car.pos = pos;
            carPolling.Add(car);
        }

        StartCoroutine("setCarActive");

    }
    private IEnumerator setCarActive()
    {
        foreach (Car i in carPolling)
        {
            i.gameObject.SetActive(true);
            int p = Random.Range(-0, 2);
            i.delta = p;
            yield return new WaitForSeconds(10/speed +  p);
        }
    }

    


    


}

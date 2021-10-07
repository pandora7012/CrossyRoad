using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewItem : MonoBehaviour
{
    [SerializeField] private GameObject[] item;

    public int point; 

    private void OnEnable()
    {
        transform.DOLocalMoveX(0, 1f).From(-1960);
        int top = PlayerPrefs.GetInt("Top");
        if (point < 30 && top >= 30)
        {
            item[0].gameObject.SetActive(true);
            item[1].gameObject.SetActive(true); 
        }
        if (point < 60 && top >= 60)
        {
            item[2].gameObject.SetActive(true);
            item[3].gameObject.SetActive(true);
        }
        if (point < 100 && top >= 100)
        {
            item[4].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject i in item)
            i.SetActive(false);
    }
}

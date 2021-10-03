using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public List<bool[]> map = new List<bool[]>();

    private void Start()
    {
        
    }

    public void RemoveMap()
    {
        map.RemoveAt(0);
    }
}

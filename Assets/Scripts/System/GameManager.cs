using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public List<bool[]> map = new List<bool[]>();

    public bool GameOver;

    private void Start()
    {
        GameOver = false; 
    }

    public void RemoveMap()
    {
        map.RemoveAt(0);
    }

    public void setGameOver(bool value)
    {
        GameOver = value;
        GameoverDo();
    }

    public void GameoverDo()
    {
        Debug.Log("GameOver");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public List<bool[]> map = new List<bool[]>();
    [SerializeField] private Player player;
    [SerializeField] private TerrainGenerator terrain;
    [SerializeField] private CamFollowPlayer cam; 

    public enum State{
        MainMenu, 
        Shop,
        OnPlay
    }

    public State state;

    public bool GameOver;

    private void Start()
    {
        GameOver = false;
        InitGame();
        state = State.MainMenu; 
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
        PlayerPrefs.SetInt("Top", Mathf.Max(PlayerPrefs.GetInt("Top"), UIManager.Instance.score));
        UIManager.Instance.GameOver();
    }

    public void InitGame()
    {
        player.gameObject.SetActive(true);
        state = State.MainMenu;
        map.Clear();
        terrain.Clear();
        cam.Clear();
        player.Clear();
        GameOver = false;
    }


}

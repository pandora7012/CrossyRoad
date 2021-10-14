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

    [Header("VFX")]
    [SerializeField] private ParticleSystem deadPar;
    [SerializeField] private ParticleSystem waterPar;
    [SerializeField] private AudioListener listener;
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
        UIManager.Instance.LoadingView();
        InitGame();
        state = State.MainMenu; 
    }

    public void RemoveMap()
    {
        map.RemoveAt(0);
    }

    [System.Obsolete]
    public void setGameOver(bool value)
    {
        GameOver = value;
        GameoverDo();
        
    }

    [System.Obsolete]
    public void GameoverDo()
    {
        
        GFX();
        player.transform.parent = null;
        UIManager.Instance.GameOver();
        listener.enabled = true;
    }


    public void GFX()
    {
        deadPar.transform.position = player.transform.position + new Vector3(0, 1, 0);
        deadPar.gameObject.SetActive(true);
        waterPar.transform.position = player.transform.position;
        waterPar.gameObject.SetActive(true);
        if (player.transform.position.y < 00.75f)
        {
            waterPar.Play();
        }
        else
            deadPar.Play();
    }
    public void InitGame()
    {
        deadPar.gameObject.SetActive(false);
        waterPar.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        state = State.MainMenu;
        map.Clear();
        terrain.Clear();
        cam.Clear();
        player.Clear();
        GameOver = false;
        listener.enabled = false;
    }


}

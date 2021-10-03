using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [HideInInspector] public  Vector3 genePos = new Vector3(-4,0,0);
    public List<GameObject> terrainPrefabs;
    [HideInInspector] public List<GameObject> terrains = new List<GameObject>() ;
    [SerializeField] private Player player; 

    public int maxTerrain;

    public void Start()
    {
        init();
        Observer.Forward += Generate; 
    }

    private void init()
    {

        // init first start game
        for (int i = 0; i < 7; i++)
        {
            GameObject terrain = Instantiate(terrainPrefabs[1], genePos, Quaternion.identity);
            terrains.Add(terrain);
            genePos += new Vector3(1, 0, 0);
            terrain.gameObject.transform.SetParent(this.transform);
        }
        for (int i = 0; i < 23; i++)
            Generate();
    }

    public void Generate()
    {
        // add more terrain 
        if (player.isJumping)
            return;
        GameObject terrain =  Instantiate(terrainPrefabs[(int)Random.Range(0, 3)], genePos , Quaternion.identity);
        terrains.Add(terrain);
        genePos += new Vector3(1, 0, 0);
        
        terrain.gameObject.transform.SetParent(this.transform);

        //destroy terrain out of bound. 
        if (terrains.Count > maxTerrain)
        {
            Destroy(terrains[0].gameObject);
            terrains.RemoveAt(0);
        }
    }

    private void OnDestroy()
    {
        Observer.Forward -= Generate;
    }
}

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
        Observer.Forward += Generate; 
    }

    private void init()
    {
        
        for (int i = 0; i < 7; i++)
        {
            
            GameObject terrain = Instantiate(terrainPrefabs[1], genePos, Quaternion.identity);
            terrains.Add(terrain);
            genePos += new Vector3(1, 0, 0);
            terrain.gameObject.transform.SetParent(this.transform);
        }
        for (int i = 0; i < 15; i++)
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

    public void Clear()
    {
        genePos = new Vector3(-6, 0, 0);
        foreach (GameObject i in terrains)
            Destroy(i); 
        terrains.Clear();
        init();
    }

    private void OnDestroy()
    {
        Observer.Forward -= Generate;
    }
}

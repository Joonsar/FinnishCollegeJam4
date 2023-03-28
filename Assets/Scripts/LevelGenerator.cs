using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.UI;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public string name;
    public GameObject tile;
    public int weight;
}

public class LevelGenerator : MonoBehaviour
{
    public List<Tile> tiles;
    public GameObject wall;
    public int seed;
    public int size = 10;

    public List<GameObject> tileSpawnList = new List<GameObject>();
    GameObject[,] map;


    // Start is called before the first frame update
    void Start()
    {
        //Jos seediä ei anneta, niin valitaan random seed
        if(seed == 0) {
            seed = Random.Range(0, 9999);
        }
        print(seed);
        Random.InitState(seed);
        //Rakennetaan taso
        BuildLevel();
    }

    //Rakentaa tason
    void BuildLevel()
    {
        //Luodaan spawnlist tileille        
        foreach(Tile t in tiles)
        {
            for(int i = 0; i < t.weight; i++)
            {
                tileSpawnList.Add(t.tile);
            }           
        }

        //Luodaan array mapille
        map = new GameObject[size + 1, size + 1];
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                //Tarkistetaan tile
                if (map[x,z] == null)
                {
                    SpawnTile(x, z);
                }
            }
        }
    }

    void SpawnTile(int x, int z)
    {
        //Arvotaan tilen index listasta
        int tileID = (int)(Random.value * tileSpawnList.Count);
        //Tarkistetaan tilen viereiset tilet päällekkäisyyksiltä
        if (tileSpawnList[tileID].GetComponent<TileManager>().size.x > 1 && x < size)
        {
            if(map[x + 1, z] != null)
            {
                tileID = 0;
            }
        }
        if (tileSpawnList[tileID].GetComponent<TileManager>().size.y > 1 && z < size)
        {
            if (map[x, z + 1] != null)
            {
                tileID = 0;
            }           
        }
        if(tileSpawnList[tileID].GetComponent<TileManager>().size == Vector2.one * 2 && z < size && x < size)
        {
            if (map[x + 1, z + 1] != null)
            {
                tileID = 0;
            }
        }

        //Luodaan tile       
        map[x, z] = Instantiate(tileSpawnList[tileID], new Vector3(x * 5, 0, z * 5) * 6, Quaternion.Euler(Vector3.zero));
        TileManager tm = map[x, z].GetComponent<TileManager>();
        map[x, z].name = "TILE:" + x + "/" + z;

        //Tarkistetaan tilen koko
        if (tm.size.x > 1)
        {
            map[x + 1, z] = map[x, z];
        }
        if (tm.size.y > 1)
        {
            map[x, z + 1] = map[x, z];
        }
        if(tm.size.x > 1 && tm.size.y > 1)
        {
            map[x + 1, z + 1] = map[x,z];
        }
    }
}

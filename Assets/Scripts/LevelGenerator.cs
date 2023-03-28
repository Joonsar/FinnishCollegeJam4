using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class Tile   //Tile mahdollistaa weightin k�yt�n helposti, jonka avulla m��ritell��n kuinka todenn�k�isesti tile valitaan spawn listist�
{
    public string name;
    public GameObject tile;
    public int weight;
}

public class LevelGenerator : MonoBehaviour
{
    public List<Tile> tiles;    //Lista tileist�, joiden class on m��ritelty t�m�n scriptin alussa
    public int seed;
    public int mapSize = 4;

    public List<GameObject> tileSpawnList = new List<GameObject>();
    GameObject[,] map;


    // Start is called before the first frame update
    void Start()
    {
        //Jos seedi� ei anneta, niin valitaan random seed
        if (seed == 0)
        {
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
        //Luodaan spawnlist tileille niille annetun painon perusteella    
        foreach (Tile t in tiles)
        {
            for (int i = 0; i < t.weight; i++)
            {
                tileSpawnList.Add(t.tile);
            }
        }

        //Luodaan array mapille
        map = new GameObject[mapSize + 1, mapSize + 1];
        for (int z = 0; z < mapSize; z++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                //Tarkistetaan tile
                if (map[x, z] == null)
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
        //Tarkistetaan tilen viereiset tilet p��llekk�isyyksilt�
        if (tileSpawnList[tileID].GetComponent<TileManager>().size.x > 1)
        {
            if (map[x + 1, z] != null || x >= mapSize - 1)
            {
                tileID = 0;
            }
        }
        if (tileSpawnList[tileID].GetComponent<TileManager>().size.y > 1 && z <= mapSize - 1)
        {
            print("Z");
            if (map[x, z + 1] != null || z >= mapSize - 1)
            {
                tileID = 0;
            }
        }
        if (tileSpawnList[tileID].GetComponent<TileManager>().size == Vector2.one * 2 && z <= mapSize - 2 && x <= mapSize - 1)
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
        if (tm.size.x > 1 && tm.size.y > 1)
        {
            map[x + 1, z + 1] = map[x, z];
        }
    }
}

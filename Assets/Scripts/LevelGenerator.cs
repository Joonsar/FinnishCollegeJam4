using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> buildings;
    public List<GameObject> tiles;
    public GameObject wall;
    public int seed;
    public int size = 10;


    // Start is called before the first frame update
    void Start()
    {
        //Jos seediä ei anneta, niin valitaan random seed
        if(seed == 0) {
            seed = Random.Range(0, 9999);
        }
        Random.InitState(seed);
        //Rakennetaan taso
        BuildLevel();
    }

    //Rakentaa tason
    void BuildLevel()
    {
        GameObject[,] map = new GameObject[size, size];
        for(int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {               
                //Luodaan tile
                int tileID = (int)(Random.value * tiles.Count); //Tiilen id listassa
                map[x, z] = Instantiate(tiles[tileID], new Vector3(x * 5, 0, z * 5) * 5, Quaternion.Euler(Vector3.zero));
                //Luodaan rakennus
                Quaternion rotation = Quaternion.Euler(Vector3.up * 90 * (int)(Random.value * 4));  //Rakennuksen rotaatio
                int buildingID = (int)(Random.value * buildings.Count);
                GameObject building = Instantiate(buildings[buildingID], map[x,z].transform.position + map[x, z].GetComponent<TileManager>().plots[0], rotation);
                building.transform.parent = map[x, z].transform;
                //Tarkistetaan sijaitseeko tile mapin reunassa
                if (x <= 0 || x >= size || z <= 0 || z >= size)
                {

                }
            }
        }
    }
}

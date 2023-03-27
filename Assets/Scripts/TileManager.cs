using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Vector3> plots;

    [Header("Walls")]
    public bool walled;
    public GameObject wall;
    public Vector3 wallDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    void BuildWall()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject spawned_wall = Instantiate(wall, transform.position + wallDir * i, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Vector3 plot in plots)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + plot, 1f);
        }
    }
}

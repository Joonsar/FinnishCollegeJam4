using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPainter : MonoBehaviour
{
    public int wallIndex = 0;
    public List<Material> wallMaterials;
    public int detailIndex = 1;
    public List<Material> detailMaterials;
    public int windowIndex = 2;
    public Material windows;
    public int doorIndex = 3;
    public Material doors;

    void Start()
    {
        PaintHouse();
    }

    void PaintHouse()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material[] mats = new Material[4];
        mats[wallIndex] = wallMaterials[(int)(Random.value * wallMaterials.Count)];
        mats[detailIndex] = detailMaterials[(int)(Random.value * detailMaterials.Count)];
        mats[windowIndex] = windows;
        mats[doorIndex] = doors;
        mr.materials = mats;
    }
}

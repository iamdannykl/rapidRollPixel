using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bj : MonoBehaviour
{
    // Start is called before the first frame update
    public TilemapRenderer tr;
    public Vector2 spd;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tr.material.mainTextureOffset += (spd * Time.deltaTime);
    }
}

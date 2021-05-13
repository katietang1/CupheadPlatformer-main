using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    Material material;
    Vector2 offset;

    public float xVelocity, yVelocity;


    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        
    }
    void Update()
    {
        offset = new Vector2(xVelocity, yVelocity);
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}

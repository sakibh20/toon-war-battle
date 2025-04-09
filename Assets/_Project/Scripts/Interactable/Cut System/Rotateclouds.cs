using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotateclouds : MonoBehaviour
{
    public Vector3 rotatedirection;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotatedirection * Time.deltaTime);
    }
}

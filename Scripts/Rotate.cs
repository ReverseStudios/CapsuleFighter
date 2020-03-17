using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateY;

    void Update()
    {
        transform.Rotate(new Vector3(0f, rotateY , 0f)); 
    }
}

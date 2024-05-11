using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Per_Coil : MonoBehaviour
{
    public Sprite OpenCoil;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void toOpenShape()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OpenCoil;
    }
}

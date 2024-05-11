using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Per_Poling : MonoBehaviour
{
    internal void toOpenShape()
    {
        gameObject.GetComponent<Animator>().SetTrigger("jump");
    }

}

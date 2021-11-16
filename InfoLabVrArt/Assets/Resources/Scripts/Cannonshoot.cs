using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonshoot : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y<-2){
            Destroy(this);
        }
    }
}

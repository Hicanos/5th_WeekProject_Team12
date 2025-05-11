using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyAbleIObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        { return; }
        else { DestroySelf(); }
    }

    public void DestroySelf()
    {
       
         Destroy(gameObject);
    }
}

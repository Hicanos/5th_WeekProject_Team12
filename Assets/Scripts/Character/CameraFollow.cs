using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [Header("카메라가 따라갈 두 캐릭터")]
    [SerializeField] private GameObject [] player;
    [SerializeField]float minY = 0f;
    [SerializeField] float maxY = 11f;
   
    Camera camera;
   
    void Awake()
    {
        camera = Camera.main;
       
        
    }
    private void Start()
    {
         player = GameObject.FindGameObjectsWithTag("Player");
    }
    private void FixedUpdate()
    {
        MoveCamera();
    }



    private void MoveCamera()
    { 
        Vector3 midpoint = (player[0].transform.position + player[1].transform.position)/ 2f;
        camera.transform.position = new Vector3(0f, midpoint.y, -10f);

        if (camera.transform.position.y<minY)
        { camera.transform.position =new Vector3(0,minY,-10); }
        if (camera.transform.position.y > maxY)
        { camera.transform.position = new Vector3(0, maxY, -10); }



    }

    


}


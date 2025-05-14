using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [Header("카메라가 따라갈 두 캐릭터")]
    [SerializeField] private GameObject [] player;
    [SerializeField]float minY = 0f;
    [SerializeField] float maxY = 10f;
    
    


    /*[Header("카메라 따라가기 속도")]
    [SerializeField] private float followSpeed = 5f;*/
    Camera camera;
    // Start is called before the first frame update
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

        



    }

    


}


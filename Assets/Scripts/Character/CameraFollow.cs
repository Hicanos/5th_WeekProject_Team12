using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [Header("ī�޶� ���� �� ĳ����")]
    [SerializeField] private Transform Cat;
    [SerializeField] private Transform Dog;
    Rigidbody2D rb;
    Transform tCam;
    Transform tCat;
    Transform tDog;

    /*[Header("ī�޶� ���󰡱� �ӵ�")]
    [SerializeField] private float followSpeed = 5f;*/
    Camera camera;
    // Start is called before the first frame update
    void Awake()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        tCam = GetComponent<Transform>();
        
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }



    private void MoveCamera()
    { 
        Vector3 midpoint = (tCat.position + tDog.position)/ 2f;
        rb.position = midpoint;
        



    }

    


}


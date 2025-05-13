using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scaffolding : MonoBehaviour
{
    public Rigidbody2D rbscaffolding;
    public Transform tf;
    void Start()
    {
        rbscaffolding = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        ScaffoldingMove();
    }



    public bool scaffoldingOn = true;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float maxY = 3f;
    [SerializeField] public float minY = 0f;
    [SerializeField] private bool scaffoldingMovingUp = true;

    public void ScaffoldingMove()
    {
        Vector2 updown = rbscaffolding.velocity;
      
        if (scaffoldingOn)
        {

            if (scaffoldingMovingUp)
            {
                updown.y = moveSpeed;
                if (tf.localPosition.y >= maxY)
                { scaffoldingMovingUp = false; }


            }
            else
            {

                updown.y = -moveSpeed;
                if (tf.localPosition.y <= minY)
                { scaffoldingMovingUp = true; }
            }
        }
        else { updown.y = 0; }



        rbscaffolding.velocity = updown;


    }


}


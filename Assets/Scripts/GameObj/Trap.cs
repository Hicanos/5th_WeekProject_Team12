using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("비교할 태그")] public string compareTag;
    public GameObject Dog;
    public GameObject Cat;
    private Vector3 initialDogPosition; //초기 개의 위치
    private Vector3 initialCatPosition; //초기 고양이의 위치

    private void Start()
    {
        initialDogPosition = Dog.transform.position;
        initialCatPosition = Cat.transform.position;        
    }


    public string GetTargetTagName()
    {
        return compareTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetTargetTagName();
        if (collision.CompareTag(compareTag))
        {
            //오브젝트의 TargetName과 부딪힌 Player의 태그비교
            //개가 부딪혔다면 개, 고양이가 부딪혔다면 고양이가 초기위치로 감
            if(compareTag == "Dog")
            {
                Dog.transform.position = initialDogPosition;
            }
            else if(compareTag == "Cat")
            {
                Cat.transform.position = initialCatPosition;
            }
        }
    }

}

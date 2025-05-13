using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("상호작용 오브젝트")] public GameObject gameObject;
    //게임 오브젝트에 Collider 적용, Collision = 상자를 인식해서 버튼이 눌리면 적용됨
    //상자의 Tag => Box로 변경
    //SetActive를 false면 true, true면 false로 만들어서 적용

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

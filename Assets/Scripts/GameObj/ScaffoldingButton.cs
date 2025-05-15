using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldingButton : MonoBehaviour
{
   
    
    /*[Header("상호작용 오브젝트")] public GameObject gameObj;*/
    
    //게임 오브젝트에 Collider 적용, Trigger = 상자를 인식해서 버튼이 눌리면 적용됨
    //고양이의 경우: Type Wall일 때 왼쪽 쉬프트로 상호작용 가능 (고양이가 stay중에 왼쪽 쉬프트를 누르면, 오브젝트 작동)
    //상자의 Tag => Box로 변경
    //SetActive를 false면 true, true면 false로 만들어서 적용
    //상자가 범위를 벗어나면 다시 역으로 바꿈

    // Start is called before the first frame update

 MyAnimationController anim;
   public Scaffolding scaffolding;

    void Start()
    {
        anim = GetComponentInChildren<MyAnimationController>();
        
    }
    bool switchON = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
                    switchON = true;
                    scaffolding.scaffoldingOn = true;
            anim.SetSwitch(switchON);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switchON = false;
            scaffolding.scaffoldingOn = false;
            anim.SetSwitch(switchON);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldingButton : MonoBehaviour
{
   
    
    /*[Header("��ȣ�ۿ� ������Ʈ")] public GameObject gameObj;*/
    
    //���� ������Ʈ�� Collider ����, Trigger = ���ڸ� �ν��ؼ� ��ư�� ������ �����
    //������� ���: Type Wall�� �� ���� ����Ʈ�� ��ȣ�ۿ� ���� (����̰� stay�߿� ���� ����Ʈ�� ������, ������Ʈ �۵�)
    //������ Tag => Box�� ����
    //SetActive�� false�� true, true�� false�� ���� ����
    //���ڰ� ������ ����� �ٽ� ������ �ٲ�

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

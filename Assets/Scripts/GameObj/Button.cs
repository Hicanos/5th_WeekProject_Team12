using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("��ȣ�ۿ� ������Ʈ")] public GameObject gameObj;
    //���� ������Ʈ�� Collider ����, Trigger = ���ڸ� �ν��ؼ� ��ư�� ������ �����
    //������� ���: Type Wall�� �� ���� ����Ʈ�� ��ȣ�ۿ� ���� (����̰� stay�߿� ���� ����Ʈ�� ������, ������Ʈ �۵�)
    //������ Tag => Box�� ����
    //SetActive�� false�� true, true�� false�� ���� ����
    //���ڰ� ������ ����� �ٽ� ������ �ٲ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

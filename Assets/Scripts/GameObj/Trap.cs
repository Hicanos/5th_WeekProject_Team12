using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("���� �±�")] public string compareTag;
    public GameObject Dog;
    public GameObject Cat;
    private Vector3 initialDogPosition; //�ʱ� ���� ��ġ
    private Vector3 initialCatPosition; //�ʱ� ������� ��ġ

    private void Start()
    {
        //�� �÷��̾��� ������ �ʱⰪ ����
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
            //������Ʈ�� TargetName�� �ε��� Player�� �±׺�
            //���� �ε����ٸ� ��, ����̰� �ε����ٸ� ����̰� �ʱ���ġ�� ��
            //�̸� Ȱ���ϸ�, ���� �߶��ؼ� ����������ų� ����ġ ��ų �� ���� ������Ʈ�� ���� �ڸ��� �ű� �� ����
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

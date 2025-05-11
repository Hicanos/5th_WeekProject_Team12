using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterDog : Characterbase
{

    protected override void Awake()
    {
        enumChar = CHAR.DOG;//ĳ���� �з�
        base.Awake();
        ControlKey();//����Ű�Ҵ�
    }
    /// <summary>
    /// �Է¸ż���� ���⿡
    /// </summary>
    private void Update()
    {
        MoveCall();         // �̵� �Է� 
        SkillCall(); //��ų Ű �Է�
        JumpCall();   // ���� �Է� 
    }
    /// <summary>
    /// �ൿ�ż���� ���⿡
    /// </summary>
    private void FixedUpdate()
    {
        JumpAtivate();//���� ����
        HandleJumpAnim();//���� �ִϸ��̼�
        CheckLanding();//���� ����
        
        MoveActivate(moveInput); // �̵� ȣ�� 
        SpriteFlip();    // ĳ���� �¿� ����
        HandleMoveAnim();     // �̵� �ִϸ��̼� 
        InstantSkillActivate(); //��ų�ߵ�
    }

    protected override void SkillCall()
    {
        if (IsGrounded() &&  currentJumpCount == 0) //���������϶��� ��ų ��� ���� �ϰ� �ϱ�
            base.SkillCall();
    }
    protected override void Skill()
    {

        Vector2 currentLook; //������ ������� ���Ͱ�
        if (this.spriteRenderer.flipX == false)
        {
            currentLook = new Vector2(1, 0);
        }
        else 
        { 
            currentLook = new Vector2(-1, 0); 
        }
        rb.velocity = Vector2.zero; //  �ӵ� �ʱ�ȭ
        rb.AddForce(currentLook * 50f, ForceMode2D.Impulse);

        //��ų ������ �־� �¿� ���� Update���� �������̴� ���� �ʿ�
        //����ó�� �ܼ�ȭ�� ���ؼ� "���¸ӽ�" ����ϱ� 
    }
}




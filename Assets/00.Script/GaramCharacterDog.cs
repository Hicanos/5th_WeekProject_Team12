using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterDog : Characterbase
{

    protected override void Awake()
    {
        enumChar = CHAR.DOG;
        base.Awake();
        ControlKey();
    }
    /// <summary>
    /// �Է¸ż���� ���⿡
    /// </summary>
    private void Update()
    {
        HandleInput();         // �̵� �Է� 
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
        
        Move(moveInput); // �̵� ȣ�� 
        HeadSpriteFlip();    // ĳ���� �¿� ����
        HandleMoveAnim();     // �̵� �ִϸ��̼� 
        SkillActivate(); //��ų�ߵ�
    }

    protected override void SkillCall()
    {
        if (IsGrounded() && IsGrounded() && currentJumpCount == 0)
            base.SkillCall();
    }
    protected override void Skill()
    {

        Vector2 currentLook; /*= new Vector2(moveX, 0).normalized;*/
        if (this.spriteRenderer.flipX == false)
        {
            currentLook = new Vector2(1, 0);
        }
        else { currentLook = new Vector2(-1, 0); }
        rb.velocity = Vector2.zero; //  �ӵ� �ʱ�ȭ
        rb.AddForce(currentLook * 50f, ForceMode2D.Impulse);

        //��ų ������ �־� �¿� ���� Update���� �������̴� ���� �ʿ�
        //����ó�� �ܼ�ȭ�� ���ؼ� "���¸ӽ�" ����ϱ� 
    }
}




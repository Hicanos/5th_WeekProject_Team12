using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterCat : Characterbase
{
    protected override void Awake()
    {
        enumChar = CHAR.CAT; // ĳ���� �з��ϱ�
        base.Awake();
        ControlKey();//�з��� ���� ����Ű �Ҵ�
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
        if (Input.GetKey(SkillKey) && skillReady&&IsWallClimb())
        {
            StartCoroutine(SkillCoolDown(SkillCoolTime));
            SkillReq = true;
            Debug.Log("��ų Ű �Է�");
        }
    }
    protected override void Skill()
    {
        if (SkillReq)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f); //���� �ٵ��� �����ֱ� ���� z�� ȸ��
            rb.gravityScale = 0f; //�߷� 0���� ����� 
            isClimb = true;

        }
        else 
        {
            transform.rotation = Quaternion.identity; // ���� �ʱⰪ (0,0,0)
            rb.gravityScale = 1f;
            isClimb = false;
        }
       
    }
   
    protected override void MoveCall()//���� �پ��� �ÿ� ���� ���� ������ ���� ������
    {
        if (!isClimb)
        {
            moveX = 0;

            if (Input.GetKey(leftKey)) moveX -= 1;
            if (Input.GetKey(rightKey)) moveX += 1;
            moveInput = new Vector2(moveX, moveY).normalized;
        }
        else
        {
            moveY = 0;

            if (Input.GetKey(leftKey)) moveY -= 1;
            if (Input.GetKey(rightKey)) moveY += 1;
            moveInput = new Vector2(moveX, moveY).normalized;
        }
        }
    /// <summary> �̵� ó��: �ӵ� ���� �� ���� ���� </summary>
    protected override void MoveActivate(Vector2 input)//���� �پ��� �ÿ� ���� ���� ������ ���� ������
    {
        if (!isClimb)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = input.x * moveSpeed;

            rb.velocity = velocity;
        }
        else
        {
            Vector2 velocity = rb.velocity;
            velocity.y = input.y * moveSpeed;

            rb.velocity = velocity;
        }



    }


}
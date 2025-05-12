using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;
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
        if (isDash)
        {
            return;
        }
        else
        {
            InstantSkillCall(); //��ų Ű �Է�
            JumpCall();   // ���� �Է� 
            MoveCall();   // �̵� �Է� 
        }


    }
    /// <summary>
    /// �ൿ�ż���� ���⿡
    /// </summary>
    private void FixedUpdate()
    {

        HandleJumpAnim();//���� �ִϸ��̼�
        CheckLanding();//���� ����
        if (isDash)
        {
            return;
        }
        else
        {
            JumpAtivate();//���� ����
            MoveActivate(moveInput); // �̵� ȣ�� 
        }


        SpriteFlip();    // ĳ���� �¿� ����
        HandleMoveAnim();     // �̵� �ִϸ��̼� 
        InstantSkillActivate(); //��ų�ߵ�
        HandleSkillAnim();//��ų �ִϸ��̼�
    }

    protected override void InstantSkillCall()
    {
        if (IsGrounded() && currentJumpCount == 0) //���������϶��� ��ų ��� ���� �ϰ� �ϱ�
            base.InstantSkillCall();

    }
    protected override void InstantSkill()
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
        rb.AddForce(currentLook * 10f, ForceMode2D.Impulse);
        StartCoroutine(DashDuration(currentLook,0.2f,30f));

    }
    private IEnumerator DashDuration(Vector2 direction, float duration, float dashSpeed)
    {
        isDash = true;
        
        yield return new WaitForSeconds(0.1f);
        rb.velocity = direction * dashSpeed;  // ���������� ���� �ӵ� ����
       

        yield return new WaitForSeconds(duration); // �� duration��ŭ�� �̵�

        rb.velocity = Vector2.zero;  // ��� ���߱�
        isDash = false;
    }
    //��ų ������ �־� �¿� ���� Update���� �������̴� ���� �ʿ�
    //����ó�� �ܼ�ȭ�� ���ؼ� "���¸ӽ�" ����ϱ� 
    private bool isDash = false; // ������ ��ų�ߵ����� ������ �Ұ�
    protected override void MoveCall() //�̵�Ű �Է� ���� 
    {
        /*moveX = Input.GetAxisRaw("Horizontal");*///���� �ٸ� �� ĳ���͸� �����ؾ� �ؼ� �Է� ����� �ٲ� 

       
            moveX = 0;

            if (Input.GetKey(leftKey)) moveX -= 1;
            if (Input.GetKey(rightKey)) moveX += 1;
            moveInput = new Vector2(moveX, moveY).normalized;
        
        //�Է°��� ���� ���� ���ϱ�
    }

    protected override void HandleSkillAnim()
    {
        bool isSkill = isDash;
        Anim.SetSkill(isSkill);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ���� �ƴ� ���� ����
        if (!isDash) return;

        // ���̾� üũ: ������ ���̾ �ش��ϴ��� Ȯ��
        if (((1 << other.gameObject.layer) & DestroyLayer) != 0)
        {
            Destroy(other.gameObject);
            HandleCrashAnim();
            isDash = false;                     
            rb.velocity = Vector2.zero;
            
            Debug.Log(" �������� �ı���: " + other.name);
        }
    }

}





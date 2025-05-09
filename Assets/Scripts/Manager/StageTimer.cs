using UnityEngine;                         
using UnityEngine.SceneManagement;         
using UnityEngine.UI;                      

public class SceneTimer : MonoBehaviour
{
    public Text timerText;                 
    public float timeInScene = 300f;         // ���� ������ ����� �ð� (�� ����)
    public bool isTiming = false;          // �ð� ���� Ȱ��ȭ ���θ� ��Ÿ���� �÷���

    
    
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;     // ���� �ε�� �� ȣ���� �޼��� ���
        SceneManager.sceneUnloaded += OnSceneUnloaded; // ���� ��ε�� �� ȣ���� �޼��� ���
    }

   
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;     // �� �ε� �̺�Ʈ ���� ����
        SceneManager.sceneUnloaded -= OnSceneUnloaded; // �� ��ε� �̺�Ʈ ���� ����
    }

    // �� �����Ӹ��� ȣ��Ǵ� �޼���
    public void Update()
    {
        if (isTiming) // �ð� ������ Ȱ��ȭ�� ���
        {
            timeInScene -= Time.deltaTime;

            if (timerText != null) // �ؽ�Ʈ UI�� ����Ǿ� �ִٸ�
            {
                timerText.text = $"{timeInScene:F1}";
            }
        }
    }

    // ���� �ε�� �� �ڵ����� ȣ��Ǵ� �޼���
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timeInScene = 300f;  // �� ���� �� �ð� �ʱ�ȭ
        isTiming = true;   // �ð� ���� ����
    }

    // ���� ��ε�� �� �ڵ����� ȣ��Ǵ� �޼���
    public void OnSceneUnloaded(Scene current)
    {
        isTiming = false;  // �ð� ���� ����
        timeInScene = 0f;  // �ð� �ʱ�ȭ
    }
}

using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioManager audioManager;

    void Start()
    {
        // AudioManager�� �ν��Ͻ��� �����ͼ� audioManager ������ ����
        audioManager = AudioManager.Instance;
        // �����̴��� �ʱ� ���� ���� ����� �������� ���� (��: 0.8�̸� �����̴��� 0.8�� ��ġ)
        volumeSlider.value = audioManager.CurrentVolume;
        // �����̴� ���� ����� ������ OnVolumeChanged �Լ��� ȣ��ǵ��� �̺�Ʈ ����
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        audioManager.SetVolume(value);
    }
}

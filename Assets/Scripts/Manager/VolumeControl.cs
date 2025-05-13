using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioManager audioManager;

    void Start()
    {
        // AudioManager의 인스턴스를 가져와서 audioManager 변수에 저장
        audioManager = AudioManager.Instance;
        // 슬라이더의 초기 값을 현재 오디오 볼륨으로 설정 (예: 0.8이면 슬라이더도 0.8에 위치)
        volumeSlider.value = audioManager.CurrentVolume;
        // 슬라이더 값이 변경될 때마다 OnVolumeChanged 함수가 호출되도록 이벤트 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        audioManager.SetVolume(value);
    }
}

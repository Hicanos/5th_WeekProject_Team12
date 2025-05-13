using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneManager : MonoBehaviour
{
    public static VideoSceneManager Instance;

    // 유니티의 VideoPlayer 컴포넌트
    public VideoPlayer videoPlayer;

    // 비디오 재생이 끝난 후 전환할 다음 씬 이름
    public string nextSceneName;

    private void Awake()
    {
        // 인스턴스가 없으면 자기 자신을 설정
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null; // 부모에서 분리해서 루트로 만들기
            DontDestroyOnLoad(gameObject); // 씬 전환 시 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 자기 자신 제거 (중복 방지)
            return;
        }
    }

    // 비디오를 재생하고 끝나면 다음 씬으로 전환
    public void PlayVideoAndLoadScene(VideoClip clip, string nextScene)
    {
        // 다음 씬 이름 저장 (씬 전환 시 사용)
        nextSceneName = nextScene;

        // VideoPlayer 컴포넌트가 비어 있으면 새로 붙임
        if (videoPlayer == null)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }

        // 비디오 클립 지정
        videoPlayer.clip = clip;

        // 게임 오브젝트가 활성화될 때 자동 재생
        videoPlayer.playOnAwake = false;

        // 영상은 한 번만 재생
        videoPlayer.isLooping = false;

        // 영상 출력 설정
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCamera = Camera.main; // 메인 카메라에 출력

        // 오디오를 연결
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());

        // 영상 재생이 끝났을 때 실행할 메서드 등록
        videoPlayer.loopPointReached += OnVideoEnd;

        // 비디오 재생 시작
        videoPlayer.Play();
    }

    // 비디오 재생이 끝나면 호출되는 메서드
    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("비디오 재생 완료, 씬 전환");
        // 지정된 씬으로 전환
        SceneManager.LoadScene(nextSceneName);
    }

}

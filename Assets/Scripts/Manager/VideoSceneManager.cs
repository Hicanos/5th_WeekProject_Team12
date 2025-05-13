using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneManager : MonoBehaviour
{
    public static VideoSceneManager Instance;

    // 싱글톤 인스턴스
    public RawImage rawImage;                // 영상이 출력될 UI RawImage
    public VideoPlayer videoPlayer;          // VideoPlayer 컴포넌트

    public string nextSceneName;             // 영상 종료 후 이동할 씬 이름
    public VideoClip videoClip;              // 재생할 영상 클립

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SetupVideo();
    }

    private void SetupVideo()
    {
        if (videoPlayer == null || videoClip == null)
        {
            Debug.LogError("VideoManager: 필수 컴포넌트가 미할당");
            return;
        }

        // 비디오 플레이어 설정
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;

        // 렌더 타겟으로 Texture 사용 설정
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
        videoPlayer.prepareCompleted += OnVideoPrepared;

        videoPlayer.loopPointReached += OnVideoFinished;

        // 영상 준비 시작
        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        // 비디오 텍스처를 RawImage에 출력
        rawImage.texture = videoPlayer.texture;

        // 재생 시작
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("영상 끝, 다음 씬으로 이동");
        SceneManager.LoadScene(nextSceneName);
    }
}


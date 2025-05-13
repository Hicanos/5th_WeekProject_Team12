using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _mainBGMs;
    private AudioSource audioSource;

    public bool IsAudioSourceChanged { get; set; } = false;

    private static AudioManager m_instance;
    public static AudioManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            return m_instance;
        }
    }

    void Awake()
    {
        // Prevent Double Init. of AudioManager
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = Helper.GetComponentHelper<AudioSource>(gameObject);
        audioSource.clip = _mainBGMs[0];
        audioSource.volume = 1f;
        audioSource.Play();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = Helper.GetCurrentSceneName();
        int bgmIndex = GetBGMIndexForScene(sceneName);
        StartCoroutine(FadeInSound(bgmIndex, 1f));
    }

    private int GetBGMIndexForScene(string sceneName) //Scene 이름에 따라 다른 BGM이 재생되게끔.
    {
        switch (sceneName)
        {
            case "Title": return 0;
            case "SelectStage": return 1;
            case "Tutorial": return 2;
            case "Stage_1_1": return 3;
            case "Stage_1_2":
            case "Stage_1_3":
            case "Stage_2_1": return 4;
            case "Stage_2_2":
            case "Stage_2_3":
            case "Stage_3_1": return 5;
            case "Stage_3_2":
            case "Stage_3_3":
            case "EndingScene": return 6;
            default: return 0;
        }
    }


    public IEnumerator FadeOutSound(float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();

        IsAudioSourceChanged = true;
    }

    public IEnumerator FadeInSound(int category, float duration)
    {
        IsAudioSourceChanged = false;

        audioSource.clip = _mainBGMs[category];
        audioSource.Play();

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }

        audioSource.volume = 1f;
    }
    public void SetVolume(float volume)
    {
        // audioSource가 null이 아니면 (즉, 정상적으로 할당되어 있으면)
        if (audioSource != null)
            // volume 값을 0~1 사이로 제한해서 설정
            audioSource.volume = Mathf.Clamp01(volume);
    }

    //읽기 전용 프로퍼티
    public float CurrentVolume => audioSource.volume;

}

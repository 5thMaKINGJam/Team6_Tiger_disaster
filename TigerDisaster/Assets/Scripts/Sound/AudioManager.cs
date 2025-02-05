using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 새로 로드된 씬에서 이미 AudioManager가 존재하면 삭제
            if (SceneManager.GetActiveScene().name == "startMenu")
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
      
    }
    private void OnEnable()
    {
        // 씬 로드 후 처리
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (sceneName == "startMenu")
        {
            PlayMusic("Start");  // "startMenu" 씬일 경우 "Start" 음악 재생
        }
        else if (sceneName == "Stage")
        {
            PlayBGMforMainGame();
        }
        else
        {
            //에필로그
        }
    }

    private void Start()
    {
        PlayMusic("Start");
    }

    public void PlayBGMforMainGame()
    {
        Tuple<int, int> dayAndTurn = SaveManager.getDayAndTurn();
        int day = dayAndTurn.Item1;

        if (day == 0 || day == 1)
        {
            PlayMusic("1bgm"); // 1BGM을 재생
        }
        else if (day == 2)
        {
            PlayMusic("2bgm"); // 1BGM을 재생
        }
        else if (day == 3)
        {
            PlayMusic("3bgm"); // 1BGM을 재생
        }
    }

    public void FadeOutMusic(float fadeDuration)
    {
        StartCoroutine(FadeOutCoroutine(musicSource, fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // 원래 볼륨으로 복원
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound 없음");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound 없음");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop(); // SFX 재생 중단
        sfxSource.loop = false; // 루프 설정 해제
    }

    public void AdjustSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp(volume, 0f, 1f); // 볼륨 조절 (0~1 범위)
    }

    public void AdjustSFXLoop(bool isLoop)
    {
        sfxSource.loop = isLoop; // Loop 설정
    }
    public void MuteAllMusic()
    {
        musicSource.mute = true;  // BGM 뮤트
        sfxSource.mute = true;    // SFX 뮤트
    }
    public void UnmuteAllMusic()
    {
        musicSource.mute = false;  // BGM 음소거 해제
        sfxSource.mute = false;    // SFX 음소거 해제
    }

}

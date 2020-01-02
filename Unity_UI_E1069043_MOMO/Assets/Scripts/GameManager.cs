using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    [Header("Mixer")]
    public AudioMixer mixer;
    public Slider sliderBGM, sliderSFX;
    [Header("Loading")]
    public Text loadText;
    public Slider loadSlider;

    void Start () {
        Time.timeScale = 1;
    }
    public void volBGM (float value) {
        mixer.SetFloat("Vol_BGM", value);
    }
    public void volSFX (float value) {
        mixer.SetFloat("Vol_SFX", value);
    }
    public void Play () {
        StartCoroutine(Loading());
    }
    private IEnumerator Loading () {
        AsyncOperation ao = SceneManager.LoadSceneAsync("遊戲場景");
        ao.allowSceneActivation = false;
        while (ao.isDone == false) {
            loadText.text = "LOADING "+((ao.progress / 0.9f) * 100).ToString("f0") + " %";
            loadSlider.value = ao.progress / 0.9f;
            yield return new WaitForSeconds(0.025f);
            if (ao.progress == 0.9f && Input.anyKey) ao.allowSceneActivation = true;
        }
    }
    public void Return () {
        SceneManager.LoadScene("標題畫面");
    }
    public void Quit () {
        print("QUIT");
        Application.Quit();
    }
}

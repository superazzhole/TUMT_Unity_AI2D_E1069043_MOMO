using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour {

    [Header("Pause")]
    public Image pauseImage;
    public Sprite spritePause, spritePlay;
    public bool pause;

    public void Pause () {
        print("Paused");
        pause = !pause;
        pauseImage.sprite = pause ? spritePlay : spritePause;
        if (pause == true) Time.timeScale = 0;
        else if (pause == false) Time.timeScale = 1;
    }
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;
    }
}

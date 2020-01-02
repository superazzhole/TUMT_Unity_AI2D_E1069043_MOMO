using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour{
    [Header("HP")]
    public float hp;
    public Slider hpBar;
    [Header("vIcons")]
    public Text textIcon;
    private int iconCount, iconTotal;
    [Header("Time")]
    public Text textTime;
    private float gameTime = 0f;
    [Header("Result")]
    public GameObject result;
    public GameObject failed;
    public Text textBest;
    public Text textCurrent;
    public Text textBestDead;

    // Use this for initialization
    void Start () {
        iconTotal = GameObject.FindGameObjectsWithTag("vIcons").Length;
        textIcon.text = "V " + iconCount + " / " + iconTotal;
        if (PlayerPrefs.GetFloat("BestTime") == 0) PlayerPrefs.SetFloat("BestTime", 9999);
    }
    // Update is called once per frame
    void Update () {
        gameTime += Time.deltaTime;
        textTime.text = "TIME " + gameTime.ToString("f1");
        if (hp <= 0) Failed();
    }
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "vIcons") {
            iconCount++;
            textIcon.text = "V " + iconCount + " / " + iconTotal;
        }
        if (other.name == "Goal" && iconCount == iconTotal) {
            print("GOAL!!");
            Result();
        }
        if (other.tag == "Trap") {
            float dmg = other.GetComponent<Trap>().damage;
            print("DAMAGED");
            hp -= dmg;
            hpBar.value = hp;
        }
        if (other.tag == "debug") {
            print("reset best time");
            PlayerPrefs.SetFloat("BestTime", 9999);
        }
    }
    private void OnParticleCollision (GameObject other) {
        if (other.tag == "Trap") {
            float dmg = other.GetComponent<Trap>().damage;
            print("DAMAGED");
            hp -= dmg;
            hpBar.value = hp;
        }
    }
    private void Result () {
        Cursor.lockState = CursorLockMode.None;
        result.SetActive(true);
        textCurrent.text = "TIME " + gameTime.ToString("f1");
        if (gameTime < PlayerPrefs.GetFloat("BestTime")) PlayerPrefs.SetFloat("BestTime", gameTime);
        textBest.text = "BEST " + PlayerPrefs.GetFloat("BestTime", gameTime).ToString("f1");
        Time.timeScale = 0;
    }
    private void Failed () {
        Cursor.lockState = CursorLockMode.None;
        failed.SetActive(true);
        textBestDead.text = "BEST " + PlayerPrefs.GetFloat("BestTime", gameTime).ToString("f1");
        Time.timeScale = 0;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    //[SerializeField] GameObject _startUI;
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject PlayUI;
    [SerializeField] Text currentLevel;
    [SerializeField] Text currentLevel2;

    [Header("SoundManager")]
    [SerializeField] AudioSource finishSound;
    [SerializeField] AudioSource loseSound;
    [SerializeField] public AudioSource addBrick;
    [SerializeField] public AudioSource putBrick;
    [SerializeField] public AudioSource jump;
    [SerializeField] AudioSource music;

    public bool isPlay = true;
    public bool isLose;
    public bool isFinish;
    public bool isLate;
    void Awake()
    {
        UIManager.Instance = this;
    }
    private void Start()
    {
        currentLevel.text = "Level " + PlayerPrefs.GetInt("level").ToString();
        currentLevel2.text = currentLevel.text;
    }
    void Update()
    {
        if (isPlay && isLose)
        {
            isPlay = false;
            loseSound.Play();
            music.Stop();
            Invoke("Lose", 0.5f);
        }
        if (isPlay && isFinish)
        {
            if (isLate)
            {
                isLose = true;
                return;
            }
            isPlay = false;
            finishSound.Play();
            music.Stop();
            Invoke("Finish", 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Space)) ChangeMove();
    }
    void ChangeMove()
    {
        if (isPlay) isPlay = false;
        else isPlay = true;
    }
    void Finish()
    {
        finishUI.SetActive(true);
        PlayUI.SetActive(false);
    }
    void Lose()
    {
        loseUI.SetActive(true);
        PlayUI.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        isPlay = true;
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}

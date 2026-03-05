using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField]
    int timeToEnd;

    bool gamePaused = false;

    bool endGame = false;
    bool win = false;

    public int points = 0;

    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    public Text timeText;
    public Text goldKeyText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text crystalText;
    public Image snowFlake;
    public GameObject infoPanel;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useInfo;

    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        Time.timeScale = 1f;
        InvokeRepeating("Stopper", 2, 1);

        if(timeToEnd <= 0)
        {
            timeToEnd = 100;
        }

        audioSource = GetComponent<AudioSource>();
        snowFlake.enabled = false;
        timeText.text = timeToEnd.ToString();
        infoPanel.SetActive(false);
        pauseEnd.text = "Pause";
        reloadInfo.text = "";
        SetUseInfo("");

    }

    public void SetUseInfo(string info)
    {
        useInfo.text = info;
    }


    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    void PickUpCheck()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Actual Time: " + timeToEnd);
            Debug.Log("Key red: " + redKey + " green: " + greenKey + " gold: " + goldKey);
            Debug.Log("Points: " + points);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PauseCheck();
        PickUpCheck();

        if (endGame)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("LabTest");
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Application.Quit();
            }
        }
    }

    void Stopper()
    {
        timeToEnd--;
        //Debug.Log("Time: " + timeToEnd + " s");
        timeText.text = timeToEnd.ToString();
        snowFlake.enabled = false;

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }

        if(endGame)
        {
            EndGame();
        }

    }


    public void PauseGame()
    {
        PlayClip(pauseClip);
        //Debug.Log("Pause Game");
        infoPanel.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        PlayClip(resumeClip);
        //Debug.Log("Resume Game");
        infoPanel.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void PauseCheck()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void EndGame()
    {        
        CancelInvoke("Stopper");
        infoPanel.SetActive(true);
        Time.timeScale = 0f;
        if(win)
        {
            PlayClip(winClip);
            //Debug.Log("You win!!! Reload?");
            pauseEnd.text = "You Win!!!";
            reloadInfo.text = "Reload? Y/N";
        }
        else
        {
            PlayClip(loseClip);
            //Debug.Log("You Lose!!! Reload?");
            pauseEnd.text = "You Lose!!!";
            reloadInfo.text = "Reload? Y/N";
        }
    }


    public void AddPoints(int point)
    {
        points += point;
        crystalText.text = points.ToString();
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        timeText.text = points.ToString();
    }

    public void FreezeTime(int freeze)
    {
        CancelInvoke("Stopper");
        snowFlake.enabled = true;
        InvokeRepeating("Stopper", freeze, 1);
    }

    public void AddKey(KeyColor color)
    {
        if(color == KeyColor.Gold)
        {
            goldKey++;
            goldKeyText.text = goldKey.ToString();
        }
        else if(color == KeyColor.Red)
        {
            redKey++;
            redKeyText.text = redKey.ToString();
        }
        else if (color == KeyColor.Green)
        {
            greenKey++;
            greenKeyText.text = greenKey.ToString();
        }

    }

    public void WinGame()
    {
        win = true;
        endGame = true;
    }

}

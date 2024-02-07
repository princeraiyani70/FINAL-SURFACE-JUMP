using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] Structure;

    [SerializeField]
    GameObject SettingsPanel, MainmenuPanel, GameOverPanel, SuccessPanel,GameScreen;

    [SerializeField]
    Transform ParentObject;

    [SerializeField]
    TextMeshProUGUI CurrentLevelText, NextLevelText;

    [SerializeField]
    AudioSource SoundSource;

    [SerializeField]
    Sprite SoundOnSprite, SoundOffSprite;

    [SerializeField]
    Button SoundButton;

    [SerializeField]
    AudioClip[] SoundClips;


    public Slider SliderBg;

    int CurrentLevel, NextLevel;

    public static GameManager instance;

    public bool GetScore;

    public bool Rotate;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GettingSoundData();


        GameObject Ring;
        int Levels = PlayerPrefs.GetInt("Level", 5);
        CurrentLevel = Levels - 5;
        NextLevel = CurrentLevel + 1;
        CurrentLevelText.text = CurrentLevel.ToString();
        NextLevelText.text = NextLevel.ToString();

        SliderBg.GetComponent<Slider>().maxValue = Levels;

        for (int i = 0; i <= Levels; i++)
        {
            if (i == 0)
            {
                Ring = Instantiate(Structure[0].gameObject, ParentObject);
            }
            else if (i == Levels)
            {
                Ring = Instantiate(Structure[Structure.Length - 1], ParentObject);
            }
            else
            {
                Ring = Instantiate(Structure[Random.Range(1, Structure.Length - 1)], ParentObject);
            }
            Ring.gameObject.transform.position = (new Vector3(Ring.transform.position.x, -(i) * 3, Ring.transform.position.z));
            Ring.gameObject.transform.rotation = Quaternion.Euler(0f,Random.Range(0,360), 0f);
         }

    }

    public void RetryButtonClickAction()
    {
       // SoundManagement();
        SoundPlayOnButtonClick();
        int Score = PlayerPrefs.GetInt("Score", 0);
        Score = 0;
        PlayerPrefs.SetInt("Score", Score);
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        Rotate = true;
        GameOverPanel.SetActive(true);
        StartCoroutine(GameOverPanelClose());
    }

    IEnumerator GameOverPanelClose()
    {
        yield return new WaitForSeconds(2.5f);
        GameOverPanel.transform.GetChild(0).GetComponent<Animator>().Play("GameOverTextExit");
        StartCoroutine(GameOverBgWaiting());
    }

    IEnumerator GameOverBgWaiting()
    {
        yield return new WaitForSeconds(0.30f);
        GameOverPanel.SetActive(false);
       // SoundManagement();
        SceneManager.LoadScene(1);
    }

    public void Success()
    {
        Rotate = true;
        SuccessPanel.SetActive(true);
        StartCoroutine(SuccessPanelClose());
    }

    IEnumerator SuccessPanelClose()
    {
        yield return new WaitForSeconds(2.5f);
        SuccessPanel.transform.GetChild(0).GetComponent<Animator>().Play("DoneTextExit");
        StartCoroutine(SuccessPanelBgWaiting());
    }

    IEnumerator SuccessPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.30f);
        SuccessPanel.SetActive(false);
        //SoundManagement();
        SceneManager.LoadScene(1);
    }

    public void BackButtonClickAction()
    {
        SoundPlayOnButtonClick();
        int Score = PlayerPrefs.GetInt("Score", 0);
        Score = 0;
        PlayerPrefs.SetInt("Score", Score);

        GameScreen.transform.GetChild(0).GetComponent<Animator>().Play("ScoreExit");
        GameScreen.transform.GetChild(1).GetComponent<Animator>().Play("BestScoreExit");
        GameScreen.transform.GetChild(2).GetComponent<Animator>().Play("BackButtonExit");
        GameScreen.transform.GetChild(3).GetComponent<Animator>().Play("MainMenuButtonExit");
        GameScreen.transform.GetChild(4).GetComponent<Animator>().Play("SliderExit");
        StartCoroutine(SceneChangeWaiting());
    }

    IEnumerator SceneChangeWaiting()
    {
        yield return new WaitForSeconds(0.30f);
        SceneManager.LoadScene(0);
    }

    public void SettingsButtonClickAction()
    {
        Rotate = true;
        SoundPlayOnButtonClick();
        SettingsPanel.SetActive(true);
    }

    public void SettingsPanelCloseButtonClickAction()
    {
        SoundPlayOnButtonClick();
        SettingsPanel.transform.GetChild(0).GetComponent<Animator>().Play("MusicButtonExit");
        SettingsPanel.transform.GetChild(1).GetComponent<Animator>().Play("SoundButtonExit");
        SettingsPanel.transform.GetChild(2).GetComponent<Animator>().Play("SettingsCloseButtonExit");
        SettingsPanel.transform.GetChild(3).GetComponent<Animator>().Play("SettingsTextExit");
        StartCoroutine(SettingsPanelBgWaiting());
    }

    IEnumerator SettingsPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.30f);
        SettingsPanel.SetActive(false);
        Rotate = false;
    }

    public void MainMenuButtonClickAction()
    {
        Rotate = true;
        SoundPlayOnButtonClick();
        MainmenuPanel.SetActive(true);
    }

    public void MainMenuPanelCloseButtonClickAction()
    {
        SoundPlayOnButtonClick();
        MainmenuPanel.transform.GetChild(0).GetComponent<Animator>().Play("RetryButtonExit");
        MainmenuPanel.transform.GetChild(1).GetComponent<Animator>().Play("SoundButtonExit");
        MainmenuPanel.transform.GetChild(2).GetComponent<Animator>().Play("MainMenuCloseExit");
        MainmenuPanel.transform.GetChild(3).GetComponent<Animator>().Play("MainMenuTextExit");
        StartCoroutine(MainMenuPanelBgWaiting());
    }

    IEnumerator MainMenuPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.3f);
        MainmenuPanel.SetActive(false);
        Rotate = false;
    }


    public void SoundManagement()
    {
        int Sound = PlayerPrefs.GetInt("Sound", 0);

        if (Sound == 1)
        {
            SoundSource.mute = false;
            SoundButton.GetComponent<Image>().sprite = SoundOnSprite;
            PlayerPrefs.SetInt("Sound", 0);
        }
        if (Sound == 0)
        {
           
            SoundSource.mute = true;
            SoundButton.GetComponent<Image>().sprite = SoundOffSprite;  
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    public void GettingSoundData()
    {
        int Sound = PlayerPrefs.GetInt("Sound", 0);

        if (Sound == 1)
        {
            SoundSource.mute = true;
            SoundButton.GetComponent<Image>().sprite = SoundOffSprite;
        }
        if (Sound == 0)
        {
            SoundSource.mute = false;
            SoundButton.GetComponent<Image>().sprite = SoundOnSprite;
            SoundPlayOnButtonClick();
        }
    }

    public void SoundPlayOnButtonClick()
    {
        SoundSource.clip = SoundClips[0];
       SoundSource.Play();
    }

    public void SoundPlayOnHelixBreak()
    {
        SoundSource.clip = SoundClips[1];
        SoundSource.Play();
    }

    public void SoundPlayOnBouncing()
    {
        SoundSource.clip = SoundClips[2];
        SoundSource.Play();
    }

}

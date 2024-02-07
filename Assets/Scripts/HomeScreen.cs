using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    [SerializeField]
    GameObject SettingsPanel, ExitPanel,HomePanel;

    public void PlayButtonClickAction()
    {
        SoundPlayOnButtonClick();
        HomePanel.transform.GetChild(4).GetComponent<Animator>().Play("ExitButtonExit");
        HomePanel.transform.GetChild(3).GetComponent<Animator>().Play("SettingsButtonExit");
        HomePanel.transform.GetChild(1).GetComponent<Animator>().Play("StructurePhotoExit");
        SceneManager.LoadScene(1);
    }

    public void SettingButtonClickAction()
    {
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
    }

    public void ExitButtonClickAction()
    {
        SoundPlayOnButtonClick();
        ExitPanel.SetActive(true);
    }

    public void ExitPanelYesClickAction()
    {
        SoundPlayOnButtonClick();
        int Score = PlayerPrefs.GetInt("Score", 0);
        Score = 0;
        PlayerPrefs.SetInt("Score", Score);
        Application.Quit();
    }

    public void ExitPanelNoClickAction()
    {
        SoundPlayOnButtonClick();
        ExitPanel.transform.GetChild(0).GetComponent<Animator>().Play("YesButtonExit");
        ExitPanel.transform.GetChild(1).GetComponent<Animator>().Play("NoButtonExit");
        ExitPanel.transform.GetChild(2).GetComponent<Animator>().Play("AreYouSureExit");
        StartCoroutine(ExitPanelBgWaiting());
    }

    IEnumerator ExitPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.30f);
        ExitPanel.SetActive(false);
    }


    public void SoundPlayOnButtonClick()
    {
        SoundManager.Instance.SoundSource.Play();
    }
}

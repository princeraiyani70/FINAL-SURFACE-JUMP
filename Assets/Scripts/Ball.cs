using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class Ball : MonoBehaviour
{
    [SerializeField]
    float JumpSpeed;

    [SerializeField]
    GameObject SplashImage,AllSplashes;

    GameObject ParentOfHelix;

    [SerializeField]
    TextMeshProUGUI ScoreText,BestScoreText;

    [SerializeField]
    ParticleSystem ExplosionParticles;

    bool Bounce;

    Rigidbody RB;

    int BestScore;
    int Score = 0;

    bool Levelup;

    private void Awake()
    {
        RB= GetComponent<Rigidbody>();

        BestScore = PlayerPrefs.GetInt("BestScore",0);
        BestScoreText.text = "Best : " + BestScore.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.SoundPlayOnBouncing();
        if (collision.gameObject.tag == "Helix" && Bounce == false)
        {
            GameObject Splashes = Instantiate(SplashImage, new Vector3(transform.position.x, transform.position.y - 0.292148f, transform.position.z - 0.015f), transform.rotation * Quaternion.Euler(-90f, 0f, Random.Range(0, 360)));
            Splashes.transform.parent = collision.gameObject.transform;
            ExplosionParticles.Play();
            Destroy(Splashes.gameObject, 2f);
            RB.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            Bounce = true;


        }
        if (collision.gameObject.tag == "Enemy")
        {
            Score = PlayerPrefs.GetInt("Score", 0);
            Score = 0;
            PlayerPrefs.SetInt("Score", Score);
            GameManager.instance.GameOver();
            //Debug.Log("Game Over");
        }
        if (collision.gameObject.tag == "Last")
        {
            if (!Levelup)
            {
                int Levels = PlayerPrefs.GetInt("Level", 5);
                Levels++;
                PlayerPrefs.SetInt("Level", Levels);
                GameManager.instance.Success();
            }
        }
        Invoke("DelayBouncing", 0.5f);

        //if (collision.gameObject.transform.parent.GetComponentInParent<Transform>().tag == "Score")
        //{
        //    if (collision.gameObject.transform.parent.name != "First")
        //    {
        //        if (ParentOfHelix != collision.gameObject.transform.parent.gameObject)
        //        {
                    
        //        }
        //        ParentOfHelix = collision.gameObject.transform.parent.gameObject;
        //    }
        //}
    }

    private void Update()
    {
        Score = PlayerPrefs.GetInt("Score", 0);
        ScoreText.text = Score.ToString();
        PlayerPrefs.SetInt("Score", Score);

        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (Score > BestScore)
        {
            BestScore = Score;
            BestScoreText.text = "Best : " + BestScore.ToString();
            PlayerPrefs.SetInt("BestScore", BestScore);
            //Debug.Log("best score is:= " + BestScore);
        }
    }

    public void DelayBouncing()
    {
        Bounce = false;
    }

    private void OnTriggerEnter(Collider Other)
    {
        GameManager.instance.SoundPlayOnHelixBreak();
        foreach (Transform child in Other.transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.GetComponent<Rigidbody>().AddExplosionForce(400, child.transform.position, 2f);
            Other.transform.GetComponent<BoxCollider>().enabled = false;
            Destroy(child.gameObject, 0.7f);
        }

        Score = PlayerPrefs.GetInt("Score", 0);
        Score += 2;
        PlayerPrefs.SetInt("Score", Score);

        GameManager.instance.SliderBg.DOValue(GameManager.instance.SliderBg.value + 1, 1f);
    }
}

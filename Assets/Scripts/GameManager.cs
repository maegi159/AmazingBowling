using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // 이벤트가 발동하는 순간에 이벤트에 있는 함수가 자동으로 발동됨
    public UnityEvent onReset;

    public static GameManager instance;

    public GameObject readyPannel;
    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;

    public bool isRoundActive = false;
    private int score = 0;
    public ShooterRotator shooterRotator;
    public CamFollow cam;

    private void Awake()
    {
       instance = this;
       UpdateUI();
    }

    private void Start()
    {
        StartCoroutine("RoundRoutine");
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if (GetBestScore() < score) {
            // key값이 BestScore인 값에 덮어씀
            // 해킹하기 쉬움.
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    int GetBestScore()
    {
        //Key값이 BestScore인 값을 가져옴
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }
    
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        bestScoreText.text = "BestScore: " + GetBestScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();
        StartCoroutine("RoundRoutine");
        // 라운드를 다시 처음부터 시작하는 코드 Coroutine으로 구현
        
    }

    IEnumerator RoundRoutine()
    {
        onReset.Invoke();

        // ready
        readyPannel.SetActive(true);
        // 캠이 Idle 상태
        cam.SetTarget(shooterRotator.transform,CamFollow.State.Idle);
        // 레디상태에선 조작 불가
        shooterRotator.enabled = false;
        isRoundActive = false;
        messageText.text = "Ready..";
        yield return new WaitForSeconds(3f);

        // play
        isRoundActive = true;
        readyPannel.SetActive(false);
        // 플레이 상태에서 조작 가능
        shooterRotator.enabled = true;
        // 캠이 Ready 상태
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while (isRoundActive == true)
        {
            yield return null;
        }
        // end
        readyPannel.SetActive(true);
        shooterRotator.enabled = false;
        messageText.text = "Wait For Next Round..";

        yield return new WaitForSeconds(3f);
        Reset();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Escpae"))
            Application.Quit();
    }
}

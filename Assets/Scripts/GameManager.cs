using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int _stageTime;
    //[SerializeField] private GameObject _stageManager;
    [SerializeField] private StageHandler _currentStageManager;
    [SerializeField] private UnityEvent FinishedLoop;
    [SerializeField] private string level;

    private bool _stageStarted;

    private void Start()
    {
        TimeManager.Instance.SetUp(this, _stageTime);

        StartCoroutine(GameLoop());
    }

    IEnumerator Preparation()
    {
        Debug.Log("Ready?");

        yield return new WaitForSeconds(2);

        TimeManager.Instance.InitiateCountdown();
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(2);

        do
        {
            yield return StartCoroutine(Preparation());
        }
        while (_stageStarted);

        yield return StartCoroutine(TimeRunning());
    }

    IEnumerator TimeRunning()
    {
        _currentStageManager.Initiate();
        TimeManager.Instance.InitiateTimer();
        yield return null;
    }

    public void EndLoop()
    {
        StopAllCoroutines();
        TimeManager.Instance.FrezeTime();
        FinishedLoop?.Invoke();

        LevelsManager.Instance.LoadScene(level);
    }
}

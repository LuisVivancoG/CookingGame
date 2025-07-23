using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private int _stageTime;
    [SerializeField] private GameObject _stageManager;
    [SerializeField] private UnityEvent FinishedLoop;
    [SerializeField] private string level;

    private void Start()
    {
        _timeManager.SetUp(this, _stageTime);

        StartCoroutine(GameLoop());
    }

    IEnumerator Preparation()
    {
        Debug.Log("Ready?");

        yield return new WaitForSeconds(2);

        _timeManager.InitiateTimer();
        _stageManager.SetActive(true);
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(Preparation());
    }

    public void EndLoop()
    {
        StopAllCoroutines();
        _timeManager.FrezeTime();
        _stageManager.SetActive(false);
        FinishedLoop?.Invoke();

        LevelsManager.Instance.LoadScene(level);
    }
}

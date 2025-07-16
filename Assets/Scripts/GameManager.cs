using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*[SerializeField] private GameObject _miniGame1;
    [SerializeField] private GameObject _miniGame2;
    [SerializeField] private GameObject _miniGame3;*/

    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private int _stageTime;

    [SerializeField] private GameObject _stageManager;

    [SerializeField] private UnityEvent FinishedLoop;

    [SerializeField] private string level;

    /*private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            _miniGame1.SetActive(true);
            _miniGame2.SetActive(false);
            _miniGame3.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            _miniGame1.SetActive(false);
            _miniGame2.SetActive(true);
            _miniGame3.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            _miniGame1.SetActive(false);
            _miniGame2.SetActive(false);
            _miniGame3.SetActive(true);
        }
    }*/

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

        StartCoroutine(Transitioning());
    }

    IEnumerator Transitioning()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level/*SceneManager.GetActiveScene().buildIndex + 1*/);
    }
}

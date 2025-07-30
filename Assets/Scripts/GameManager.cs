using System.Collections;
using Timer;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _buttonGO;
    [SerializeField] private int _stageTime;
    //[SerializeField] private GameObject _stageManager;
    [SerializeField] private StageHandler _currentStageManager;
    [SerializeField] private UnityEvent FinishedLoop;
    [SerializeField] private string level;

    private bool _stageStarted;

    private void Start()
    {
        StartCoroutine(TipButton());
    }
    IEnumerator TipButton()
    {
        yield return new WaitForSeconds(1.8f);
        _buttonGO.SetActive(true);
    }

    public void InstructionsAccepted() //Set timer for current stage 
    {
        StartCoroutine(Preparation());
    }
    /*private IEnumerator GameLoop()
    {
        StartCoroutine(Preparation());

        yield return null;
    }*/
    IEnumerator Preparation()
    {
        //yield return new WaitForSeconds(.5f);

        TimeManager.Instance.SetUp(this, _stageTime, _currentStageManager);

        yield return null;
    }

    public void EndLoop()
    {
        Debug.Log($"Finishing stage {_stageTime}");
        StopAllCoroutines();
        TimeManager.Instance.FrezeTime();
        FinishedLoop?.Invoke();
    }

    public void LoadNextLevel()
    {
        LevelsManager.Instance.LoadScene(level);
    }
}

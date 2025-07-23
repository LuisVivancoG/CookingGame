using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : Singleton<LevelsManager>
{
    [Header("UI Prefab Setup")]
    [SerializeField] private GameObject _loadingUIPrefab;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _fadeOutDuration = .6f;
    private float _target = 0f;
    private Color _initialColor;

    protected override void Awake()
    {
        base.Awake();

        /*_loadingUIInstance = Instantiate(_loadingUIPrefab);
        DontDestroyOnLoad(_loadingUIInstance);

        _progressBar = _loadingUIInstance.GetComponentInChildren<Image>();*/
        _loadingUIPrefab.SetActive(false);
    }

    private void Start()
    {
        _initialColor = _progressBar.color;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }
    public void RestartScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        yield return LoadSceneInternal(SceneManager.LoadSceneAsync(sceneName));
    }
    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        yield return LoadSceneInternal(SceneManager.LoadSceneAsync(sceneIndex));
    }
    private IEnumerator LoadSceneInternal(AsyncOperation asyncOp)
    {
        _progressBar.color = _initialColor;

        _target = 0f;
        _progressBar.fillAmount = 0f;

        _loadingUIPrefab.SetActive(true);

        asyncOp.allowSceneActivation = false;

        float timeout = 10f;
        float timer = 0f;

        while (asyncOp.progress < 0.89f && timer < timeout)
        {
            _target = asyncOp.progress;
            timer += Time.deltaTime;
            yield return null;
        }

        if (timer >= timeout)
        {
            Debug.LogWarning("Scene loading timeout reached. Forcing activation.");
        }

        _target = 1f;
        yield return new WaitForSeconds(0.5f);

        asyncOp.allowSceneActivation = true;

        while (!asyncOp.isDone)
            yield return null;

        yield return new WaitForSeconds(0.1f);
        _loadingUIPrefab.SetActive(false);
    }

    private void Update()
    {
        if (_progressBar != null)
        {
            _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3f * Time.deltaTime);
            if (_progressBar.fillAmount >= 1)
            {
                StartCoroutine(FadeCourtine(_fadeOutDuration));
            }
        }
    }

    IEnumerator FadeCourtine(float duration)
    {
        float elapsedTime = 0;
        float startValue = _progressBar.color.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0, elapsedTime/duration);
            _progressBar.color = new Color(_progressBar.color.r, _progressBar.color.g, _progressBar.color.b, newAlpha);
            yield return null;
        }
    }
}

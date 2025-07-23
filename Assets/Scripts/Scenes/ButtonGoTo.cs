using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoTo : MonoBehaviour
{
    [SerializeField] private string _level;
    private LevelsManager _levelManager;

    /*private void OnLevelWasLoaded()
    {
        _levelManager = LevelsManager.Instance;
    }*/

    public void GoToScene()
    {
        LevelsManager.Instance.LoadScene(_level);
    }
}

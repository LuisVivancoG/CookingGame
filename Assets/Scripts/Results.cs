using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resultTxt;
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private MarinateManager _marinateManager;

    public void CheckResult()
    {
        var finalResult = _scrollBar.value;

        if(finalResult < _marinateManager.MinRange)
        {
            _resultTxt.text = ("Who cares about taste anyways");
        }
        else if (finalResult > _marinateManager.MaxRange)
        {
            _resultTxt.text = ("Too much icky");
        }
        else if (finalResult >= _marinateManager.MinRange && finalResult <= _marinateManager.MaxRange)
        {
            _resultTxt.text = ("You have a talent for cooking");
        }
    }
}

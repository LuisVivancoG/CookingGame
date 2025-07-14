using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManag : MonoBehaviour
{
    [SerializeField] private GameObject _miniGame1;
    [SerializeField] private GameObject _miniGame2;
    [SerializeField] private GameObject _miniGame3;

    private void Update()
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
    }
}

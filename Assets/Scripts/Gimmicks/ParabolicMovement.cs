using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _newPos;
    [SerializeField] private float _cycleLenght;
    [SerializeField] private Ease _easeMode;
    [SerializeField] private SquashAndStretch _squashAndStretch;

    private void OnEnable()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        MoveObject();

        yield return new WaitForSeconds(_cycleLenght);

        _squashAndStretch.CheckForAndStartCoroutine();
    }

    public void MoveObject()
    {
        transform.DOMove(_newPos, _cycleLenght).SetEase(_easeMode).SetLoops(0);
    }
}

using System.Collections.Generic;
using Timer;
using UnityEngine;
using UnityEngine.Events;

public class SliceIngredient : MonoBehaviour
{
    [SerializeField] private SliceSprite _spriteToSlice;

    [Header("Cutting Setup")]
    [SerializeField] private List<Transform> cutStartPoints;
    [SerializeField] private List<Transform> cutEndPoints;
    [SerializeField] private List<LineRenderer> guides;
    [SerializeField] private float cutTolerance = 0.5f;
    [SerializeField] private List<string> namesList = new List<string>();

    private int currentCutIndex = 0;
    private Camera mainCamera;
    private bool isDragging = false;
    private bool cutCompletedDuringDrag = false;
    private StageHandler stageHandler;
    private Transform _piecesParent;

    [Header("Fail Setup")]
    //[SerializeField] private UnityEvent OnFailedSlice;
    [SerializeField] private int _penalty = 5;
    private Vector3 lastDragStartPos;

    [Header("Completed")]
    [SerializeField] private UnityEvent OnFinished;

    private void Awake()
    {
        this.gameObject.name = namesList[currentCutIndex];
    }
    void Start()
    {
        mainCamera = Camera.main;
        ShowGuideLine();
    }

    public void SetStageManager(StageHandler handler, Transform parent)
    {
        stageHandler = handler;
        _piecesParent = parent;
    }

    void Update()
    {
        Vector3 mousePos = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 expectedStart = cutStartPoints[currentCutIndex].position;

            if (Vector2.Distance(mousePos, expectedStart) <= cutTolerance)
            {
                isDragging = true;
                cutCompletedDuringDrag = false;
                lastDragStartPos = mousePos;
            }
        }

        if (isDragging && !cutCompletedDuringDrag)
        {
            Vector3 expectedEnd = cutEndPoints[currentCutIndex].position;

            if (Vector2.Distance(mousePos, expectedEnd) <= cutTolerance)
            {
                MakeCutVisual();
                cutCompletedDuringDrag = true;
                currentCutIndex++;

                if (currentCutIndex >= cutStartPoints.Count)
                {
                    OnCuttingComplete();
                }
                else
                {
                    ShowGuideLine();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging && !cutCompletedDuringDrag)
            {
                TimeManager.Instance.TimeConsume(_penalty);
                //OnFailedSlice?.Invoke();
                // Debug.Log("Cut failed.");
            }

            isDragging = false;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(screenPos);
    }

    private void ShowGuideLine()
    {
        for (int i = 0; i < guides.Count; i++)
        {
            guides[i].enabled = (i == currentCutIndex);
        }
    }

    private void MakeCutVisual()
    {
        _spriteToSlice.MakeSlice(_piecesParent);
        if (namesList.Count > currentCutIndex)
        {
            if (currentCutIndex + 1 < namesList.Count)
            {
                this.gameObject.name = namesList[currentCutIndex + 1];
            }
            else return;
        }
    }

    private void OnCuttingComplete()
    {
        foreach (var guide in guides)
        {
            guide.enabled = false;
        }
        OnFinished?.Invoke();
        stageHandler.FetchNextIngredient();
    }
}

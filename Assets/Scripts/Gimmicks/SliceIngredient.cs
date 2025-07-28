using System.Collections.Generic;
using Timer;
using UnityEngine;
using UnityEngine.Events;

public class SliceIngredient : MonoBehaviour
{
    [Header("Cutting Setup")]
    [SerializeField] private List<Transform> cutStartPoints;
    [SerializeField] private List<Transform> cutEndPoints;
    [SerializeField] private List<LineRenderer> guides;
    [SerializeField] private List<GameObject> slicedSprites;
    [SerializeField] private float cutTolerance = 0.5f;

    [SerializeField] private UnityEvent OnSlice;

    private int currentCutIndex = 0;
    private Camera mainCamera;
    private bool isDragging = false;
    private bool cutCompletedDuringDrag = false;
    private StageHandler stageHandler;

    [Header("Fail Setup")]
    //[SerializeField] private UnityEvent OnFailedSlice;
    [SerializeField] private int _penalty = 5;
    private Vector3 lastDragStartPos;

    [Header("Completed")]
    [SerializeField] private UnityEvent OnFinished;

    void Start()
    {
        mainCamera = Camera.main;
        ShowGuideLine();
    }

    public void SetStageManager(StageHandler handler)
    {
        stageHandler = handler;
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
        OnSlice?.Invoke();
        slicedSprites[currentCutIndex].SetActive(true);
        // Debug.Log("Cut #" + currentCutIndex + " done.");
    }

    private void OnCuttingComplete()
    {
        foreach (var guide in guides)
        {
            guide.enabled = false;
        }

        //Debug.Log("All cuts completed! Moving to next step.");

        OnFinished?.Invoke();
        stageHandler.FetchNextIngredient();
    }
}

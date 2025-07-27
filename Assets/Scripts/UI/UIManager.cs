using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _safeZone;
    [SerializeField] private InstructionsDialog _instructionsPrefab;
    //[SerializeField] private ConfirmationDialog _confirmationDialogPrefab;
    //[SerializeField] private BuildingOptions _buildingOptionsPrefab;
    //[SerializeField] private ArmyCampOptions _armyCampOptionsPrefab;
    //[SerializeField] private PauseMenu _pauseOptionsPrefab;
    //[SerializeField] private DefeatDialog _defeatPrefab;

    Dictionary<MenusEnum, DialogBase> _dialogInstances = new();

    Stack<DialogBase> _dialogStack = new Stack<DialogBase>();
    Dictionary<MenusEnum, DialogBase> _disabledDialogs = new ();

    private int _topSortingOrder = 0;
    private const int _sortOrderGap = 10;

    public DialogBase ShowDialog(MenusEnum dialogType)
    {
        return PushDialog(dialogType);
    }

    public DialogBase PushDialog(MenusEnum dialogType)
    {
        if (!_dialogInstances.ContainsKey(dialogType))
        {
            DialogBase created = null;
            switch (dialogType)
            {
                case MenusEnum.StageInstructions:
                    created = CreateDialogFromPrefab(_instructionsPrefab);
                    break;
                case MenusEnum.ConfirmationDialog:
                    //created = CreateDialogFromPrefab(_confirmationDialogPrefab);
                    break;
                case MenusEnum.Settings:
                    //created = CreateDialogFromPrefab(_armyCampOptionsPrefab);
                    break;
                case MenusEnum.PauseMenu:
                    //created = CreateDialogFromPrefab(_pauseOptionsPrefab);
                    break;
            }
            if (created == null)
            {
                Debug.LogError($"Could not created dialog from prefab: {dialogType}");
            }
            else
            {
                _dialogInstances.Add(dialogType, created);
            }
        }
        DialogBase instance = _dialogInstances[dialogType];
        if (_dialogStack.Contains(instance))
        {
            Debug.LogError($"Dialog is already pushed: {dialogType}");
        }
        else
        {
            if (_disabledDialogs.ContainsKey(dialogType))
            {
                _disabledDialogs.Remove(dialogType);
            }
            _dialogStack.Push(instance);
            instance.gameObject.SetActive(true);
            instance.DialogCanvas.overrideSorting = true;
            _topSortingOrder += _sortOrderGap;
            instance.DialogCanvas.sortingOrder = _topSortingOrder;
        }
        return instance;
    }

    private DialogBase CreateDialogFromPrefab(DialogBase dialogPrefab)
    {
        DialogBase created = Instantiate(dialogPrefab , _safeZone.transform);
        created.OnCreation(this);
        return created;
    }

    public void HideDialog(MenusEnum dialogType)
    {
        PopDialog(dialogType);
    }

    private void PopDialog(MenusEnum dialogType)
    {
        if (!_dialogInstances.ContainsKey(dialogType))
        {
            Debug.LogError($"Tried to pop dialog, but dialog was never created {dialogType}");
            return;
        }
        DialogBase instance = _dialogInstances[dialogType];

        if(_dialogStack.TryPeek(out DialogBase topDialogPeek))
        {
            if(topDialogPeek == instance)
            {
                DialogBase topDialog = _dialogStack.Pop();
                topDialog.gameObject.SetActive(false);
                _disabledDialogs.Add(topDialog.MenuType(), topDialog);
                _topSortingOrder -= _sortOrderGap;  
            }
            else
            {
                Debug.LogError($"Tried to pop the dialog type {dialogType} but the top dialog was {topDialogPeek.MenuType()}");
            }
        }
        else
        {
            Debug.LogError($"Failed to peek the top dialog");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBase : MonoBehaviour
{
    public Canvas DialogCanvas;
    protected UIManager _manager;
    public virtual MenusEnum MenuType()
    {
        return MenusEnum.Underfined;
    }

    public void OnCreation(UIManager manager)
    {
        _manager = manager;
    }
}

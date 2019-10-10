using UnityEngine;

public class UIManager : Manager
{
    [ContextMenu("Hide Cursor")]
    public void HideCursor(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    [ContextMenu("Show Cursor")]
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
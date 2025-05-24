using System;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private bool lockAndHideCursor;

        private void Awake()
        {
            if (!lockAndHideCursor) return;
            LockAndHideCursor();
        }
        
        [ContextMenu("Lock and Hide Cursor")]
        private void LockAndHideCursor() => SetCursorState(CursorLockMode.Locked, false);
        [ContextMenu("Unlock and Show Cursor")]
        private void UnlockAndShowCursor() => SetCursorState(CursorLockMode.None, true);

        public static void SetCursorState(CursorLockMode cursorLockMode, bool cursorVisible)
        {
            Cursor.lockState = cursorLockMode;
            Cursor.visible = cursorVisible;
        }
    }
}

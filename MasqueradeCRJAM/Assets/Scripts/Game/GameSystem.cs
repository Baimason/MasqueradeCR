using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private static GameSystem m_instance;
    private static GameSystem Instance
    {
        get
        {
            if (m_instance == null)
            {
                var gs = Resources.Load<GameSystem>("GameSystem");
                m_instance = Instantiate<GameSystem>(gs);
            }
            return m_instance;
        }
    }

    public static void CallNewMask(MaskObject.MaskData data)
    {
        Instance.CheckIfNewMask(data);
    }

    private static List<int> maskIds = new List<int>();

    public static void ResetState()
    {
        maskIds.Clear();
    }

    private void CheckIfNewMask(MaskObject.MaskData data)
    {
        if (!maskIds.Contains(data.id))
        {
            maskIds.Add(data.id);
            newMask.Set(data);
        }
    }

    public static void CallPause(ControlMaps inputs)
    {
        Instance.callerInputs = inputs;
        Instance.SetPause(true);
    }
    public static void ClosePause()
    {
        Instance.SetPause(false);
    }

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private NewMaskController newMask;
    private bool pauseState;
    private ControlMaps callerInputs;

    public void SetPause(bool state)
    {
        if (state == pauseState) return;
        pauseState = state;

        if (state) callerInputs.Disable();
        else callerInputs.Enable();

        Time.timeScale = state ? 0 : 1;

        pauseMenu.Set(state);
    }
}

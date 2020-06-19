using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI
/// Used in UI to change scene ID
/// </summary>

public class UI_SceneManager : MonoBehaviour
{
    public void LoadSceneById(int id)
    {
        Core.Ins.SceneManager.LoadSceneById(id);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public LevelLoader LevelLoader;

    public void BackToMain()
    {
        LevelLoader.StartGame("MainMenu");   
    }
}

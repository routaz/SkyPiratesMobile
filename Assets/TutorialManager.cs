using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameManager.isTutorialWatched = true;
    }
}

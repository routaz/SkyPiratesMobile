using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairButton : MonoBehaviour
{

    public void RepairButtonPress()
    {
        GameManager.gameManager.RepairButton();
    }
}

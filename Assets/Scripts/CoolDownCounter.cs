using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoolDownCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cooldownText;

    private void Update()
    {
        //Debug.Log(GameManager.gameManager.coolDownLeft);
        cooldownText.text = GameManager.gameManager.coolDownLeft.ToString("hh\\:mm\\:ss");
    }
}

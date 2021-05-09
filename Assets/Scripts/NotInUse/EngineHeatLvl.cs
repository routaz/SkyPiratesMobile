using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class EngineHeatLvl : MonoBehaviour
{
    public float max;
    public float current;
    public float minimum;
    public float engineHeatToInt;
    public Image mask;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //current = GameObject.FindWithTag("Player").GetComponent<PlaneControl>().engineHeat;
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maxOffset = max - minimum;
        float fillAmount = currentOffset / maxOffset;
        mask.fillAmount = fillAmount;
    }
}

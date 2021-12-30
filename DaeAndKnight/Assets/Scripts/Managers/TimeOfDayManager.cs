using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayManager : MonoBehaviour
{
    #region Fields

    [Header("References")]
    public GameManager gameManager;
    public Light sun;
    public Skybox currentBox;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLightSetting(float newIntensity, Material newBox)
    {
        sun.intensity = newIntensity;
        RenderSettings.skybox = newBox;
    }
}

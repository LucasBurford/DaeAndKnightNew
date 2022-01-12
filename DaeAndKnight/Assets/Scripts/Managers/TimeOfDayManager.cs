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
    public GameObject sunMoon;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLightSetting(float newIntensity, Material newBox, Material newMat)
    {
        sun.intensity = newIntensity;
        RenderSettings.skybox = newBox;
        sunMoon.GetComponent<MeshRenderer>().material = newMat;
    }

    public void ChangeObjectSize(Vector3 newSize)
    {
        sunMoon.transform.localScale = newSize;
    }
}

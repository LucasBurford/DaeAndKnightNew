using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public TimeOfDayManager timeOfDayManager;
    public Material dayBox;
    public Material nightBox;
    public Material sunMat;
    public Material moonMat;

    #region Gameplay and spec
    [Header("Gameplay and spec")]
    public float dayIntensity;
    public float nightIntensity;
    public Vector3 sunSize;
    public Vector3 moonSize;

    public TimeOfDay timeOfDay;
    public enum TimeOfDay
    {
        Day,
        Night
    }

    #endregion
    #endregion
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("NewArea");
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeOfDay();
        GetInput();
    }

    private void CheckTimeOfDay()
    {
        switch (timeOfDay)
        {
            case TimeOfDay.Day:
                {
                    timeOfDayManager.ChangeLightSetting(dayIntensity, dayBox, sunMat);
                    timeOfDayManager.ChangeObjectSize(sunSize);
                }
                break;

            case TimeOfDay.Night:
                {
                    timeOfDayManager.ChangeLightSetting(nightIntensity, nightBox, moonMat);
                    timeOfDayManager.ChangeObjectSize(moonSize);
                }
                break;
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (timeOfDay == TimeOfDay.Day)
            {
                timeOfDay = TimeOfDay.Night;
            }
            else if (timeOfDay == TimeOfDay.Night)
            {
                timeOfDay = TimeOfDay.Day;
            }

            // Play cool sound effect
            FindObjectOfType<AudioManager>().Play("TimeOfDayChange");
        }
    }
}

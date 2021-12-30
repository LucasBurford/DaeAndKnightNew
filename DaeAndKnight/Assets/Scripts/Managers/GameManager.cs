using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public TimeOfDayManager timeOfDayManager;

    #region Gameplay and spec
    [Header("Gameplay and spec")]
    public float dayIntensity;
    public float nightIntensity;

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
                    timeOfDayManager.ChangeLightSetting(dayIntensity);
                }
                break;

            case TimeOfDay.Night:
                {
                    timeOfDayManager.ChangeLightSetting(nightIntensity);
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

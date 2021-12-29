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
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (timeOfDay == TimeOfDay.Day)
            {
                timeOfDay = TimeOfDay.Night;

                // Rotate sun around and bring moon out
                timeOfDayManager.ChangeLightSetting(nightIntensity);
            }
            else if (timeOfDay == TimeOfDay.Night)
            {
                timeOfDay = TimeOfDay.Day;

                // Rotate moon around and bring sun out
                timeOfDayManager.ChangeLightSetting(dayIntensity);
            }

            // Play cool sound effect
            FindObjectOfType<AudioManager>().Play("TimeOfDayChange");
        }
    }
}

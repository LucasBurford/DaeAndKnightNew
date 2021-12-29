using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public TimeOfDay timeOfDay;
    public enum TimeOfDay
    {
        Day,
        Night
    }

    #endregion
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (timeOfDay == TimeOfDay.Day)
            {
                timeOfDay = TimeOfDay.Night;

                // Rotate sun around and bring moon out

            }
            else if (timeOfDay == TimeOfDay.Night)
            {
                timeOfDay = TimeOfDay.Day;

                // Rotate moon around and bring sun out

            }

            // Play cool sound effect

        }
    }
}

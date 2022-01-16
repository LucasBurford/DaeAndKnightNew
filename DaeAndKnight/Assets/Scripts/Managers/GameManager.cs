using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public Player player;
    public TimeOfDayManager timeOfDayManager;
    public GameObject castleObject;
    public GameObject darkForestObject;
    public GameObject alchemyLab;
    public Material dayBox;
    public Material nightBox;
    public Material sunMat;
    public Material moonMat;

    #region Gameplay and spec
    [Header("Gameplay and spec")]
    public float dayIntensity;
    public float nightIntensity;

    // Distance between player and object before it is disabled
    public float distanceToBeGreaterThanDarkForest;
    // Current actual distance between player and object during runtime
    public float distanceBetweenPlayerAndDarkForest;    
    
    // Distance between player and object before it is disabled
    public float distanceToBeGreaterThanCastle;
    // Current actual distance between player and object during runtime
    public float distanceBetweenPlayerAndCastle;

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
        //CheckDistanceToAreas();
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

    private void CheckDistanceToAreas()
    {
        if (darkForestObject.activeInHierarchy)
        {
             distanceBetweenPlayerAndDarkForest = Vector3.Distance(player.transform.position, darkForestObject.transform.position);
        }
        else
        {
            distanceBetweenPlayerAndDarkForest = 0;
        }

        if (castleObject.activeInHierarchy)
        {
            distanceBetweenPlayerAndCastle = Vector3.Distance(player.transform.position, castleObject.transform.position);
        }
        else
        {
            distanceBetweenPlayerAndCastle = 0;
        }

        if (distanceBetweenPlayerAndDarkForest > distanceToBeGreaterThanDarkForest)
        {
            Areas(darkForestObject, false);
        }
        if (distanceBetweenPlayerAndDarkForest < distanceToBeGreaterThanDarkForest)
        {
            Areas(darkForestObject, true);
        }

        if (distanceBetweenPlayerAndCastle > distanceToBeGreaterThanCastle)
        {
            Areas(castleObject, false);
        }
        else
        {
            Areas(castleObject, true);
        }
    }

    private void Areas(GameObject area, bool active)
    {
        area.SetActive(active);
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

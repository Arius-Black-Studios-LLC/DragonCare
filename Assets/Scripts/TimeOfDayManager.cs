using Pinwheel.Jupiter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayManager : MonoBehaviour
{

    public JDayNightCycle dayNightCycle;

    private void Start()
    {
        if(dayNightCycle == null)
        {
            Debug.LogError("DAYNIGHTCYCLE MISSING");
        }


    }

    private void Update()
    {
        SunRoation();
    }

    private void SunRoation()
    {



        System.DateTime currenTime = System.DateTime.Now;

        dayNightCycle.Time = currenTime.Hour;

    }

    private float CalculateSunAngle(System.DateTime time)
    {
        float hours = time.Hour;
        float minutes = time.Minute;
        float seconds = time.Second;

        float totalMinutes = hours * 60 + minutes * seconds/60;
        return (totalMinutes / 1440f) * 360;
    }


}

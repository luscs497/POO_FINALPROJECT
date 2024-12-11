using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : Buildings
{
    public DayNightCycle day;
    void Update()
    {
        if(GetIsPlayerInRange())
        {
            InteractWith();
        }
    }

    public override void InteractWith(){
        if (Input.GetKeyDown(KeyCode.Q))
        {
            base.DestroyObject(gameObject);
        }else if (Input.GetKeyDown(KeyCode.Z) && (day.GetTimeOfDay() <= 0.5))
        {
            day.SetAux(true);
            day.SetTimeOfDay(0.6f);
        }
    }

}

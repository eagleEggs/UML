using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// track time within steps regardless of timescale within UML:

public class yourAcademy : Academy
{

	public float theTime=0f;

    public override void AcademyReset()
    {
    	theTime=theTime+Time.deltaTime;
    }

    public override void AcademyStep()
    {
    	theTime=theTime+Time.deltaTime;
    	Debug.Log(theTime);
    }
}

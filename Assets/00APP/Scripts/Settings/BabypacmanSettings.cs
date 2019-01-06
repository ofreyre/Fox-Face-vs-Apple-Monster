using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BabypacmanSettings : ScriptableObject {

    public float lapseStart = 10;
    public float lapseMin = 15;
    public float lapseIncPerSecond = 0.01f;
    public Vector3 velocity = new Vector3(0,-1,0);
    public float marginLeft = 0.5f;
    public float marginRight = -0.5f;
    public float marginTop = 0.5f;
    public float marginBottom = -0.5f;
}

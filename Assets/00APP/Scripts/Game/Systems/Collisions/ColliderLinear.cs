using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLinear : MonoBehaviour, ICollider {
    public float limitX;
    public float limitY;
    public float ramge;

    public float LimitX { get { return limitX; } }
}

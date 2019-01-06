using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils {

    public static Vector3 Range(Vector3 v0, Vector3 v1) {
        return new Vector3(Random.Range(v0.x, v1.x), Random.Range(v0.y, v1.y), Random.Range(v0.z, v1.z));
    }

    public static Color Range(Color v0, Color v1)
    {
        return new Color(Random.Range(v0.r, v1.r), Random.Range(v0.g, v1.g), Random.Range(v0.b, v1.b), Random.Range(v0.a, v1.a));
    }
}

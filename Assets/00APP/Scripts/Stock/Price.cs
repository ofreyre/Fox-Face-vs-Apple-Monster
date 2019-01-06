using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Price : MonoBehaviour {
    public int coins;

    void Start()
    {
        transform.Find("Text").GetComponent<Text>().text = coins.ToString();
    }
}

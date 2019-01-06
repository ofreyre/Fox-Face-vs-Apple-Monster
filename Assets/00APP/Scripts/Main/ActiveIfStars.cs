using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveIfStars : MonoBehaviour
{
    void Start()
    {
        //gameObject.SetActive(DBmanager.Stars - DBmanager.Upgrades.TotalStars > 0);
        if (DBmanager.Stars - DBmanager.Upgrades.TotalStars < 1)
        {
            Destroy(gameObject);
        }
    }
}

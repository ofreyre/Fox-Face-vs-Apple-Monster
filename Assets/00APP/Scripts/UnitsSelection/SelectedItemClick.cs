using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemClick : MonoBehaviour
{
    public void OnClick()
    {
        ItemsEvents.DispatchSelectedItemClick(gameObject);
    }
}

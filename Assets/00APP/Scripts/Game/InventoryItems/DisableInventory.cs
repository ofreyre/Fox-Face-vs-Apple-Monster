using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInventory : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameEvents.instance.FlowEvent += OnFlowEvent;
    }

    void OnFlowEvent(FLOWEVENTTYPE type)
    {
        switch (type)
        {
            case FLOWEVENTTYPE.win:
            case FLOWEVENTTYPE.lose:
                Disable();
                break;
        }
    }

    // Update is called once per frame
    void Disable() {
        gameObject.SetActive(false);
	}
}

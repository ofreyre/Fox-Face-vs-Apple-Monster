using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FLOWEVENTTYPE
{
    start,
    hordestart,
    hordelast,
    hordeend,
    win,
    lose
}

public class GameEvents : MonoBehaviour {
    public static GameEvents instance;

    //Tile map clicked
    public delegate void DelegateMapClicked(Vector2 screenpos);
    public event DelegateMapClicked MapClicked;

    public delegate void DelegateClickToRemove(Vector2 screenpos);
    public event DelegateClickToRemove ClickToRemove;

    public delegate void DelegateRemoveClicked();
    public event DelegateRemoveClicked RemoveClicked;

    //Unit added to map
    public delegate void DelegateUnitAdded(int cellX, int cellY);
    public event DelegateUnitAdded UnitAdded;

    public delegate void DelegateAttackerKilled(Vector3 pos, ATTACKERTYPE type);
    public event DelegateAttackerKilled AttackerKilled;

    public delegate void DelegateAttackerArrive(int row);
    public event DelegateAttackerArrive AttackerArrive;

    //public delegate void DelegateAttackerEnd(Transform attacker, int row);
    //public event DelegateAttackerEnd AttackerEnd;

    public delegate void DelegateBabyPacmanCollected();
    public event DelegateBabyPacmanCollected BabyPacmanCollected;

    public delegate void DelegateFlowEvent(FLOWEVENTTYPE type);
    public event DelegateFlowEvent FlowEvent;

    public delegate void DelegateInventoryClicked();
    public event DelegateInventoryClicked InventoryClicked;

    public delegate void DelegateUnitItemRecovered(UNITTYPE type);
    public event DelegateUnitItemRecovered UnitItemRecovered;

    void Awake()
    {
        instance = this;
    }

    public static void DispatchMapClicked(Vector2 screenpos)
    {
        if (instance.MapClicked != null)
        {
            instance.MapClicked(screenpos);
        }
    }

    public static void DispatchRemoveClicked()
    {
        if (instance.RemoveClicked != null)
        {
            instance.RemoveClicked();
        }
    }

    public static void DispatchClickToRemove(Vector2 screenpos)
    {
        if (instance.ClickToRemove != null)
        {
            instance.ClickToRemove(screenpos);
        }
    }

    public static void DispatchInventoryClicked()
    {
        if (instance.InventoryClicked != null)
        {
            instance.InventoryClicked();
        }
    }

    public static void DispatchUnitAdded(int cellX, int cellY)
    {
        if (instance.UnitAdded != null)
        {
            instance.UnitAdded(cellX, cellY);
        }
    }

    public static void DispatchAttackerKilled(Vector3 pos, ATTACKERTYPE type)
    {
        if (instance.AttackerKilled != null)
        {
            instance.AttackerKilled(pos, type);
        }
    }

    public static void DispatchAttackerArrive(int row)
    {
        if (instance.AttackerArrive != null)
        {
            instance.AttackerArrive(row);
        }
    }

    /*public static void DispatchAttackerEnd(Transform attacker, int row)
    {
        if (instance.AttackerEnd != null)
        {
            instance.AttackerEnd(attacker, row);
        }
    }*/

    public static void DispatchBabyPacmanCollected()
    {
        if (instance.BabyPacmanCollected != null)
        {
            instance.BabyPacmanCollected();
        }
    }

    public static void DispatchFlowEvent(FLOWEVENTTYPE type)
    {
        if (instance.FlowEvent != null)
        {
            instance.FlowEvent(type);
        }
    }

    public static void DispatchUnitItemRecovered(UNITTYPE type)
    {
        if (instance.UnitItemRecovered != null)
        {
            instance.UnitItemRecovered(type);
        }
    }
}

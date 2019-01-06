using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionFlow : MonoBehaviour {

    public UnitsBankController m_bankController;
    public SelectedUnitsController m_selectionController;
    public SpritesOrderManager m_spriteOrders;
    public UnitsDisplay m_unitsDisplay;

    // Use this for initialization
    void Start () {
        m_bankController.Init();
        m_selectionController.Init();
        m_spriteOrders.Init();
        m_unitsDisplay.Init();
    }
}

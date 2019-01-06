using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct PowerUpFeedback
{
    public INVENTORYITEM_ITEM_CATEGORY category;
    public Color color;
    public float duration;

    public PowerUpFeedback(INVENTORYITEM_ITEM_CATEGORY category, Color color, float duration)
    {
        this.category = category;
        this.color = color;
        this.duration = duration;
    }

    public static PowerUpFeedback NONE = new PowerUpFeedback(INVENTORYITEM_ITEM_CATEGORY.none, Color.white, 0);
}

public class InventoryConsumer : MonoBehaviour {

    public Image m_powerUpFeedbackUI;
    public PowerUpFeedback[] m_powerUpFeedback;

    ApplyInventoryItemAir m_applyAir;
    ApplyInventoryItemBabypacman m_applyBabypacman;
    ApplyInventoryItemFire m_applyFire;
    ApplyInventoryItemHit m_applyHit;
    ApplyInventoryItemIce m_applyIce;
    ApplyInventoryItemImmortality m_applyImmortality;
    ApplyInventoryItemSpeedup m_applySpeedup;
    ApplyInventoryItemFreeze m_applyFreeze;

    Coroutine m_Feedback;

    // Use this for initialization
    void Start () {
        InventoryItemsEvents.instance.ItemConsumed += Consume;
        m_applyAir = GetComponent<ApplyInventoryItemAir>();
        m_applyBabypacman = GetComponent<ApplyInventoryItemBabypacman>();
        m_applyFire = GetComponent<ApplyInventoryItemFire>();
        m_applyHit = GetComponent<ApplyInventoryItemHit>();
        m_applyIce = GetComponent<ApplyInventoryItemIce>();
        m_applyImmortality = GetComponent<ApplyInventoryItemImmortality>();
        m_applySpeedup = GetComponent<ApplyInventoryItemSpeedup>();
        m_applyFreeze = GetComponent<ApplyInventoryItemFreeze>();
    }

    void Consume(INVENTORYITEM_ITEM_CATEGORY category, float amount) {
        switch (category)
        {
            case INVENTORYITEM_ITEM_CATEGORY.immortality:
                m_applyImmortality.Apply(amount);
                break;
            case INVENTORYITEM_ITEM_CATEGORY.freeze:
                m_applyFreeze.Apply(amount);
                break;
            case INVENTORYITEM_ITEM_CATEGORY.damage_hit:
                m_applyHit.Apply(amount);
                break;
            case INVENTORYITEM_ITEM_CATEGORY.damage_ice:
                m_applyIce.Apply(amount);
                break;
            case INVENTORYITEM_ITEM_CATEGORY.damage_fire:
                m_applyFire.Apply(amount);
                break;
            case INVENTORYITEM_ITEM_CATEGORY.damage_air:
                m_applyAir.Apply(amount);
                break;
            case INVENTORYITEM_ITEM_CATEGORY.character_speedup:
                m_applySpeedup.Apply(amount);
                break;
            case INVENTORYITEM_ITEM_CATEGORY.babypacman:
                m_applyBabypacman.Apply(amount);
                break;
            default:
                break;
        }

    }

    void CallFeedback(INVENTORYITEM_ITEM_CATEGORY category)
    {
        PowerUpFeedback feedback = GetFeedback(category);
        if (feedback.category != INVENTORYITEM_ITEM_CATEGORY.none)
        {
            if (m_Feedback != null)
            {
                StopCoroutine(m_Feedback);
            }
            m_Feedback = StartCoroutine(Feedback(feedback));
        }
    }

    PowerUpFeedback GetFeedback(INVENTORYITEM_ITEM_CATEGORY category) {
        PowerUpFeedback feedback;
        for (int i = 0, n = m_powerUpFeedback.Length; i < n; i++)
        {
            feedback = m_powerUpFeedback[i];
            if (feedback.category == category)
            {
                return feedback;
            }
        }
        return PowerUpFeedback.NONE;
    }

    IEnumerator Feedback(PowerUpFeedback feedback)
    {
        Debug.Log("Feedback ");
        float t = Time.time + feedback.duration;
        float k = 1/ feedback.duration;
        Color c0 = feedback.color;
        m_powerUpFeedbackUI.gameObject.SetActive(true);
        while (t > Time.time)
        {
            k = (t - Time.time) * k;
            m_powerUpFeedbackUI.color = new Color(c0.r, c0.g, c0.b, c0.a * k);
            yield return null;
        }
        m_powerUpFeedbackUI.color = new Color(c0.r, c0.g, c0.b, 0);
        m_powerUpFeedbackUI.gameObject.SetActive(false);
    }
}

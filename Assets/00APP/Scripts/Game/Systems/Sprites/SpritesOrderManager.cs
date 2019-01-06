using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ORDERSUPERGROUPTYPE
{
    units,
    attackers,
    bullets,
    explosions
}

public enum ORDERGROUPTYPE
{
    units,
    attackers,
    bullets,
    explosions
}

[Serializable]
public struct OrderGroup
{
    public ORDERGROUPTYPE orderGroup;
    public int size;

    public OrderGroup(ORDERGROUPTYPE orderGroup, int size)
    {
        this.orderGroup = orderGroup;
        this.size = size;
    }
}

public struct SortLayer
{
    public int orderMin;
    public int orderMax;
    public int order;

    public SortLayer(int orderMin, int orderMax, int order)
    {
        this.orderMin = orderMin;
        this.orderMax = orderMax;
        this.order = order;
    }
}

public struct ORDERGROUPTYPEcomparer : IEqualityComparer<ORDERGROUPTYPE>
{
    public bool Equals(ORDERGROUPTYPE x, ORDERGROUPTYPE y)
    {
        return x == y;
    }

    public int GetHashCode(ORDERGROUPTYPE obj)
    {
        // you need to do some thinking here,
        return (int)obj;
    }
}

public class SpritesOrderManager : MonoBehaviour {
    public static SpritesOrderManager instance;

    public SpriteOrders m_orders;
    public MapSettings m_map;
    public int m_cellsY = 6;
    Dictionary<ORDERGROUPTYPE, SortLayer>[] m_rows;
    Dictionary<ORDERGROUPTYPE, SortLayer> m_localGroups;

    void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        int cellsY = m_map != null ? m_map.cellsY : m_cellsY;
        m_rows = new Dictionary<ORDERGROUPTYPE, SortLayer>[cellsY];
        OrderGroup orderGroup;
        int order = 0;
        ORDERGROUPTYPEcomparer comparer = new ORDERGROUPTYPEcomparer();
        for (int row = 0; row < cellsY; row++)
        {
            m_localGroups = new Dictionary<ORDERGROUPTYPE, SortLayer>(comparer);
            for (int groupI = 0, groupN = m_orders.row.Length; groupI < groupN; groupI++)
            {
                orderGroup = m_orders.row[groupI];
                m_localGroups.Add(orderGroup.orderGroup, new SortLayer(order, order + orderGroup.size - 1, 0));
                order += orderGroup.size;
            }
            m_rows[cellsY - 1 - row] = m_localGroups;
        }
    }

    public void SetOrder(int row, ORDERGROUPTYPE orderGroup, SpriteRenderer[] sprites)
    {
        int n = sprites.Length;
        m_localGroups = m_rows[row];
        SortLayer sortLayer = m_localGroups[orderGroup];
        int order = sortLayer.orderMin + sortLayer.order;
        if (order + n > sortLayer.orderMax)
        {
            order = sortLayer.orderMin;
        }
        for (int i=0; i < n; i++)
        {
            sprites[i].sortingOrder = order + i;
        }
        sortLayer.order += n;
        m_localGroups[orderGroup] = sortLayer;
    }
}

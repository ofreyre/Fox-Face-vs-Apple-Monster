using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Defender
{
    public Transform transform;
    public ColliderLinear collider;

    public Defender(Transform transform, ColliderLinear collider)
    {
        this.transform = transform;
        this.collider = collider;
    }
}

public class Map : MonoBehaviour
{
    public static Map instance;
    [HideInInspector]
    public MapSettings m_settings;
    public float m_hordeSpawnDistance = 1;

    [HideInInspector]
    public int m_cellsX, m_cellsY;
    public Vector3 m_cellL;
    public Vector3 m_origin;
    [HideInInspector]
    public Vector3 m_cellPivot;
    [HideInInspector]
    public Defender[,] m_defenders;
    //public UnitState[,] m_states;
    public UNITTYPE[,] m_filled;
    GameObject m_unitInCell;
    GameObject m_localGameObject;
    List<int> m_rows;
    public float m_attackerSpawnX;
    [HideInInspector]
    public float m_attackerArriveX;
    public float m_attackerEndXfromMap = 2;
    [HideInInspector]
    public float m_bulletEndX;
    [HideInInspector]
    public float m_attackerEndX;
    float screen2world;
    Vector3 m_cameraLeftBottom;
    public Vector4 m_mapRect;

    public Transform m_bananasHorizontal;
    public Transform m_bananasVertical;

    Transform m_localTransform;

    // Use this for initialization

    public void Init()
    {
        GameEvents.instance.ClickToRemove += RemoveUnitImmediateWithCheck;

        instance = this;
        m_cellsX = m_settings.cellsX;
        m_cellsY = m_settings.cellsY;
        Sprite sprite = m_settings.sprite;
        float pixelsPerUnit = sprite.pixelsPerUnit;
        m_cellL = new Vector3(sprite.rect.width, sprite.rect.height, 0) / pixelsPerUnit;
        m_cellPivot = new Vector3(m_cellL.x * m_settings.cellPivot.x, m_cellL.y * m_settings.cellPivot.y, 0);
        Camera cam = Camera.main;
        //float height = 2f * cam.orthographicSize;
        //float width = height * cam.aspect;
        //Vector3 worldSizeHalf = cam.ScreenToWorldPoint(new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize));
        //worldSizeHalf = new Vector3(Mathf.Abs(worldSizeHalf.x), Mathf.Abs(worldSizeHalf.y), 0);
        Vector3 worldSizeHalf = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);

        float leftFromRight = cam.transform.position.x + worldSizeHalf.x - m_settings.rightBottomMargin.x - m_cellL.x * m_cellsX;

        float x0 = Mathf.Min(leftFromRight - m_attackerEndXfromMap, cam.transform.position.x - worldSizeHalf.x + m_settings.leftTopMarginMin.x);

        if (cam.transform.position.x - worldSizeHalf.x > x0)
        {
            float w = -x0;
            cam.orthographicSize = w / cam.aspect;
            //worldSizeHalf = cam.ScreenToWorldPoint(new Vector2(cam.orthographicSize, cam.orthographicSize * cam.aspect));
            worldSizeHalf = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);
            leftFromRight = cam.transform.position.x + worldSizeHalf.x - m_settings.rightBottomMargin.x - m_cellL.x * m_cellsX;
        }

        m_cameraLeftBottom = new Vector3(cam.transform.position.x - worldSizeHalf.x, cam.transform.position.y - worldSizeHalf.y, 0);

        float y = Mathf.Max(cam.transform.position.y + worldSizeHalf.y - m_settings.leftTopMarginMin.y - m_cellL.y * m_cellsY, cam.transform.position.y - worldSizeHalf.y + m_settings.rightBottomMargin.y);
        GameObject powerUps = GameObject.Find("InventoryItems");
        if (powerUps != null)
        {
            //float yMax = cam.ScreenToWorldPoint(new Vector3(0, powerUps.GetComponent<RectTransform>().rect.yMax, 1)).y;
            //float yMin = cam.ScreenToWorldPoint(new Vector3(0, powerUps.GetComponent<RectTransform>().rect.yMin, 1)).y;

            Vector3 vMax, vMin;
            RectTransform rectTransform = powerUps.GetComponent<RectTransform>();
            RectTransform rectContainer = rectTransform.parent.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectContainer, new Vector2(0, rectTransform.rect.height), cam, out vMax);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectContainer, new Vector2(0, rectTransform.rect.yMin), cam, out vMin);
            Vector3 v;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectContainer, new Vector2(rectTransform.localPosition.x, rectTransform.localPosition.y), cam, out v);

            //y = Mathf.Min(cam.transform.position.y - height * 0.5f + yMax - yMin, y);
            y = cam.transform.position.y - worldSizeHalf.y + 0.8f;
        }

        m_origin = new Vector3(leftFromRight, y, 0);


        m_attackerSpawnX = cam.transform.position.x + worldSizeHalf.x - m_origin.x + m_hordeSpawnDistance;
        m_attackerArriveX = m_origin.x;
        //m_attackerEndX = cam.transform.position.x - width * 0.5f;
        m_attackerEndX = m_origin.x - m_attackerEndXfromMap;
        m_bulletEndX = cam.transform.position.x + worldSizeHalf.x;

        m_defenders = new Defender[m_cellsX, m_cellsY];
        //m_states = new UnitState[m_cellsX, m_cellsY];
        m_filled = new UNITTYPE[m_cellsX, m_cellsY];

        m_rows = new List<int>();

        m_mapRect = mapRect;

        Display();
    }

    void Display()
    {
        GameObject go = new GameObject();
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = m_settings.sprite;
        renderer.drawMode = SpriteDrawMode.Tiled;
        renderer.size = new Vector2(m_cellL.x * m_cellsX, m_cellL.y * m_cellsY);
        go.transform.position = m_origin;

        go = new GameObject();
        renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = m_settings.spriteTop;
        renderer.drawMode = SpriteDrawMode.Tiled;
        renderer.size = new Vector2(m_cellL.x * m_cellsX, renderer.sprite.rect.height / renderer.sprite.pixelsPerUnit);
        go.transform.position = m_origin + new Vector3(0, m_cellL.y * m_cellsY);
        

        m_bananasHorizontal.position = new Vector3(m_attackerEndX, mapRectWorld.w, 0);
        m_bananasVertical.position = new Vector3(m_attackerEndX, m_cameraLeftBottom.y, 0);
    }

    public bool AddUnit(Vector2Int grid, GameObject gobj)
    {
        gobj.transform.position = new Vector3(m_origin.x + grid.x * m_cellL.x, m_origin.y + grid.y * m_cellL.y, 0) + m_cellPivot;
        m_defenders[grid.x, grid.y] = new Defender(gobj.transform, gobj.GetComponent<ColliderLinear>());
        //m_states[grid.x, grid.y] = gobj.GetComponent<UnitState>();
        m_filled[grid.x, grid.y] = gobj.GetComponent<UnitType>().type;
        GameEvents.DispatchUnitAdded(grid.x, grid.y);
        return true;
    }

    public void RemoveUnit(Vector2 world)
    {
        Vector2Int ij = World2Grid(world);
        m_filled[ij.x, ij.y] = UNITTYPE.none;
    }

    public void RemoveUnitImmediate(Vector3 world)
    {
        Vector2Int ij = World2Grid(world);
        m_filled[ij.x, ij.y] = UNITTYPE.none;
        m_defenders[ij.x, ij.y].transform.gameObject.SetActive(false);
    }

    public void RemoveUnitImmediateWithCheck(Vector2 screenpos)
    {
        if (m_mapRect.x < screenpos.x && screenpos.x < m_mapRect.z && m_mapRect.y < screenpos.y && screenpos.y < m_mapRect.w)
        {
            Vector2Int grid = World2Grid(Screen2World(screenpos));
            if (m_filled[grid.x, grid.y] != UNITTYPE.none)
            {
                m_filled[grid.x, grid.y] = UNITTYPE.none;
                m_defenders[grid.x, grid.y].transform.gameObject.SetActive(false);
            }
        }
    }

    public void RemoveUnit(int i, int j)
    {
        m_filled[i, j] = UNITTYPE.none;
    }

    public void RemoveUnitImmediate(int i, int j)
    {
        m_filled[i, j] = UNITTYPE.none;
        m_defenders[i, j].transform.gameObject.SetActive(false);
    }

    public void KillUnit(int i, int j)
    {
        GameAudioPlayer.instance.PlayPacmanDie();
        AnimatorController.instance.Die(m_defenders[i, j].transform.GetComponent<Animator>());
        m_filled[i, j] = UNITTYPE.none;
    }

    public void DamageUnitsRange(Vector3 position, float range, float hit)
    {
        Defender defender;
        float x0 = position.x - range;
        float x1 = position.x + range;
        int j = y2j(position.y);
        int i = x2i(x0);
        if (i < 0)
        {
            i = 0;
        }
        for (int i1 = x2i(x1) + 1; i < i1; i++)
        {
            if (m_filled[i, j] != UNITTYPE.none)
            {
                defender = m_defenders[i, j];
                if (defender.transform.position.x + defender.collider.ramge > x0 || defender.transform.position.x - defender.collider.ramge < x1)
                {
                    if (!defender.transform.GetComponent<UnitState>().Hit(hit))
                    {
                        KillUnit(i, j);
                    }
                }
            }
        }
    }

    public void ResetHordeRowRND()
    {
        m_rows.Clear();
    }

    public int GetHordeRowRND()
    {
        int i = 0, n = m_rows.Count;
        if (n == 0)
        {
            n = m_cellsY;
            for (; i < n; i++)
            {
                m_rows.Add(i);
            }
        }
        i = Random.Range(0, n);
        n = m_rows[i];
        m_rows.RemoveAt(i);
        return n;
    }

    public Vector3 GetHordePositionAtRow(int j)
    {
        return m_origin + new Vector3(m_attackerSpawnX, j * m_cellL.y,0) + m_cellPivot;
    }

    public Transform IJ_2_GameObject(int i, int j)
    {
        return m_defenders[i, j].transform;
    }

    public Vector2 Screen2World(Vector2 screenpos)
    {
        return new Vector2(screenpos.x * screen2world + m_cameraLeftBottom.x, screenpos.y * screen2world + m_cameraLeftBottom.y);
    }

    public Vector2Int World2Grid(Vector2 world)
    {
        int i = (int)((world.x - m_origin.x) / m_cellL.x);
        int j = (int)((world.y - m_origin.y) / m_cellL.y);
        return new Vector2Int(i, j);
    }

    public Vector2Int World2Grid(Vector3 world)
    {
        int i = (int)((world.x - m_origin.x) / m_cellL.x);
        int j = (int)((world.y - m_origin.y) / m_cellL.y);
        return new Vector2Int(i, j);
    }

    public int World2GridJ(float y)
    {
        return (int)((y - m_origin.y) / m_cellL.y);
    }

    public float GetRowSpawnY(int i)
    {
        return m_origin.y + i * m_cellL.y + m_cellPivot.y;
    }

    public Vector3 ij2xy(int i, int j)
    {
        return m_origin + new Vector3(i * m_cellL.x, j * m_cellL.y, 0) + m_cellPivot;
    }

    public float i2x(int i)
    {
        return m_origin.x + i * m_cellL.x;
    }

    public float j2y(int j)
    {
        return m_origin.y + j * m_cellL.y;
    }

    public float GetRowPivotY(int row)
    {
        return m_origin.y + m_cellL.y * row + m_cellPivot.y;
    }

    public float GetUnitX(int i, int j)
    {
        return m_defenders[i, j].transform.position.x;
    }

    public float GetUnitLimit(int i, int j)
    {
        if (m_filled[i, j] != UNITTYPE.none)
        {
            Defender defender = m_defenders[i, j];
            return defender.transform.position.x + defender.collider.limitX;
        }
        return m_attackerEndX;
    }

    public int x2i(float x)
    {
        int i = (int)((x - m_origin.x) / m_cellL.x);
        return i<m_cellsX?i: m_cellsX - 1;
    }

    public int y2j(float y)
    {
        return (int)((y - m_origin.y) / m_cellL.y);
    }

    public float i2mapX(int i)
    {
        return i * m_cellL.x;
    }

    public float i2mapX(float i)
    {
        return i * m_cellL.x;
    }

    public UNITTYPE x2filled(int i, int j)
    {
        return m_filled[i, j];
    }

    public int cellsCount { get { return m_cellsX * m_cellsY; } }

    public Vector2 cellL
    {
        get
        {
            Sprite sprite = m_settings.sprite;
            float pixelsPerUnit = sprite.pixelsPerUnit;
            return new Vector3(sprite.rect.width, sprite.rect.height) / pixelsPerUnit;
        }
    }

    //(x,y,z,w) == (left, bottom, right, top)
    public Vector4 mapRect
    {
        get
        {
            Vector2 _cellL = cellL;
            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            screen2world = height / Screen.height;
            return new Vector4(m_origin.x - m_cameraLeftBottom.x, m_origin.y - m_cameraLeftBottom.y, m_origin.x - m_cameraLeftBottom.x + _cellL.x * m_settings.cellsX, m_origin.y + _cellL.y * m_settings.cellsY - m_cameraLeftBottom.y) / screen2world;
        }
    }

    public Vector4 mapRectWorld
    {
        get
        {
            Vector2 _cellL = cellL;
            return new Vector4(m_origin.x, m_origin.y, m_origin.x + _cellL.x * m_settings.cellsX, m_origin.y + _cellL.y * m_settings.cellsY);
        }
    }
}

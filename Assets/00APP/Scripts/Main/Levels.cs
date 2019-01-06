using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour {
    public GlobalFlow m_flow;
    public GameObject[] m_levels;
    public GameObject m_minionPlace;
    public GameObject m_minionLast;
    public GameObject m_pacmanLast;
    public Transform m_levelsContainer;
    public ScrollRect m_scroll;

    // Use this for initialization
    void Start () {
        m_flow.toPlay = false;
        DBlevels levels = DBmanager.Levels;
        int stars;
        GameObject pacmanPlace;
        m_minionPlace.SetActive(false);
        for (int i = 0, n = levels.stars.Length - 1; i < n; i++)
        {
            stars = levels.stars[i];
            if (stars > 0)
            {
                pacmanPlace = m_levels[i];
                AddDelegateToButton(pacmanPlace.GetComponent<Button>(), i);
                pacmanPlace.transform.Find("star2").gameObject.SetActive(stars > 1);
                pacmanPlace.transform.Find("star3").gameObject.SetActive(stars > 2);
                pacmanPlace.transform.Find("level").GetComponent<Text>().text = (i + 1).ToString();
            }
            else if (i > 0)
            {
                m_levels[i].SetActive(false);
                if (levels.stars[i - 1] > 0)
                {
                    m_minionPlace.transform.localPosition = m_levels[i].transform.localPosition;
                    AddMinionPlaceListener(i);
                    m_minionPlace.SetActive(true);
                }
            }
            else
            {
                m_levels[i].SetActive(false);
                m_minionPlace.transform.localPosition = m_levels[i].transform.localPosition;
                AddMinionPlaceListener(i);
                m_minionPlace.SetActive(true);
            }
        }

        int lastStars = levels.stars[levels.stars.Length - 1];
        if (lastStars > 0)
        {
            m_minionLast.SetActive(false);
            m_pacmanLast.SetActive(true);
            m_pacmanLast.transform.Find("star2").gameObject.SetActive(lastStars > 1);
            m_pacmanLast.transform.Find("star3").gameObject.SetActive(lastStars > 2);
        }
        else if (levels.stars[levels.stars.Length - 2] > 0)
        {
            m_minionLast.GetComponent<Button>().interactable = true;
        }
        else
        {
            m_minionLast.GetComponent<Button>().interactable = false;
        }

        m_scroll.normalizedPosition = DBmanager.mainScrollPosition;
    }

    void AddDelegateToButton(Button button, int level)
    {
        button.onClick.AddListener(delegate { LoadLevel(level); });
    }

    void AddMinionPlaceListener(int level)
    {
        m_minionPlace.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(level); });
    }

    public void LoadLevel(int level)
    {
        m_flow.toPlay = true;
        m_flow.level = level;
        m_flow.ToScene("ItemsStore");
    }

    public void SaveScrollPosition()
    {
        DBmanager.mainScrollPosition = new Vector2(m_scroll.normalizedPosition.x, m_scroll.normalizedPosition.y);
    }
}

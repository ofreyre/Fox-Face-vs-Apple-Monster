using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hordes : ScriptableObject {
    public Horde[] hordes;

    public Dictionary<ATTACKERTYPE, GameObject> GetAttackerType()
    {
        HordeSpawnMoment[] momments;
        Dictionary<ATTACKERTYPE, GameObject> attackers = new Dictionary<ATTACKERTYPE, GameObject>(AttackerType.AttackerTypeComparer);
        GameObject[] prefabs;
        ATTACKERTYPE type;
        GameObject gobj;
        for (int i = 0, n = hordes.Length; i < n; i++)
        {
            momments = hordes[i].momments;
            for (int j = 0, m = momments.Length; j < m; j++)
            {
                prefabs = momments[j].prefabs;
                for (int k = 0, p = prefabs.Length; k < p; k++)
                {
                    gobj = prefabs[k];
                    type = gobj.GetComponent<AttackerType>().type;
                    if(!attackers.ContainsKey(type))
                    {
                        attackers.Add(type, gobj);
                    }
                }
            }
        }
        return attackers;
    }

    public List<GameObject> Prefabs
    {
        get
        {
            HordeSpawnMoment[] momments;
            List<GameObject> prefs = new List<GameObject>();
            GameObject[] prefabs;
            List<ATTACKERTYPE> types = new List<ATTACKERTYPE>();
            ATTACKERTYPE type;
            GameObject prefab;
            for (int i = 0, n = hordes.Length; i < n; i++)
            {
                momments = hordes[i].momments;
                for (int j = 0, m = momments.Length; j < m; j++)
                {
                    prefabs = momments[j].prefabs;
                    for (int k = 0, p = prefabs.Length; k < p; k++)
                    {
                        prefab = prefabs[k];
                        type = prefab.GetComponent<AttackerType>().type;
                        if (!types.Contains(type))
                        {
                            types.Add(type);
                            prefs.Add(prefab);
                        }
                    }
                }
            }
            return prefs;
        }
    }

    public Dictionary<ATTACKERTYPE, int> Points
    {
        get
        {
            HordeSpawnMoment[] momments;
            Dictionary<ATTACKERTYPE, int> points = new Dictionary<ATTACKERTYPE, int>();
            ATTACKERTYPE type;
            GameObject[] prefabs;
            GameObject prefab;

            for (int i = 0, n = hordes.Length; i < n; i++)
            {
                momments = hordes[i].momments;
                for (int j = 0, m = momments.Length; j < m; j++)
                {
                    prefabs = momments[j].prefabs;
                    for (int k = 0, p = prefabs.Length; k < p; k++)
                    {
                        prefab = prefabs[k];
                        type = prefab.GetComponent<AttackerType>().type;
                        if (!points.ContainsKey(type))
                        {
                            points.Add(type, prefab.GetComponent<AttackerScore>().points);
                        }
                    }
                }
            }
            return points;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATTACKERTYPE
{
    carro_hielero,
    carro_hielero_strong,
    cuasimodo0,
    escalera_lento,
    eskiador,
    eskiador_rapido,
    fire_lento_alta_baja,
    fire_lento_alta_normal,
    fire_lento_baja_baja,
    fire_lento_baja_normal,
    fire_lento_normal_baja,
    fire_lento_normal_normal,
    fire_normal_alta_baja,
    fire_normal_alta_normal,
    fire_normal_baja_baja,
    fire_normal_baja_normal,
    fire_normal_normal_baja,
    fire_normal_normal_normal,
    futbol_americano0,
    futbol_americano1,
    futbol_americano2,
    garrocha,
    ice_baja_alta_baja,
    ice_baja_baja_baja,
    ice_baja_normal_baja,
    ice_normal_alta_alto,
    ice_normal_alta_baja,
    ice_normal_alta_normal,
    ice_normal_lento_alto_100,
    ice_normal_lento_baja_100,
    ice_normal_lento_normal_100,
    ice_normal_normal_alta_100,
    ice_normal_normal_baja_100,
    ice_normal_normal_normal_100,
    malla_protectora,
    normal_lento,
    normal_lento_alta,
    normal_lento_normal,
    normal_normal,
    normal_normal_alta,
    normal_normal_normal,
    saltarin,
    spawner,
    viejo_bomba0,
    viejo_bomba0_alta,
    viejo_bomba0_normal,
    viejo_bomba1,
    viejo_bomba1_alta,
    viejo_bomba1_normal,
    none
}

public struct ATTACKERTYPEcomparer : IEqualityComparer<ATTACKERTYPE>
{
    public bool Equals(ATTACKERTYPE x, ATTACKERTYPE y)
    {
        return x == y;
    }

    public int GetHashCode(ATTACKERTYPE obj)
    {
        // you need to do some thinking here,
        return (int)obj;
    }
}

public class AttackerType : MonoBehaviour
{
    public static ATTACKERTYPEcomparer AttackerTypeComparer = new ATTACKERTYPEcomparer();

    public ATTACKERTYPE type;
}

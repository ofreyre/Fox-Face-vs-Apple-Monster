using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNITTYPE
{
    none,
    agregador_glue,
    agregador_ice,
    agregador_fire,
    atomic_bomb,
    couve_pult,
    couve_pult_strong,
    glue,
    glue_strong,
    jalapeno,
    couve_pult_ice,
    couve_pult_ice_strong,
    couve_pult_glue,
    couve_pult_fire,
    couve_pult_fire_strong,
    squash,
    cherry_bomb,
    doom_shroom,
    espada,
    espada_escudo,
    espada_rapida,
    espada_rapida_defence,
    espada_rapida_fuego,
    espada_rapida_fuego_defence,
    espada_rapida_ice,
    espada_rapida_ice_defence,
    fire_peashooter,
    fire_peashooter_strong,
    mina_batata,
    pea_neve,
    pea_neve_super,
    peashooter,
    peashooter_fast,
    peashooter_fast_strong,
    repeater,
    threepeater,
    splitPea,
    splitPea_ice,
    sunflower_one,
    sunflower_two,
    sunflower_three,
    wall_nut,
    wall_nut_tall,
    mousetrap
}

public enum BULLETTYPE
{
    none,
    normal_weak,
    ice_weak,
    fire_weak,
    glue_weak,
    normal_normal,
    ice_normal,
    fire_normal,
    glue_normal,
    normal_strong,
    ice_strong,
    fire_strong,
    glue_strong,
    eliptic_normal_weak,
    eliptic_ice_weak,
    eliptic_fire_weak,
    eliptic_glue_weak,
    eliptic_normal,
    eliptic_ice,
    eliptic_fire,
    eliptic_glue,
    eliptic_normal_strong,
    eliptic_ice_strong,
    eliptic_fire_strong,
    eliptic_glue_strong,
    babypacman,
    jalapeno,
    threepeater_weak,
    threepeater_normal,
    threepeater_strong
}

public enum EXPLOSIONTYPE
{
    none,
    normal,
    fire,
    glue,
    ice,
    normal_mediun,
    fire_mediun,
    glue_mediun,
    ice_mediun,
    normal_big,
    fire_big,
    glue_big,
    ice_big,
    eliptic_normal,
    eliptic_ice,
    eliptic_fire,
    eliptic_glue,
    eliptic_normal_normal,
    eliptic_ice_normal,
    eliptic_fire_normal,
    eliptic_glue_normal,
    eliptic_normal_big,
    eliptic_ice_big,
    eliptic_fire_big,
    eliptic_glue_big,
}

public struct UNITTYPEcomparer : IEqualityComparer<UNITTYPE>
{
    public bool Equals(UNITTYPE x, UNITTYPE y)
    {
        return x == y;
    }

    public int GetHashCode(UNITTYPE obj)
    {
        // you need to do some thinking here,
        return (int)obj;
    }
}

public struct BULLETTYPEcomparer : IEqualityComparer<BULLETTYPE>
{
    public bool Equals(BULLETTYPE x, BULLETTYPE y)
    {
        return x == y;
    }

    public int GetHashCode(BULLETTYPE obj)
    {
        // you need to do some thinking here,
        return (int)obj;
    }
}

public struct EXPLOSIONTYPEcomparer : IEqualityComparer<EXPLOSIONTYPE>
{
    public bool Equals(EXPLOSIONTYPE x, EXPLOSIONTYPE y)
    {
        return x == y;
    }

    public int GetHashCode(EXPLOSIONTYPE obj)
    {
        // you need to do some thinking here,
        return (int)obj;
    }
}

public class UnitType : MonoBehaviour {
    public static UNITTYPEcomparer UnitTypeComparer = new UNITTYPEcomparer();
    public static BULLETTYPEcomparer BulletTypeComparer = new BULLETTYPEcomparer();
    public static EXPLOSIONTYPEcomparer ExplosionTypeComparer = new EXPLOSIONTYPEcomparer();

    public UNITTYPE type;
}

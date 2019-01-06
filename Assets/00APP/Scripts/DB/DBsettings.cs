using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DBsettings
{
    public float sfxVolume;
    public float musicVolume;
    public bool showAdds;
    public float mainScrollPositionX;
    public float mainScrollPositionY;

    public DBsettings(bool showAdds, float sfxVolume, float musicVolume, float mainScrollPositionX, float mainScrollPositionY) {
        this.showAdds = showAdds;
        this.sfxVolume = sfxVolume;
        this.musicVolume = musicVolume;
        this.mainScrollPositionX = mainScrollPositionX;
        this.mainScrollPositionY = mainScrollPositionY;
    }

    public Vector2 mainScrollPosition
    {
        get
        {
            return new Vector2(mainScrollPositionX, mainScrollPositionY);
        }

        set
        {
            mainScrollPositionX = value.x;
            mainScrollPositionY = value.y;
        }
    }
}

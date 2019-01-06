using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DBlevels
{
    //public bool[] unlocked;
    public int[] score;
    public int[] stars;
    public bool win;
    public bool winFull;

    //public DBlevels(bool[] unlocked, int[] score, int[] stars)
    public DBlevels(int[] score, int[] stars, bool win, bool winFull)
    {
        //this.unlocked = unlocked;
        this.score = score;
        this.stars = stars;
        this.win = win;
        this.winFull = winFull;
    }
}
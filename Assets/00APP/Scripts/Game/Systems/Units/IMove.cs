﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void Move(float delta);
    float Speed { get; set; }
}

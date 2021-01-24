﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeatherPreset
{
    //holds multiple effect data presets to create 1 weather

    List<IAnimatableEffect> Effects { get; }
}


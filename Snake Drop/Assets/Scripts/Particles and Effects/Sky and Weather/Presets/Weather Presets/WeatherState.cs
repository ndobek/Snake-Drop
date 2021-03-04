using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//public class WeatherState 
//{

//    public List<SunVolumeLightPreset> sunVolumePresets;
//    public List<SunTerrainLightPreset> sunTerrainPresets;
//    public List<SkyVolumeLightPreset> skyVolumePresets;
//    public List<SkyDetailLightPreset> skyDetailPresets;

//    public List<SkyPreset> skyPresets;    

//}
public class WeatherState
{
    public LightPreset sunVolumePreset;
    public LightPreset sunTerrainPreset;
    public LightPreset skyVolumePreset;
    public LightPreset skyDetailPreset;

    public VolumePreset volumePreset;
    //public SkyPreset skyPreset;
}



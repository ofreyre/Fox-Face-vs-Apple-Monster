using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBGorientationManager
{
    public class OrientationAdjustUI : MonoBehaviour
    {
        public CanvasScaler m_scaler;
        public float matchLandscape;
        public float matchPortrate;

        // Use this for initialization
        void Start()
        {
            OrientationEvents.instance.Detected += OnDetected;
            OrientationEvents.instance.Changed += OnDetected;
        }

        void OnDetected(ScreenOrientation orientation)
        {
            switch (orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    m_scaler.matchWidthOrHeight = matchLandscape;
                    break;
                default:
                    m_scaler.matchWidthOrHeight = matchPortrate;
                    break;
            }
        }
    }
}

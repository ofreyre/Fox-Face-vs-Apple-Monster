using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DBGorientationManager
{
    public class OrientationEvents : MonoBehaviour
    {
        public static OrientationEvents instance;
        public delegate void DelegateChange(ScreenOrientation orientation);
        public event DelegateChange Changed;
        public event DelegateChange Detected;

        [SerializeField] private bool m_debug = false;

        private ScreenOrientation m_orientation;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            m_orientation = Screen.orientation;
            DispatchDetected(m_orientation);
        }

        private void DispatchChanged(ScreenOrientation orientation)
        {
            if (Changed != null)
            {
                Changed(orientation);
            }
        }

        private void DispatchDetected(ScreenOrientation orientation)
        {
            if (Changed != null)
            {
                Changed(orientation);
            }
        }

        void OnRectTransformDimensionsChange()
        {
            if (m_debug)
            {
                Debug.Log("Screen.orientation=" + Screen.orientation);
            }
            if (m_orientation != Screen.orientation)
            {
                m_orientation = Screen.orientation;
                DispatchChanged(m_orientation);
            }
        }
    }
}

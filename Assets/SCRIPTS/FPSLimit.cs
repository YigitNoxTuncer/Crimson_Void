using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class FPSLimit : MonoBehaviour
    {
        public int targetFrameRate = 60;

        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
        }
    }
}


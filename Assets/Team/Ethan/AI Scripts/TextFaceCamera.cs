using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public class TextFaceCamera : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public class TextFaceCameraEvilMarble : MonoBehaviour
    {
        public Transform parentObject; 
        public Vector3 positionOffset; 

        private Transform cameraTransform;

        void Start()
        {
            cameraTransform = Camera.main.transform;

            if (parentObject == null)
                parentObject = transform.parent;
        }

        void Update()
        {
            if (parentObject == null || cameraTransform == null) return;

            //make sure it is on the marbles transform
            transform.position = parentObject.position + positionOffset;

            //set text to face camera
            Vector3 directionToCamera = (cameraTransform.position - transform.position).normalized;
            directionToCamera.y = 0; 
            transform.rotation = Quaternion.LookRotation(directionToCamera);

            //flip text 180 so it faces the camera properly
            transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}


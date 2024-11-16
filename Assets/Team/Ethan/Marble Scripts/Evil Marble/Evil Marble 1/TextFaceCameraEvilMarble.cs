using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public class TextFaceCameraEvilMarble : MonoBehaviour
    {
        public Transform parentObject; // The object the text follows
        public Vector3 positionOffset; // Offset for positioning the text relative to the parent

        private Transform cameraTransform;

        void Start()
        {
            // Cache the camera's transform for efficiency
            cameraTransform = Camera.main.transform;

            // Ensure the parentObject is assigned if not explicitly set
            if (parentObject == null)
                parentObject = transform.parent;
        }

        void Update()
        {
            if (parentObject == null || cameraTransform == null) return;

            // Position the text relative to the parent with an offset
            transform.position = parentObject.position + positionOffset;

            // Make the text face the camera
            Vector3 directionToCamera = (cameraTransform.position - transform.position).normalized;
            directionToCamera.y = 0; // Keep the text upright
            transform.rotation = Quaternion.LookRotation(directionToCamera);

            // Flip the text 180 degrees
            transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}


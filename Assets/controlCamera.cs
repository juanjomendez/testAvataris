using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts
{

    public class controlCamera : MonoBehaviour
    {
        public renderMode rm;

        

        void Start()
        {

        }


        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, 0, 1), 0.1f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, 0, 1), -0.1f);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.1f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.1f);
            }

        }
    }
}
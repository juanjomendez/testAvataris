using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts
{

    public class rotate : MonoBehaviour
    {
        float timer;
        float xInc, yInc, zInc;

        void Start()
        {
            timer = 0;
            setIncs();
        }

        void setIncs()
        {
            xInc = Random.Range(-2, 2);
            yInc = Random.Range(-2, 2);
            zInc = Random.Range(-2, 2);
        }

        void Update()
        {

            timer += Time.deltaTime;
            int seconds = (int)(timer % 60);

            if (seconds >= 2)
            {
                timer = 0;
                setIncs();
            }

            gameObject.transform.Rotate(new Vector3(0, 0, 1), zInc);
            gameObject.transform.Rotate(new Vector3(0, 1, 0), yInc);
            gameObject.transform.Rotate(new Vector3(1, 0, 0), xInc);
        }
    }
}
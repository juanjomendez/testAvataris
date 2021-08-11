using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.scripts
{

    public class renderMode : MonoBehaviour
    {
        public Canvas m_canvas;
        bool isMoving, isWaiting;

        public enum Types { World_mode, Screen_mode };
        public Types type;
        Types prevType;

        Vector2 resolution;
        public RawImage backgroundUI;
        public Button buttonLeft, buttonRight;
        public Text titleText;
        float startingCount;

        enum uiPositions { CENTERED, EDGE}
        uiPositions posUI;



        public void Awake()
        {

            posUI = uiPositions.CENTERED;

            isMoving = false;
            isWaiting = false;

            prevType = Types.Screen_mode;
            type = Types.Screen_mode;

            m_canvas.renderMode = RenderMode.ScreenSpaceCamera;

            resolution = new Vector2(Screen.width, Screen.height);

            setsizeOfUI();

        }


        void Start()
        {

            buttonLeft.onClick.AddListener(delegate
            {
                if ((!isWaiting) && (!isMoving))
                    clickOnLeftButton();
            });

            buttonRight.onClick.AddListener(delegate
            {
                if ((!isWaiting) && (!isMoving))
                    clickOnRightButton();
            });

        }


        IEnumerator MoveUI(Vector3 posIni, Vector3 posEnd, uiPositions pos)
        {
            
            float t = 0f;
            float a;
            isMoving = true;
            float increment = 0.015f;
            float iAngle = 360f * increment;

            while (t <= 1f)
            {
                backgroundUI.transform.localPosition = Vector3.Lerp(posIni, posEnd, t);

                a = Mathf.Sin(t * Mathf.PI);
                a /= 3f;
                a = 1f - a;

                backgroundUI.transform.localScale = new Vector3(a, a, a);
                backgroundUI.transform.Rotate(new Vector3(iAngle, 0, 0) );

                t += increment;
                yield return null;
            }

            backgroundUI.transform.localPosition = posEnd;
            backgroundUI.transform.localScale = Vector3.one;
            backgroundUI.transform.localRotation = Quaternion.Euler(0,0,0);

            posUI = pos;
            isMoving = false;

            if (posUI == uiPositions.EDGE)
            {
                startingCount = 0f;
                isWaiting = true;
            }

        }


        void clickOnLeftButton()
        {
            float W, w;
            W = m_canvas.GetComponent<RectTransform>().sizeDelta.x;
            w = backgroundUI.rectTransform.sizeDelta.x;

            StartCoroutine(MoveUI(backgroundUI.transform.localPosition, 
                new Vector3(-W/2 + w/2, backgroundUI.transform.localPosition.y, backgroundUI.transform.localPosition.z), uiPositions.EDGE));
        }


        void clickOnRightButton()
        {
            float W, w;
            W = m_canvas.GetComponent<RectTransform>().sizeDelta.x;
            w = backgroundUI.rectTransform.sizeDelta.x;

            StartCoroutine(MoveUI(backgroundUI.transform.localPosition,
                new Vector3(W / 2 - w / 2, backgroundUI.transform.localPosition.y, backgroundUI.transform.localPosition.z), uiPositions.EDGE));
        }


        public void setsizeOfUI()
        {

            float W, H, w, h, xLeft, xRight, y;

            W = Screen.width / 2;
            H = Screen.height / 2;

            backgroundUI.rectTransform.sizeDelta = new Vector2(W, H);

            w = W / 6;
            h = H / 6;

            xLeft = 0 - (W / 2) + (w / 2);
            y = 0 - (H / 2) + (h / 2);
            xRight = 0 + (W / 2) - (w / 2);

            buttonLeft.image.transform.localPosition = new Vector2(xLeft, y);
            buttonLeft.image.rectTransform.sizeDelta = new Vector2(w, h);

            buttonRight.image.transform.localPosition = new Vector2(xRight, y);
            buttonRight.image.rectTransform.sizeDelta = new Vector2(w, h);

            titleText.transform.localPosition = new Vector2(0, 0 + (H / 2) - (h / 2));
            titleText.rectTransform.sizeDelta = new Vector2(W, h);

        }


        private void ChangeRenderMode(RenderMode m)
        {
            m_canvas.renderMode = m;
            setsizeOfUI();
        }


        void Update()
        {

            if ((posUI == uiPositions.EDGE) && (!isMoving))
            {
                startingCount += Time.deltaTime;
                if (startingCount >= 3f)
                {
                    isWaiting = false;
                    StartCoroutine(MoveUI(backgroundUI.transform.localPosition,
                        new Vector3(0, 0, 0), uiPositions.CENTERED));
                }
            }
            
            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                resolution.x = Screen.width;
                resolution.y = Screen.height;

                if (m_canvas.renderMode == RenderMode.WorldSpace)
                {
                    Canvas.ForceUpdateCanvases();
                    m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    m_canvas.renderMode = RenderMode.WorldSpace;
                    setsizeOfUI();
                }
                else
                    setsizeOfUI();
            }
            
            if (type != prevType)
            {
                prevType = type;
                if (type == Types.World_mode)
                    ChangeRenderMode(RenderMode.WorldSpace);
                else
                    ChangeRenderMode(RenderMode.ScreenSpaceCamera);
            }

        }
    }

}
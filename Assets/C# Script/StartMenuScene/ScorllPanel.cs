using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.C__Script.StartMenuScene
{
    public class ScorllPanel : MonoBehaviour
    {
        private ScrollRect scrollRect;

        // Start is called before the first frame update
        void Start()
        {
            scrollRect = GetComponent<ScrollRect>();

            scrollRect.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            scrollRect.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            scrollRect.verticalNormalizedPosition = 1;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator FixSize()
        {
            yield return new WaitForSecondsRealtime(0.2F);

            scrollRect.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            scrollRect.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            scrollRect.verticalNormalizedPosition = 1;
        }
    }

}

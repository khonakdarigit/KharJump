using Assets.C__Script.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.C__Script.StartMenuScene
{
    public class TopScoreSection : MonoBehaviour
    {

        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject topSocreContent;
        [SerializeField] private GameObject scrollPanel;
        [SerializeField] private GameObject itemPrefab;

        public static TopScoreSection instance;


        public bool ActiveSection;

        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (API.instance._topScoreAll.Any() &&
                API.instance._topScoreWeek != null &&
                API.instance._topScoreDay != null && !ActiveSection)
            {
                ActiveSection = true;
                loadingPanel.SetActive(false);
                FillContent(API.instance._topScoreAll, "TOP SCORE");
            }
        }


        public void Btn_TopScoreAll()
        {
            GameController.instance.ButtonClickSound();
            FillContent(API.instance._topScoreAll, "TOP SCORE");

        }
        public void Btn_TopScoreWeek()
        {
            GameController.instance.ButtonClickSound();
            FillContent(API.instance._topScoreWeek, "TOP SCORE IN WEEK");
        }
        public void Btn_TopScoreDay()
        {
            GameController.instance.ButtonClickSound();
            FillContent(API.instance._topScoreDay, "TOP SCORE IN DAY");
        }

        public void FillContent(List<TopScoreViewModel> topScore, string Header)
        {
            ClearContent();
            if (topScore != null & topScore.Any())
            {

                foreach (var item in topScore)
                {
                    var additem = itemPrefab.GetComponent<RowItem>();

                    additem.Number = item.number.ToString();
                    additem.username = item.username;
                    additem.Score = item.score.ToString();

                    Instantiate(additem, topSocreContent.transform);
                }

                StartCoroutine(scrollPanel.GetComponent<ScorllPanel>().FixSize());
            }

        }

        private void ClearContent()
        {
            while (topSocreContent.transform.childCount > 0)
            {
                DestroyImmediate(topSocreContent.transform.GetChild(0).gameObject);
            }
        }





    }
}

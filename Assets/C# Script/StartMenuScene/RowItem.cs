using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.C__Script.StartMenuScene
{
    public class RowItem : MonoBehaviour
    {

        public Text txt_Number;
        public Text txt_SysName;
        public Text txt_Score;


        // Start is called before the first frame update
        public void Init(string Number, string SysName, string Score, bool thisMe)
        {
            txt_Number.text = Number;
            txt_SysName.GetComponent<FixArabic3DText>().SetText(thisMe ? $"YOU ({SysName})" : SysName);
            txt_Score.text = Score;

            if (thisMe)
            {
                gameObject.GetComponent<Image>().color = new Color(0.6226415f, 0.6226415f, 0.6226415f, 0.466f);
            }
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}

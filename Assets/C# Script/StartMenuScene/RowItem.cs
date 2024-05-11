using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.C__Script.StartMenuScene
{
    public class RowItem : MonoBehaviour
    {
        public string Number;
        public string username;
        public string Score;

        public TMPro.TextMeshProUGUI txt_Number;
        public TMPro.TextMeshProUGUI txt_Name;
        public TMPro.TextMeshProUGUI txt_Score;


        // Start is called before the first frame update
        void Start()
        {
            txt_Number.text = Number;
            txt_Name.text = username;
            txt_Score.text = Score + " m";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

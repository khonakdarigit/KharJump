using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public static class Helper
    {
        public static string AppSharingLink()
        {
            string link = "";

            switch (Can_StartPage.polishFor)
            {
                case PolishFor.Playe:
                    link = "https://play.google.com/store/apps/details?id=" + Application.identifier;
                    break;
                case PolishFor.Myket:
                    link = "https://myket.ir/app/" + Application.identifier;
                    break;
                case PolishFor.Bazar:
                    link = "https://cafebazaar.ir/app/" + Application.identifier;
                    break;
                case PolishFor.Taptap:
                    link = "https://www.taptap.io/app/33629693";
                    break;
                default:
                    break;
            }
            return link;
        }

        internal static string AppContactUrl()
        {
            string url = "";
            switch (Can_StartPage.polishFor)
            {
                case PolishFor.Playe:
                    url = "market://details?id=" + Application.identifier;
                    break;
                case PolishFor.Myket:
                    url = "myket://comment?id=" + Application.identifier;
                    break;
                case PolishFor.Bazar:
                    url = "bazaar://details?id=" + Application.identifier;
                    break;
                case PolishFor.Taptap:
                    url = "https://www.taptap.io/app/33629693";
                    break;
                default:
                    break;
            }
            return url;
        }

        internal static string ToMoneyStringFormat(int coinBox)
        {
            string str = coinBox.ToString("000,000,000,000,000");
            string resStr = TrimText(str);
            return resStr;
        }
        private static string TrimText(string str)
        {
            bool GetStartChar = false;

            string resStr = "0";
            foreach (var item in str)
            {
                if (GetStartChar)
                {
                    resStr += item;
                }
                else if (item != '0' && item != ',')
                {
                    GetStartChar = true;
                    resStr = item.ToString();
                }
            }

            return resStr;
        }
    }

}

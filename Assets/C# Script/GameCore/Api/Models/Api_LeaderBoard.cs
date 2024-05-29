using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.C__Script.GameCore.Api.Models
{
    //    {
    //  "id": "string",
    //  "userId": "string",
    //  "sysName": "string",
    //  "score": 0
    //}
    [Serializable]
    public class Api_LeaderBoard
    {
        public string id;
        public string userId;
        public string sysName;
        public int score;
    }
}

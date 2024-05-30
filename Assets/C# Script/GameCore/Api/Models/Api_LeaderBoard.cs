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

  //  [
  //{
  //  "userId": "665798a97f7b4edf5e4412c2",
  //  "sysName": "Player_3948",
  //  "score": 111,
  //  "id": "66579da8eb2030790ba1d0a7"
  //}]
[Serializable]
    public class Api_LeaderBoard
    {
        public string id;
        public string userId;
        public string sysName;
        public int score;
    }
}

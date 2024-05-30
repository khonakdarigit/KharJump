using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.C__Script.GameCore.Api.Models
{
    [System.Serializable]

    public class JsonListWrapper<T>
    {
        public List<T> list;
        public JsonListWrapper(List<T> list) => this.list = list;
    }
}

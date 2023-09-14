using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate_google
{
    public class detectClass
    {
        //{"data":{"detections":[[{"confidence":1,"isReliable":false,"language":"tr"}]]}}
        public string language { get; set; }
        public int confidence { get; set; }
        public bool isReliable { get; set; }
    }
}

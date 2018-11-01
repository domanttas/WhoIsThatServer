using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoIsThatServer.Recognition.Models
{
    public class FaceFeaturesModel
    {
        public int PersonId { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
    }
}

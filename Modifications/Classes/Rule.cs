using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TriadNSim.Modifications.Classes
{
    class Rule
    {
        public ArrayList leftPart;
        public ArrayList rightPart ;
        public NetworkObject input;
        public string Name;

        public Rule()
        {
            leftPart = new ArrayList();
            rightPart = new ArrayList();
            input = null;
            Name = "";
        }
    }
}

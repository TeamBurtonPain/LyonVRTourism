using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class Badge
    {
        private string name;
        private string description;
        private long value;

        public Badge(string n, string d, long v)
        {
            name = n;
            description = d;
            value = v;
        }
    }
}

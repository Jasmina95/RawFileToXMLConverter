using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class XMLNode
    {
        public string? Name { get; set; }
        public string? Value { get; set; }   
        public List<XMLNode> ChildNodes { get; set; } = new List<XMLNode>();
    }
}

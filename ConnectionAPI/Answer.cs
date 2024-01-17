using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionAPI
{
    internal class Answer
    {
        public Search[] search;

        public int totalResults {  get; set; }

        public bool Response {  get; set; }
    }
}

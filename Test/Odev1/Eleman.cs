using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Odev1
{
    public class Eleman
    {
        public Eleman(string binaryCount, int count, int us)
        {
            BinaryCount = binaryCount;
            Count = count;
            Us = us;
        }
        public int Us { get; set; }
        public String BinaryCount { get; set; }
        public int Count { get; set; }
        public List<Polinom> Polinom { get; set; } = new List<Polinom>();
    }
}

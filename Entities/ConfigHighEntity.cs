using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ConfigHighEntity
    {
        public double ConnectTolerance { get; set; }

        public double PartRotateStep { get; set; }

        public int EvaluationFactor { get; set; }

        public int NestTime { get; set; }

        public bool PartInPart { get; set; }

        public bool Optimization { get; set; }
    }
}

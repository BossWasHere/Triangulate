using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulate
{
    class TrigMath
    {

        public static double GetAngle(int a, int b, int opposite)
        {
            try
            {
                double solution = (Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(opposite, 2)) / (2 * a * b);
                if (solution < -1 || solution > 1) return -1;
                return Math.Acos(solution);
            } catch
            {
                return -1;
            }
        }

        public static double GetOppSin(int hyp, double angle)
        {
            return (hyp * Math.Sin(angle));
        }

        public static double GetAdjCos(int hyp, double angle)
        {
            return (hyp * Math.Cos(angle));
        }
    }
}

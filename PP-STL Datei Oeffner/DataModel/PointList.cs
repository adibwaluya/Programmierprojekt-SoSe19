using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class PointList
    {
        private int m_idx = 0;
        private Dictionary<int, Point> m_int2pt = new Dictionary<int, Point>();
        private Dictionary<Point, int> m_pt2int = new Dictionary<Point, int>();

        public int AddOrGetPoint(Point p)
        {
            if (m_pt2int.ContainsKey(p))
                return m_pt2int[p];
            //noch nicht in Liste enthalten...
            m_int2pt[m_idx] = p;
            m_pt2int[p] = m_idx;
            return m_idx++;
        }

        public Point GetPoint(int id)
        {
            if (m_int2pt.ContainsKey(id))
                return m_int2pt[id];
            return null;
        }
    }
}

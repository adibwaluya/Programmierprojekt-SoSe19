using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class EdgeList
    {
        private int m_idx = 0;
        private Dictionary<int, Edge> m_int2edge = new Dictionary<int, Edge>();
        private Dictionary<Edge, int> m_edge2int = new Dictionary<Edge, int>();

        public int AddOrGetEdge(Edge p)
        {
            if (m_edge2int.ContainsKey(p))
                return m_edge2int[p];
            //noch nicht in Liste enthalten...
            m_int2edge[m_idx] = p;
            m_edge2int[p] = m_idx;
            return m_idx++;
        }

        public Edge GetEdge(int id)
        {
            if (m_int2edge.ContainsKey(id))
                return m_int2edge[id];
            return null;
        }
    }
}

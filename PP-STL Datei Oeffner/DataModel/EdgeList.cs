using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class EdgeList
    {
        private int m_idx = 0; // Starting index
        private Dictionary<int, Edge> m_int2edge = new Dictionary<int, Edge>(); // register edge if ID is given
        private Dictionary<Edge, int> m_edge2int = new Dictionary<Edge, int>(); // register edge if edge is given

        /* User adds or gets edge if an 'edge' variable is given */
        public int AddOrGetEdge(Edge p)
        {
            /* If an edge's already registered in dictionary and contains an ID */
            if (m_edge2int.ContainsKey(p))
                return m_edge2int[p];

            //noch nicht in Liste enthalten...
            m_int2edge[m_idx] = p;
            m_edge2int[p] = m_idx;
            return m_idx++;
        }

        /* User defines an edge by giving an ID to this methode as parameter */
        public Edge GetEdge(int id)
        {
            if (m_int2edge.ContainsKey(id))
                return m_int2edge[id];
            return null;
        }
    }
}

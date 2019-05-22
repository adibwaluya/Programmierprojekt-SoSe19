using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class FaceList
    {
        private int m_idx = 0;
        private Dictionary<int, Face> m_int2Face = new Dictionary<int, Face>();
        private Dictionary<Face, int> m_Face2int = new Dictionary<Face, int>();

        public int AddOrGetFace(Face p)
        {
            if (m_Face2int.ContainsKey(p))
                return m_Face2int[p];
            //noch nicht in Liste enthalten...
            m_int2Face[m_idx] = p;
            m_Face2int[p] = m_idx;
            return m_idx++;
        }

        public Face GetFace(int id)
        {
            if (m_int2Face.ContainsKey(id))
                return m_int2Face[id];
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class FaceList
    {
        private int m_idx = 0; // Starting index
        public Dictionary<int, Face> m_int2Face = new Dictionary<int, Face>(); // register face if ID is given
        public Dictionary<Face, int> m_Face2int = new Dictionary<Face, int>(); // register face if face is given

        /* User adds or gets face if a 'face' variable is given */
        public int AddOrGetFace(Face p)
        {
            /* If a face's already registered in dictionary and contains an ID */
            if (m_Face2int.ContainsKey(p))
                return m_Face2int[p];

            //noch nicht in Liste enthalten...
            m_int2Face[m_idx] = p;
            m_Face2int[p] = m_idx;
            return m_idx++;
        }

        /* User defines a face by giving an ID to this methode as parameter */
        public Face GetFace(int id)
        {
            if (m_int2Face.ContainsKey(id))
                return m_int2Face[id];
            return null;
        }
    }
}

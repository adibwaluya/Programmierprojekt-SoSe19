/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/
using System.Collections.Generic;

namespace ErrorHandling
{
    /// <summary>
    /// Beinhaltet eine Liste von allen Kanten mit Ausrichtung vectorY, vectorZ.
    /// </summary>
    public class VectorOfEdge
    {
        public double vectorY;
        public double vectorZ;
        public List<int> edgeIDList = new List<int>();
        public void addCoordinates(double _VectorY, double _VectorZ)
        {
            vectorY = _VectorY;
            vectorZ = _VectorZ;
        }
    }
}

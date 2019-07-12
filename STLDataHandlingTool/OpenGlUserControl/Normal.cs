/*******************************************************************************************
 * Copyright (c) <2019><Michael Kaip, Maximilian Mews, Michael Reno, Adib Ghassani Waluya> *
 *******************************************************************************************/

// The aim of this class is to provide a data type, in which the normals can be stored.

#region Using directives

using OpenTK;

#endregion

namespace OpenGlUserControl
{
    public struct Normal
    {
        public Vector3d NormalVector;

        public static int SizeInBytes => Vector3d.SizeInBytes;

        public Normal(Vector3d normalVector)
        {
            NormalVector = normalVector;
        }
    }
}

using OpenTK;

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

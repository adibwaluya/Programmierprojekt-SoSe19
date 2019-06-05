using System.Windows;
using DataModel;
using Point = DataModel.Point;

namespace OpenGlUserControlTest
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Helper for creating a dummy-datamodel
        private void addFace(DataModel.Point p1, DataModel.Point p2, DataModel.Point p3, Normal n, DataModel.DataModel dm)
        {
            int pntId1 = dm.AddPoint(p1.X,p1.Y,p1.Z);
            int pntId2 = dm.AddPoint(p2.X, p2.Y, p2.Z);
            int pntId3 = dm.AddPoint(p3.X, p3.Y, p3.Z);
            int ie1 = dm.AddEdge(pntId1, pntId2);
            int ie2 = dm.AddEdge(pntId1, pntId3);
            int ie3 = dm.AddEdge(pntId2, pntId3);
            dm.AddFace(ie1, ie2, ie3, n);
        }

        // EventHandler for Btn in MAinWindow
        private void CreateAndShowModel_Click(object sender, RoutedEventArgs e)
        {
            var dm = new DataModel.DataModel();

            Point p1 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            Point p2 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            Point p3 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, -1.000000e+00, 0.000000e+00), dm);

            // Face 2
            p1 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            p2 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            p3 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, -1.000000e+00, 0.000000e+00), dm);

            // Face 3
            p1 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            p2 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            p3 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p1, p2, p3, new Normal(1.000000e+00, 0.000000e+00, -0.000000e+00), dm);

            // Face 4
            p1 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            p2 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            p3 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p1, p2, p3, new Normal(1.000000e+00, 0.000000e+00, 0.000000e+00), dm);

            // Face 5
            p1 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            p2 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            p3 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, 0.000000e+00, 1.000000e+00), dm);

            // Face 6
            p1 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            p2 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            p3 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, 0.000000e+00, 1.000000e+00), dm);

            // Face 7
            p1 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            p2 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            p3 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p1, p2, p3, new Normal(-1.000000e+00, -0.000000e+00, -0.000000e+00), dm);

            // Face 8
            p1 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            p2 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            p3 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p1, p2, p3, new Normal(-1.000000e+00, 0.000000e+00, 0.000000e+00), dm);

            // Face 9
            p1 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            p2 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            p3 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, 0.000000e+00, -1.000000e+00), dm);

            // Face 10
            p1 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            p2 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            p3 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, 0.000000e+00, -1.000000e+00), dm);

            // Face 11
            p1 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            p2 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            p3 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, 1.000000e+00, 0.000000e+00), dm);

            // Face 12
            p1 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            p2 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            p3 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, 1.000000e+00, -0.000000e+00), dm);
        }
    }
}

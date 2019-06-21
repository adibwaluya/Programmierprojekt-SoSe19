using System.Windows;
using DataModel;
using Point = DataModel.Point;

namespace OpenGlUserControlTest
{

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Helper for creating a dummy-datamodel
        private void addFace(Point firstPoint, Point secondPoint, Point thirdPoint, Normal n, DataStructure dm) 
        {
            var pntId1 = dm.AddPoint(firstPoint.X,firstPoint.Y,firstPoint.Z);
            var pntId2 = dm.AddPoint(secondPoint.X, secondPoint.Y, secondPoint.Z);
            var pntId3 = dm.AddPoint(thirdPoint.X, thirdPoint.Y, thirdPoint.Z);
            var ie1 = dm.AddEdge(pntId1, pntId2);
            var ie2 = dm.AddEdge(pntId1, pntId3);
            var ie3 = dm.AddEdge(pntId2, pntId3);
            dm.AddFace(ie1, ie2, ie3, n);
        }

        // EventHandler for Btn in MainWindow: Creates a dummy-datamodel for component-testing
        public void CreateAndShowModel_Click(object sender, RoutedEventArgs e)
        {

            var dataStructure  = new DataStructure();

            var p1 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            var p2= new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            var p3= new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p1, p2, p3, new Normal(0.000000e+00, -1.000000e+00, 0.000000e+00), dataStructure);

            // Face 2
            var p4 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            var p5 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            var p6 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p4, p5, p6, new Normal(0.000000e+00, -1.000000e+00, 0.000000e+00), dataStructure);

            // Face 3
            var p7 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p8 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p9 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p7, p8, p9, new Normal(1.000000e+00, 0.000000e+00, -0.000000e+00), dataStructure);

            // Face 4
            var p10 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            var p11 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p12 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p10, p11, p12, new Normal(1.000000e+00, 0.000000e+00, 0.000000e+00), dataStructure);

            // Face 5
            var p13 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p14 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p15 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p13, p14, p15, new Normal(0.000000e+00, 0.000000e+00, 1.000000e+00), dataStructure);

            // Face 6
            var p16 = new Point(1.000000e+01, -1.000000e+01, 1.000000e+01);
            var p17 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p18 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p16, p17, p18, new Normal(0.000000e+00, 0.000000e+00, 1.000000e+00), dataStructure);

            // Face 7
            var p19 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p20 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p21 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            addFace(p19, p20, p21, new Normal(-1.000000e+00, -0.000000e+00, -0.000000e+00), dataStructure);

            // Face 8
            var p22 = new Point(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            var p23 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p24 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p22, p23, p24, new Normal(-1.000000e+00, 0.000000e+00, 0.000000e+00), dataStructure);

            // Face 9
            var p25 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p26 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p27 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p25, p26, p27, new Normal(0.000000e+00, 0.000000e+00, -1.000000e+00), dataStructure);

            // Face 10
            var p28 = new Point(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            var p29 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p30 = new Point(1.000000e+01, -1.000000e+01, -1.000000e+01);
            addFace(p28, p29, p30, new Normal(0.000000e+00, 0.000000e+00, -1.000000e+00), dataStructure);

            // Face 11
            var p31 = new Point(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p32 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p33 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            addFace(p31, p32, p33, new Normal(0.000000e+00, 1.000000e+00, 0.000000e+00), dataStructure);

            // Face 12
            var p34 = new Point(1.000000e+01, 1.000000e+01, -1.000000e+01);
            var p35 = new Point(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            var p36 = new Point(1.000000e+01, 1.000000e+01, 1.000000e+01);
            addFace(p34, p35, p36, new Normal(0.000000e+00, 1.000000e+00, -0.000000e+00), dataStructure);

            var bc = ColorRGB.CreateBackgroundColor(192f, 192f, 192f);
            var fc = ColorRGB.CreateForegroundColor(201f, 12f, 12);
            var userSettings = new UserSettings(bc, fc);

            var dataPoints = dataStructure.GetDataPointsList3D();
            WinFormsControl.DrawModel(dataPoints, userSettings.BackgroundColor, userSettings.ForegroundColor);
        }
    }
}

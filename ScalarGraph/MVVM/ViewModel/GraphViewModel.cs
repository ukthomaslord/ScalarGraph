using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using ScalarGraph.Core;
using ScalarGraph.MVVM.View;
using ScalarGraph.MVVM.Model;
using System.Collections.ObjectModel;

namespace ScalarGraph.MVVM.ViewModel
{
    class GraphViewModel
    {
        public IList<MyVector> Vectors { get; set; }

        public GraphViewModel()
        {
            Vectors = new ObservableCollection<MyVector>();
            Vectors.Add(new MyVector());
            Vectors.Add(new MyVector());

            AddVectorCommand = new RelayCommand((obj) => { DrawVector(); });
            CreateGraph();
        }

        private void DrawVector()
        {
            //Get result
            MyVector result = AddVector();
            
            
            //Create Series
            var series1 = new LineSeries { Title = "Vector 1", MarkerType = MarkerType.Circle, Color = OxyColors.Aqua };
            series1.Points.Add(new DataPoint(0, 0));
            series1.Points.Add(new DataPoint(Vectors[0].X, Vectors[0].Y));

            var series2 = new LineSeries { Title = "Vector 2", MarkerType = MarkerType.Circle };
            series2.Points.Add(new DataPoint(0, 0));
            series2.Points.Add(new DataPoint(Vectors[1].X, Vectors[1].Y));

            var series3 = new LineSeries { Title = "Result", MarkerType = MarkerType.Circle };
            series3.Points.Add(new DataPoint(0, 0));
            series3.Points.Add(new DataPoint(Vectors[0].X, Vectors[0].Y));
            series3.Points.Add(new DataPoint(result.X, result.Y));
            

            //Add Series to Graph
            tmp.Series.Add(series1);
            tmp.Series.Add(series2);
            tmp.Series.Add(series3);
            
            
            this.Model = tmp;
        }

        private MyVector AddVector()
        {
            MyVector tempVector = new MyVector();
            tempVector.X = Vectors[0].X + Vectors[1].X;
            tempVector.Y = Vectors[0].Y + Vectors[1].Y;

            return tempVector;
        }

        public void CreateGraph()
        {

            // Axes are created automatically if they are not defined
            var verticalAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -20,
                Maximum = 20,
                MajorGridlineStyle = LineStyle.Dot
            };

            var horizontalAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -40,
                Maximum = 40,
                MajorGridlineStyle = LineStyle.Dot
            };

            tmp.Axes.Add(horizontalAxis);
            tmp.Axes.Add(verticalAxis);

            //Add Center Lines 
            tmp.Annotations.Add(new LineAnnotation
            {
                StrokeThickness = 1,
                LineStyle = LineStyle.Solid,
                Type = LineAnnotationType.Vertical,
                Color = OxyColors.Black
            });

            tmp.Annotations.Add(new LineAnnotation
            {
                StrokeThickness = 1,
                LineStyle = LineStyle.Solid,
                Type = LineAnnotationType.Horizontal,
                Color = OxyColors.Black
            });

            // Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
            this.Model = tmp;
        }

        public ICommand AddVectorCommand { get; }

        public PlotModel tmp = new PlotModel { };
        public PlotModel Model { get; private set; }
    }
}

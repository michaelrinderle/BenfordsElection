using OxyPlot;
using OxyPlot.Series;
using System;

namespace BenfordsElection.ViewModels
{
    public class MainViewModel
    {
        public PlotModel MyModel { get; private set; }
        public MainViewModel()
        {
            this.MyModel = new PlotModel { Title = "Example 1" };
            this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }
    }
}

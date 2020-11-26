/*
		  __ _/| _/. _  ._/__ /
		_\/_// /_///_// / /_|/
				   _/
		sof digital 2020 (mit license)
		written by michael rinderle <michael@sofdigital.net>
*/

using BenfordsElection.Commands;
using BenfordsElection.Models;
using BenfordsElection.Utility;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BenfordsElection.ViewModels
{
    public class BenfordGraphViewModel : BaseViewModel
    {
        public ICommand OpenDataFileCommand { get; set; }
        public ICommand ReloadDataFileCommand { get; set; }
        public ICommand OpenWIOpenDataLinkCommand { get; set; }
        public ICommand CheckboxGraphUpdateCommand { get; set; }

        public bool GraphTotal { get;set;} = true;
        public bool GraphRep { get; set; } = true;
        public bool GraphDem { get; set; } = true;
        public bool GraphGrn { get; set; } = false;
        public bool GraphLib { get; set; } = false;
        public bool GraphCon { get; set; } = false;
        public bool GraphInd { get; set; } = false;

        public string _TotalCount { get; set; }
        public string TotalCount
        {
            get => _TotalCount;
            set
            {
                if(_TotalCount == value) return;
                _TotalCount = value;
                OnPropertyChanged();
            }
        }
        public string _RepCount { get; set; }
        public string RepCount
        {
            get => _RepCount;
            set
            {
                if (_RepCount == value) return;
                _RepCount = value;
                OnPropertyChanged();
            }
        }
        public string _DemCount { get; set; }
        public string DemCount
        {
            get => _DemCount;
            set
            {
                if (_DemCount == value) return;
                _DemCount = value;
                OnPropertyChanged();
            }
        }
        public string _GrnCount { get; set; }
        public string GrnCount
        {
            get => _GrnCount;
            set
            {
                if (_GrnCount == value) return;
                _GrnCount = value;
                OnPropertyChanged();
            }
        }
        public string _LibCount { get; set; }
        public string LibCount
        {
            get => _LibCount;
            set
            {
                if (_LibCount == value) return;
                _LibCount = value;
                OnPropertyChanged();
            }
        }
        public string _ConCount { get; set; }
        public string ConCount
        {
            get => _ConCount;
            set
            {
                if (_ConCount == value) return;
                _ConCount = value;
                OnPropertyChanged();
            }
        }
        public string _IndCount { get; set; }
        public string IndCount
        {
            get => _IndCount;
            set
            {
                if (_IndCount == value) return;
                _IndCount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Algorithms { get; set; } = new ObservableCollection<string>() { "2020", "2016", "2012" };

        private string _Algorithm { get; set; } = "2016";
        public string Algorithm
        {
            get => _Algorithm;
            set
            {
                if (_Algorithm == value) return;
                _Algorithm = value;
                OnPropertyChanged();
                LoadBenfordData();
            }
        }

        BenfordsLaw BenfordsLaw = new BenfordsLaw();
        List<VoterPrecinct> VoterPrecincts { get; set; } = new List<VoterPrecinct>();

        public PlotModel BenfordGraphModel { get; private set; }
        public LineSeries TotalSeries = new LineSeries();
        public LineSeries RepublicanSeries = new LineSeries();
        public LineSeries DemocratSeries = new LineSeries();
        public LineSeries GreenSeries = new LineSeries();
        public LineSeries LibertarianSeries = new LineSeries();
        public LineSeries ConsititutionSeries = new LineSeries();
        public LineSeries IndependentSeries = new LineSeries();
        public LineSeries BenfordSeries = new LineSeries();

        public BenfordGraphViewModel()
        {
            SetCommands();
            LoadGraph();            
        }

        /// <summary>
        /// configure our relay commands for xaml buttons to behave
        /// </summary>
        private void SetCommands()
        {
            OpenDataFileCommand = new RelayCommand(o => OpenDataFile(null));
            ReloadDataFileCommand = new RelayCommand(o => LoadBenfordData());
            OpenWIOpenDataLinkCommand = new RelayCommand(o => OpenWIOpenDataLink());
            CheckboxGraphUpdateCommand = new RelayCommand(o => LoadBenfordData());
        }

        /// <summary>
        /// command relay to open csv file to parse
        /// </summary>
        /// <param name="sender"></param>
        private void OpenDataFile(object sender)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV files (*.csv)|*.csv";
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                VoterPrecincts = CsvParser.Parse(fileDialog.FileName);
                if(VoterPrecincts.Count != 0)
                {
                    string cap = "Benford's Election (WI Edition)";
                    string msg = "CSV file is parsed correctly";
                    var button = MessageBoxButton.OK;
                    var icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(msg, cap, button, icon);
                }               
            }
        }

        /// <summary>
        /// command relay for opening up wisconsin open data
        /// website for csv download to ananlyze in program
        /// </summary>
        /// <param name="p"></param>
        private void OpenWIOpenDataLink()
        {
            var wiOpenData = "https://data-ltsb.opendata.arcgis.com/datasets/2012-2018-election-data-with-2020-wards";
            var sInfo = new ProcessStartInfo(wiOpenData)
            {
                UseShellExecute = true,
            };
            Process.Start(sInfo);
        }

        /// <summary>
        /// checking for loaded data or 2020 election results
        /// </summary>
        private bool LoadCheck()
        {
            string cap = "Benford's Election";          
            if (VoterPrecincts.Count == 0)
            {
                string msg = "Election Data hasn't been loaded yet.";
                var button = MessageBoxButton.OK;
                var icon = MessageBoxImage.Exclamation;
                MessageBox.Show(msg, cap, button, icon);
                return false;
            }
            else if (VoterPrecincts.Count != 0 &&
                     Algorithm == "2020" &&
                     VoterPrecincts.Any(x => x.PresidentVote2020 == 0))
            {
                string msg = "2020 Wisconsin Election Data Is Not Published";
                var button = MessageBoxButton.OK;
                var icon = MessageBoxImage.Exclamation;
                MessageBox.Show(msg, cap, button, icon);
                return false;
            }
            else return true;
        }

        /// <summary>
        /// loading graph titles, axis, series, and colors
        /// </summary>
        private void LoadGraph()
        {
            string graphTitle = "P(d) = LOG(b)(d+1) || LOG(b)(d) = LOG(b)(1+(1/d))";
            BenfordGraphModel = new PlotModel { Title = graphTitle };
            BenfordGraphModel.PlotAreaBackground = OxyColors.Wheat;
            BenfordGraphModel.Background = OxyColors.LightSlateGray;

            BenfordGraphModel.Axes.Add(
                new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 4500 });
            BenfordGraphModel.Axes.Add(
                new LinearAxis { Position = AxisPosition.Bottom, Minimum = 1, Maximum = 9 });

            TotalSeries = new LineSeries { Title="TOTAL", Color = OxyColors.Teal };
            RepublicanSeries = new LineSeries { Title="REP", Color = OxyColors.Red };
            DemocratSeries = new LineSeries { Title = "DEM", Color = OxyColors.Blue };
            GreenSeries = new LineSeries { Title = "GRN", Color = OxyColors.Green };
            LibertarianSeries = new LineSeries { Title = "LIB", Color = OxyColors.Yellow };
            ConsititutionSeries = new LineSeries { Title = "CON", Color = OxyColors.Orange };
            IndependentSeries = new LineSeries { Title = "IND", Color = OxyColors.CornflowerBlue };
            BenfordSeries = new LineSeries { Title = "BENFORDS", Color = OxyColors.Black };

            BenfordGraphModel.Series.Add(TotalSeries);
            BenfordGraphModel.Series.Add(RepublicanSeries);
            BenfordGraphModel.Series.Add(DemocratSeries);
            BenfordGraphModel.Series.Add(GreenSeries);
            BenfordGraphModel.Series.Add(LibertarianSeries);
            BenfordGraphModel.Series.Add(ConsititutionSeries);
            BenfordGraphModel.Series.Add(IndependentSeries);
            BenfordGraphModel.Series.Add(BenfordSeries);
        }

        /// <summary>
        /// clearing our points before re-plotting new graph
        /// </summary>
        private void ClearGraph()
        {
            TotalSeries.Points.Clear();
            RepublicanSeries.Points.Clear();
            DemocratSeries.Points.Clear();
            GreenSeries.Points.Clear();
            LibertarianSeries.Points.Clear();
            ConsititutionSeries.Points.Clear();
            IndependentSeries.Points.Clear();
            BenfordSeries.Points.Clear();
            BenfordGraphModel.InvalidatePlot(true);
        }

        /// <summary>
        /// clearing plot data before inputing new year algorithm
        /// </summary>
        /// 
        private void LoadBenfordData()
        {
            if(!LoadCheck()) return;

            ClearGraph();

            BenfordsLaw.ClearBenfordArrays();
            BenfordsLaw.LoadBenfordArrays(VoterPrecincts, int.Parse(Algorithm));
            for (int i = 0; i < 9; i++)
            {
                if (GraphTotal)
                    TotalSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.BenfordsTotals[i]));
                if (GraphRep)
                    RepublicanSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.BenfordsRepublican[i]));
                if (GraphDem)
                    DemocratSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.BenfordsDemocrat[i]));
                if (GraphGrn)
                    GreenSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.BenfordsGreen[i]));
                if (GraphLib)
                    LibertarianSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.BenfordsLibertarian[i]));
                if (GraphCon)
                    ConsititutionSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.BenfordsConstitution[i]));
                if (GraphInd)
                    IndependentSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.BenfordsIndependent[i]));

                BenfordSeries.Points.Add(new DataPoint((i + 1), BenfordsLaw.GetBenfordCurveForPrecinctTotal((i + 1), VoterPrecincts.Count)));
            }

            LoadPartyVoteTotals();
        }

        /// <summary>
        /// loading totals for each party in all precincts
        /// </summary>
        private void LoadPartyVoteTotals()
        {
            TotalCount = BenfordsLaw.TotalParyVotes["Total"].ToString();
            RepCount = BenfordsLaw.TotalParyVotes["Rep"].ToString();
            DemCount = BenfordsLaw.TotalParyVotes["Dem"].ToString();
            GrnCount = BenfordsLaw.TotalParyVotes["Grn"].ToString();
            LibCount = BenfordsLaw.TotalParyVotes["Lib"].ToString();
            ConCount = BenfordsLaw.TotalParyVotes["Con"].ToString();
            IndCount = BenfordsLaw.TotalParyVotes["Ind"].ToString();
            OnPropertyChanged();
        }
    }
}

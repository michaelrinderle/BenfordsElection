/*
		  __ _/| _/. _  ._/__ /
		_\/_// /_///_// / /_|/
				   _/
		sof digital 2020 (mit license)
		written by michael rinderle <michael@sofdigital.net>
*/

using BenfordsElection.ViewModels;
using System.Windows;

namespace BenfordsElection.Views
{
    public partial class BenfordGraphWindow : Window
    {
        private BenfordGraphViewModel vm { get; set;}
        public BenfordGraphWindow()
        {
            InitializeComponent();
            this.DataContext = vm = new BenfordGraphViewModel();
        }
    }
}

/*
		  __ _/| _/. _  ._/__ /
		_\/_// /_///_// / /_|/
				   _/
		sof digital 2020 (mit license)
		written by michael rinderle <michael@sofdigital.net>
*/

namespace BenfordsElection.Models
{
    public class VoterPrecinct
    {
        public string CountyName { get; set; }
        public string CountyLabel { get; set; }
        public int Persons { get; set; }
        public int PersonsOver18 { get; set; }

        public int PresidentVote2020 { get; set; }
        public int RepublicanVote2020 { get; set; }
        public int DemocratVote2020 { get; set; }
        public int GreenVote2020 { get; set; }
        public int LibertarianVote2020 { get; set; }
        public int ConstitutionVote2020 { get; set; }
        public int IndependentVote2020 { get; set; }

        public int PresidentVote2016 { get; set; }
        public int RepublicanVote2016 { get; set; }
        public int DemocratVote2016 { get; set; }
        public int GreenVote2016 { get; set; }
        public int LibertarianVote2016 { get; set; }
        public int ConstitutionVote2016 { get; set; }
        public int IndependentVote2016 { get; set; }

        public int PresidentVote2012 { get; set; }
        public int RepublicanVote2012 { get; set; }
        public int DemocratVote2012 { get; set; }
        public int GreenVote2012 { get; set; }
        public int LibertarianVote2012 { get; set; }
        public int ConstitutionVote2012 { get; set; }
        public int IndependentVote2012 { get; set; }
    }
}

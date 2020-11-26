using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenfordsElection.Models
{
    public class BenfordsLaw
    {
        int Year { get; set;} = 2016;

        /// <summary>
        /// holds 9 columns for each parties leading digit vote distribution
        /// </summary>
        public int[] BenfordsTotals = new int[9];
        public int[] BenfordsRepublican = new int[9];
        public int[] BenfordsDemocrat = new int[9];
        public int[] BenfordsGreen = new int[9];
        public int[] BenfordsLibertarian = new int[9];
        public int[] BenfordsConstitution = new int[9];
        public int[] BenfordsIndependent = new int[9];

        public Dictionary<string, int> TotalParyVotes = new Dictionary<string, int>();

        /// <summary>
        /// clearing our benford arrays to plot a different year on graph
        /// </summary>
        public void ClearBenfordArrays()
        {
           
            BenfordsTotals = new int[9];
            BenfordsRepublican = new int[9];
            BenfordsDemocrat = new int[9];
            BenfordsGreen = new int[9];
            BenfordsLibertarian = new int[9];
            BenfordsConstitution = new int[9];
            BenfordsIndependent = new int[9];
        }

        /// <summary>
        /// iterating over our list of precincts and dumping leading 
        /// digit totals in to respective benford arrays/columns
        /// </summary>
        public void LoadBenfordArrays(List<VoterPrecinct> voterPrecincts, int year)
        {
            TotalParyVotes = new Dictionary<string, int>();
            TotalParyVotes.Add("Total", 0);
            TotalParyVotes.Add("Rep", 0);
            TotalParyVotes.Add("Dem", 0);
            TotalParyVotes.Add("Grn", 0);
            TotalParyVotes.Add("Lib", 0);
            TotalParyVotes.Add("Con", 0);
            TotalParyVotes.Add("Ind", 0);

            foreach (var vp in voterPrecincts)
            {
                switch (year)
                {
                    case 2020:
                    {
                        TotalParyVotes["Total"] += vp.PresidentVote2020;
                        TotalParyVotes["Rep"] += vp.RepublicanVote2020;
                        TotalParyVotes["Dem"] += vp.DemocratVote2020;
                        TotalParyVotes["Grn"] += vp.GreenVote2020;
                        TotalParyVotes["Lib"] += vp.LibertarianVote2020;
                        TotalParyVotes["Con"] += vp.ConstitutionVote2020;
                        TotalParyVotes["Ind"] += vp.IndependentVote2020;
                        BenfordsTotals[RetrieveLeadDigit(vp.PresidentVote2020)]++;
                        BenfordsRepublican[RetrieveLeadDigit(vp.RepublicanVote2020)]++;
                        BenfordsDemocrat[RetrieveLeadDigit(vp.DemocratVote2020)]++;
                        BenfordsGreen[RetrieveLeadDigit(vp.GreenVote2020)]++;
                        BenfordsLibertarian[RetrieveLeadDigit(vp.LibertarianVote2020)]++;
                        BenfordsConstitution[RetrieveLeadDigit(vp.ConstitutionVote2020)]++;
                        BenfordsIndependent[RetrieveLeadDigit(vp.IndependentVote2020)]++;
                        break;
                    }
                    case 2016:
                    {
                        TotalParyVotes["Total"] += vp.PresidentVote2016;
                        TotalParyVotes["Rep"] += vp.RepublicanVote2016;
                        TotalParyVotes["Dem"] += vp.DemocratVote2016;
                        TotalParyVotes["Grn"] += vp.GreenVote2016;
                        TotalParyVotes["Lib"] += vp.LibertarianVote2016;
                        TotalParyVotes["Con"] += vp.ConstitutionVote2016;
                        TotalParyVotes["Ind"] += vp.IndependentVote2016;
                        BenfordsTotals[RetrieveLeadDigit(vp.PresidentVote2016)]++;
                        BenfordsRepublican[RetrieveLeadDigit(vp.RepublicanVote2016)]++;
                        BenfordsDemocrat[RetrieveLeadDigit(vp.DemocratVote2016)]++;
                        BenfordsGreen[RetrieveLeadDigit(vp.GreenVote2016)]++;
                        BenfordsLibertarian[RetrieveLeadDigit(vp.LibertarianVote2016)]++;
                        BenfordsConstitution[RetrieveLeadDigit(vp.ConstitutionVote2016)]++;
                        BenfordsIndependent[RetrieveLeadDigit(vp.IndependentVote2016)]++;
                        break;
                    }
                    case 2012:
                    default:
                    {
                        TotalParyVotes["Total"] += vp.PresidentVote2012;
                        TotalParyVotes["Rep"] += vp.RepublicanVote2012;
                        TotalParyVotes["Dem"] += vp.DemocratVote2012;
                        TotalParyVotes["Grn"] += vp.GreenVote2012;
                        TotalParyVotes["Lib"] += vp.LibertarianVote2012;
                        TotalParyVotes["Con"] += vp.ConstitutionVote2012;
                        TotalParyVotes["Ind"] += vp.IndependentVote2012;
                        BenfordsTotals[RetrieveLeadDigit(vp.PresidentVote2012)]++;
                        BenfordsRepublican[RetrieveLeadDigit(vp.RepublicanVote2012)]++;
                        BenfordsDemocrat[RetrieveLeadDigit(vp.DemocratVote2012)]++;
                        BenfordsGreen[RetrieveLeadDigit(vp.GreenVote2012)]++;
                        BenfordsLibertarian[RetrieveLeadDigit(vp.LibertarianVote2012)]++;
                        BenfordsConstitution[RetrieveLeadDigit(vp.ConstitutionVote2012)]++;
                        BenfordsIndependent[RetrieveLeadDigit(vp.IndependentVote2012)]++;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// taking total value of precincts and
        /// getting getting int for benford probability 
        /// </summary>
        public int GetBenfordCurveForPrecinctTotal(int increment, int value)
        {
            switch (increment)
            {
                case 1:
                    return (int)(value * .3010);
                case 2:
                    return (int)(value * .1761);
                case 3:
                    return (int)(value * .1249);
                case 4:
                    return (int)(value * .0969);
                case 5:
                    return (int)(value * .0790);
                case 6:
                    return (int)(value * .0670);
                case 7:
                    return (int)(value * .0580);
                case 8:
                    return (int)(value * .0512);
                default:
                    return (int)(value * .0458);
            }
        }

        /// <summary>
        /// retrieving leading digit, 
        /// decrementing to fit in our individual benford arrays/columns
        /// </summary>
        private int RetrieveLeadDigit(int i)
        {
            int ld = int.Parse(i.ToString().Substring(0, 1));
            if (ld == 0 || ld == 1)
                return 0;
            else
                return ld - 1;
        }
    }
}

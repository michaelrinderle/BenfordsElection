/*
		  __ _/| _/. _  ._/__ /
		_\/_// /_///_// / /_|/
				   _/
		sof digital 2020 (mit license)
		written by michael rinderle <michael@sofdigital.net>
*/

using BenfordsElection.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BenfordsElection.Utility
{
    public static class CsvParser
    {
		public static List<VoterPrecinct> Parse(string fileName)
        {
            List<VoterPrecinct> voterPrecincts = new List<VoterPrecinct>();
            // open up streamreader on csv file
            var sr = new StreamReader(fileName);
            // split csv header columns for parsing
            string[] headers = sr.ReadLine().Split(",");
            // obtain header column positions
            int countyIndex = Array.FindIndex(headers, row => row.Contains("CNTY_NAME"));
            int countyLabelIndex = Array.FindIndex(headers, row => row.Contains("LABEL"));
            int personsIndex = Array.FindIndex(headers, row => row.Contains("PERSONS"));
            int personsOver18Index = Array.FindIndex(headers, row => row.Contains("PERSONS18"));
            // obtain 2020 election result columns
            int total2020Index = Array.FindIndex(headers, row => row.Contains("PRETOT20"));
            int rep2020Index = Array.FindIndex(headers, row => row.Contains("PREREP20"));
            int dem2020Index = Array.FindIndex(headers, row => row.Contains("PREDEM20"));
            int grn2020Index = Array.FindIndex(headers, row => row.Contains("PREGRN20"));
            int lib2020Index = Array.FindIndex(headers, row => row.Contains("PRELIB20"));
            int con2020Index = Array.FindIndex(headers, row => row.Contains("PRECON20"));
            int ind2020Index = Array.FindIndex(headers, row => row.Contains("PREIND20"));
            // obtain 2016 election result columns
            int total2016Index = Array.FindIndex(headers, row => row.Contains("PRETOT16"));
            int rep2016Index = Array.FindIndex(headers, row => row.Contains("PREREP16"));
            int dem2016Index = Array.FindIndex(headers, row => row.Contains("PREDEM16"));
            int grn2016Index = Array.FindIndex(headers, row => row.Contains("PREGRN16"));
            int lib2016Index = Array.FindIndex(headers, row => row.Contains("PRELIB16"));
            int con2016Index = Array.FindIndex(headers, row => row.Contains("PRECON16"));
            int ind2016Index = Array.FindIndex(headers, row => row.Contains("PREIND16"));
            // obtain 2012 election result columns
            int total2012Index = Array.FindIndex(headers, row => row.Contains("PRETOT12"));
            int rep2012Index = Array.FindIndex(headers, row => row.Contains("PREREP12"));
            int dem2012Index = Array.FindIndex(headers, row => row.Contains("PREDEM12"));

            // note : these columns are missing from wi open election data results
            // int grn2012Index = Array.FindIndex(headers, row => row.Contains("PREGRN12"));
            // int lib2012Index = Array.FindIndex(headers, row => row.Contains("PRELIB12"));

            int con2012Index = Array.FindIndex(headers, row => row.Contains("PRECON12"));
            int ind2012Index = Array.FindIndex(headers, row => row.Contains("PREIND12"));

            // create a list of precinct objects with wi open election data
            string line;
            while((line = sr.ReadLine()) != null)
            {
                try
                {
                    string[] columns = line.Split(",");
                    var voterPrecinct = new VoterPrecinct();

                    voterPrecinct.CountyName = columns[countyIndex];
                    voterPrecinct.CountyLabel = columns[countyLabelIndex];
                    voterPrecinct.Persons = int.Parse(columns[personsIndex]);
                    voterPrecinct.PersonsOver18 = int.Parse(columns[personsOver18Index]);

                    // 2020 election data is not available yet, check before loading 
                    if (total2020Index != -1)
                    {
                        voterPrecinct.PresidentVote2020 =  int.Parse(columns[total2020Index]);
                        voterPrecinct.RepublicanVote2020 =  int.Parse(columns[rep2020Index]);
                        voterPrecinct.DemocratVote2020 =  int.Parse(columns[dem2020Index]);
                        voterPrecinct.GreenVote2020 =  int.Parse(columns[grn2020Index]);
                        voterPrecinct.LibertarianVote2020 =  int.Parse(columns[lib2020Index]);
                        voterPrecinct.ConstitutionVote2020 =  int.Parse(columns[con2020Index]);
                        voterPrecinct.IndependentVote2020 =  int.Parse(columns[ind2020Index]);
                    }

                    voterPrecinct.PresidentVote2016 =  int.Parse(columns[total2016Index]);
                    voterPrecinct.RepublicanVote2016 =  int.Parse(columns[rep2016Index]);
                    voterPrecinct.DemocratVote2016 =  int.Parse(columns[dem2016Index]);
                    voterPrecinct.GreenVote2016 =  int.Parse(columns[grn2016Index]);
                    voterPrecinct.LibertarianVote2016 =  int.Parse(columns[lib2016Index]);
                    voterPrecinct.ConstitutionVote2016 =  int.Parse(columns[con2016Index]);
                    voterPrecinct.IndependentVote2016 =  int.Parse(columns[ind2016Index]);

                    voterPrecinct.PresidentVote2012 =  int.Parse(columns[total2012Index]);
                    voterPrecinct.RepublicanVote2012 =  int.Parse(columns[rep2012Index]);
                    voterPrecinct.DemocratVote2012 =  int.Parse(columns[dem2012Index]);

                    // note : these columns are missing from wi open election data results
                    // voterPrecinct.GreenVote2012 =  int.Parse(columns[grn2012Index]);
                    // voterPrecinct.LibertarianVote2012 =  int.Parse(columns[lib2012Index]);

                    voterPrecinct.ConstitutionVote2012 =  int.Parse(columns[con2012Index]);
                    voterPrecinct.IndependentVote2012 =  int.Parse(columns[ind2012Index]);

                    voterPrecincts.Add(voterPrecinct);
                }
			    catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            return voterPrecincts;
        }
    }
}

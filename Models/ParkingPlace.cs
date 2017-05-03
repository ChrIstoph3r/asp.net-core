using System.Collections.Generic;
using InterviewTest.interfaces;

namespace InterviewTest.Models
{
    public class ParkingPlace : IModel
    {
        public string Dato { get; set; }
        public string Klokkeslett { get; set; }
        public string Sted   { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Antall_ledige_plasser { get; set; }
        public int? ProsentFull 
        { 
            get
            {
                double? IkkeLedigePlasser = Kapasitet - Antall_ledige_plasser; 
                double? prosent = (double)(IkkeLedigePlasser / (double)Kapasitet)*100;
                return (int?) prosent;
            } 
            set{} 
        }
        public int? Kapasitet {get; set;}
    }

}
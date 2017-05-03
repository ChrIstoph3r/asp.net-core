using System.Collections.Generic;
using InterviewTest.interfaces;

namespace InterviewTest.Models
{
    public class Capacity : IModel
    {
        public string Sted { get; set;}
        public int Kapasitet { get; set; }
    }
}
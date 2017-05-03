using System.Collections.Generic;
using InterviewTest.interfaces;

namespace InterviewTest.Models
{
    public class Prize : IModel
    {
        public int PrizeId { get; set; }
        public string Name { get; set; }

        public int? PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
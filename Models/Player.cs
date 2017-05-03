using System.Collections.Generic;
using InterviewTest.interfaces;

namespace InterviewTest.Models
{
    public class Player : IModel
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Prize> Prizes { get; set; }
    }

}
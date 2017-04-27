namespace RazorExercise.Models
{
    using System.Collections.Generic;

    public class AboutVm : IndexVm
    {
        public int NumberOfUsers { get; set; }
        public IEnumerable<string> Resources { get; set; }
    }
}
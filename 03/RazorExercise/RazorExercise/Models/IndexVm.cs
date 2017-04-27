namespace RazorExercise.Models
{
    using System.Collections.Generic;

    public class IndexVm : ViewModel
    {
		public IEnumerable<Person> People { get; set; }
    }
}
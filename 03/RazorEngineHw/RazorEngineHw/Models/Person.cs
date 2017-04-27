namespace RazorEngineHw.Models
{
    using System.ComponentModel;

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        [DisplayName("Subscribed")]
        public bool IsSubscribed { get; set; }
    }
}

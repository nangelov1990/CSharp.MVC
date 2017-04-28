namespace RazorEngineHw.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class Person
    {
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[Not Specified]")]
        public string Name { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[Not Specified]")]
        public int? Age { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[Not Specified]")]
        public string Email { get; set; }

        [DisplayName("Subscribed")]
        public string IsSubscribed {
            get
            {
                if (this.Subscribed == true)
                {
                    return "YES";
                }
                else
                {
                    return "NO";
                }
            }
        }

        [HiddenInput(DisplayValue = false)]
        public bool Subscribed { get; set; }
    }
}

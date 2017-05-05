namespace CameraBazaar.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Camera
    {
        [Key]
        public int Id { get; set; }

        public virtual User Owner { get; set; }

        public CameraMake Make { get; set; }

        [RegularExpression("^([A-Z0-9-]+/g)$")]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public byte Quantity { get; set; }

        [DisplayName("Minimum Shutter Speed")]
        [Range(1, 30)]
        public byte MinShutterSpeed { get; set; }

        [DisplayName("Maximum Shutter Speed")]
        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        [RegularExpression("^(50|100){1}$")]
        public byte MinIso { get; set; }

        [Range(200, 409600)]
        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        [MaxLength(15)]
        public string VideoResolution { get; set; }

        public IEnumerable<LigthMetering> LigthMetering { get; set; }

        [MaxLength(6000)]
        public string Description { get; set; }

        [RegularExpression(@"^[(http:\/\/|https:\/\/)]\w.+$")]
        public string ImageUrl { get; set; }
    }
}

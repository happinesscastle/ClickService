using System;

namespace ClickServerService.Models
{
    public class Access_Point
    {
        public int AP_ID { get; set; }
        public string AP_Name { get; set; }
        public string AP_IP { get; set; }
        public string AP_Mac { get; set; }
        public bool? AP_IsEnable { get; set; }
        public bool? AP_Status { get; set; }
        public int? ID_GameCenter { get; set; }
    }
}

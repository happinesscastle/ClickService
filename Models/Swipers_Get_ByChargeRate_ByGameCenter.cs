using System;

namespace ClickServerService.Models
{
    public class Swipers_Get_ByChargeRate_ByGameCenter
    {
        public int ID { get; set; }
        public int ID_GameCenter { get; set; }
        public string Title { get; set; }
        public string MacAddress { get; set; }
        public int? ID_Games { get; set; }
        public bool? State { get; set; }
        public string Dec { get; set; }
        public DateTime? DateStart { get; set; }
        public int? Price1 { get; set; }
        public int? Price2 { get; set; }
        public string Delay1 { get; set; }
        public string Delay2 { get; set; }
        public string Pulse { get; set; }
        public int? Config_State { get; set; }
        public string RepeatCount { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ID_Games_Class { get; set; }
        public int? PulseType { get; set; }
        public string Start_Count_Voltage { get; set; }
        public string Version { get; set; }
        public string TicketErrorStop { get; set; }
        public string PullUp { get; set; }
        public string PriceAdi { get; set; }
        public string PriceVije { get; set; }
        public bool? ClassStatus { get; set; }
        public bool? GroupsStatus { get; set; }
        public bool? SegmentStatus { get; set; }
    }
}

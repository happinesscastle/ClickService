namespace ClickServerService.Models
{
    public class ServerConfigView
    {
        public int AP_ID { get; set; }
        public int ID { get; set; }
        public int ID_GameCenter { get; set; }
        public string AP_Name { get; set; }
        public string AP_IP { get; set; }
        public int AP_Port { get; set; }
        public bool AP_IsEnable { get; set; }
        public bool AP_Status { get; set; }
        public int? ValidateReceivedData { get; set; }
        public string ServerIP { get; set; }
        public int? RepeatConfig { get; set; }
        public bool? IsShowAllReceive { get; set; }
        public bool? IsShowAllSend { get; set; }
        public bool? IsDecreasePriceInLevel2 { get; set; }
        public bool? IsEnableTimerSync { get; set; }
        public int? TimeSync { get; set; }
        public bool? IsRestart { get; set; }
        public string Ftp_UserName { get; set; }
        public string Ftp_Password { get; set; }
    }
}
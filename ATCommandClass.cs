using System;

namespace ClickServerService
{
    internal class ATCommandClass
    {
        #region ' Useless '

        public string Config1 = "AT+CFG1=" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
        public string Config2 = "AT+CFG2=12000,35000";
        public string Config3 = "AT+CFG3=DELAY1,DELAY2";
        public string Config4 = "AT+CFGI=ENABLE,PULSE";

        public string Get_Command(string Name)
        {
            try
            {
                string str1 = "";
                string str2 = Name;
                if (!(str2 == "Config1"))
                {
                    if (!(str2 == "Config2"))
                    {
                        if (!(str2 == "Config3"))
                        {
                            if (str2 == "Config4")
                                str1 = Config4.ToString();
                        }
                        else
                            str1 = Config3.ToString();
                    }
                    else
                        str1 = Config2.ToString();
                }
                else
                    str1 = Config1.ToString();
                return str1;
            }
            catch
            {
                return "";
            }
        }

        #endregion
    }
}

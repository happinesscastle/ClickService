// Decompiled with JetBrains decompiler
// Type: ClickServerService.ATCommandClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;

namespace ClickServerService
{
  internal class ATCommandClass
  {
    public string Config1 = "AT+CFG1=" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", (object) DateTime.Now);
    public string Config2 = "AT+CFG2=12000,35000";
    public string Config3 = "AT+CFG3=DELAY1,DELAY2";
    public string Config4 = "AT+CFGI=ENABLE,PULSE";

    public string Get_Command(string Name)
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
              str1 = this.Config4.ToString();
          }
          else
            str1 = this.Config3.ToString();
        }
        else
          str1 = this.Config2.ToString();
      }
      else
        str1 = this.Config1.ToString();
      return str1;
    }
  }
}

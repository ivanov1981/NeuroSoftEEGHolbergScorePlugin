using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuroSoftEEGHolbergScorePlugin.DataModels
{
    public static class NeurosoftReader
    {        

        public static String GetProgramFile(ExternalEegInterface.DataModel.Reader reader)
        {
            return reader.SettingString1;
        }

        public static void SetProgramFile(ExternalEegInterface.DataModel.Reader reader, String value)
        {
            reader.SettingString1 = value;
        }
        
    }
}

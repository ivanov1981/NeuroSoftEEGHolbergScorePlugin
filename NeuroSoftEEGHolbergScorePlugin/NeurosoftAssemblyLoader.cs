using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using NeuroSoftEEGHolbergScorePlugin.DataModels;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public static class NeurosoftAssemblyLoader
    {
        private static Boolean loaded;
        private static Assembly neuroSoftAssembly;

        public static void LoadIfNotLoaded(ExternalEegInterface.DataModel.Reader reader)
        {
            if (!loaded)
            {
                var programFilePath = DataModels.NeurosoftReader.GetProgramFile(reader);
                if (!string.IsNullOrEmpty(programFilePath))
                {
                    /*
                    var assemblyName = new AssemblyName
                        {
                            Name = "Neurosoft.EEG.WPF",
                            //Version = new Version(1, 4, 7, 0),
                            //CodeBase = programFilePath,
                            //CultureInfo = CultureInfo.InvariantCulture,
                            //KeyPair = null,                            
                        
                        };                                        
                    neuroSoftAssembly = Assembly.Load(assemblyName);
                     */
                    neuroSoftAssembly = Assembly.LoadFrom(programFilePath);
                    loaded = true;

                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                }
            }

        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {                        
            //if (args.Name.Equals("NeuroSoft.EEG.WPF, Version=1.4.7.0, Culture=neutral, PublicKeyToken=null"))
            if (args.Name.StartsWith("NeuroSoft.EEG.WPF"))
            {
                return neuroSoftAssembly;
            }
            else
            {
                return null;
            }
        }
    }
}

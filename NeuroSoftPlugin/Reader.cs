using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalEegInterface.DataModel;
using System.Runtime.InteropServices;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public class Reader : ExternalEegInterface.IExternalReader
    {
        private NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper helper = new NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper();
               

        public bool IsInstalled
        {
            get { return helper.IsInstalled; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRunning
        {
            get { return helper.IsRunning; }
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recording"></param>
        /// <returns></returns>
        public int? FindRecordingWindow(ExternalEegInterface.DataModel.Recording recording)
        {
            return helper.FindRecordingWindow(recording.ExternalId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <returns></returns>
        public ExternalEegInterface.DataModel.Montage GetDisplayedMontage(Recording recording)
        {
            Montage result = null;
            var viewCount = GetIndexOfEeg(recording);
            if (viewCount.HasValue)
                result = Converter.ToHolberg(helper.GetDisplayedMontage(viewCount.Value));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <returns></returns>
        public string GetFileName(int viewCount)
        {
            return helper.GetFileName(viewCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ExternalEegInterface.Location.Window GetMainWindow()
        {
            var window = Converter.ToHolberg(helper.GetMainWindow());            
            return window;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="p"></param>
        /// <param name="s"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public ExternalEegInterface.Location.Window GetMontagePortionWindow(Recording recording)
        {
            ExternalEegInterface.Location.Window result = null;
            var viewCount = FindRecordingWindow(recording);
            if (viewCount.HasValue)
            {
                result = Converter.ToHolberg(helper.GetMontagePortionWindow(viewCount.Value));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <returns></returns>
        public string GetStudyId(int viewCount)
        {
            return helper.GetStudyId(viewCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <returns></returns>
        public List<Tuple<ExternalEegInterface.DataModel.Sensor, ExternalEegInterface.DataModel.Sensor>> GetTraces(Recording recording)
        {
            var result = new List<Tuple<ExternalEegInterface.DataModel.Sensor, ExternalEegInterface.DataModel.Sensor>>();
            var viewCount = GetIndexOfEeg(recording);
            if (viewCount.HasValue)
            {
                var traces = helper.GetTraces(viewCount.Value);                
                foreach (var trace in traces)
                {
                    result.Add(new Tuple<ExternalEegInterface.DataModel.Sensor, ExternalEegInterface.DataModel.Sensor>(
                            Converter.ToHolberg(trace.Item1), Converter.ToHolberg(trace.Item2)));
                }
            }
            return result;
        }    


        /// <summary>
        /// 
        /// </summary>
        public void Shutdown()
        {
            helper.Shutdown();
        }


        public double GetReaderSensorOverlayBottomSpacingFactor()
        {
            return Constants.READERSENSOROVERLAYBOTTOMSPACINGFACTOR;
        }

        public double GetReaderSensorOverlayTopSpacingFactor()
        {
            return Constants.READERSENSOROVERLAYTOPSPACINGFACTOR;
        }


        public int IndexOfActiveEeg
        {
            get { return helper.ActiveView; }
        }

        public int NumberOfOpenEegs
        {
            get { return helper.ViewCount; }
        }        

        public int? GetIndexOfEeg(Recording recording)
        {
            return FindRecordingWindow(recording);
        }      

        public List<Event> GetEvents(Recording recordingToActivate)
        {
            var result = new List<Event>();
            var viewCount = FindRecordingWindow(recordingToActivate);
            if (viewCount != null)
            {
                var events = helper.GetEvents(viewCount.Value);
                result = events.Select(_event => Converter.ToHolberg(_event)).ToList();
            }
            return result;
             
        }

        public void GotoEvent(Recording recording, Event eventToGoTo)
        {
            var viewCount = FindRecordingWindow(recording);
            if (viewCount != null)
            {
                helper.GotoDateTime(viewCount.Value, eventToGoTo.Start);
            }
        }

        public ExternalEegInterface.Location.Window GetEegChildWindow(Recording r)
        {
            return GetMainWindow();                        
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public void ActivateWindow(Recording recordingToActivate)
        {
            var window = GetMainWindow();
            if (window != null)
            {
                ShowWindow(window.WindowHandle, 1);
                SetForegroundWindow(window.WindowHandle);
            }
        }        


        bool ExternalEegInterface.IExternalReader.FindRecordingWindow(Recording recording)
        {
            return FindRecordingWindow(recording) != null;
        }

        Recording ExternalEegInterface.IExternalReader.OpenRecording(Recording recording)
        {
            helper.OpenRecording(recording.ExternalId);
            return recording;
        }
    }   
}

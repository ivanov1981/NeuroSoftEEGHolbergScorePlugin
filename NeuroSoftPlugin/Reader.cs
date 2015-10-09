using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalEegInterface.DataModel;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public class Reader : ExternalEegInterface.IExternalReader
    {
        private NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper helper = new NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper();
        /// <summary>
        /// 
        /// </summary>
        public int ViewCount
        {
            get { return helper.ViewCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ActiveView
        {
            get { return helper.ActiveView; }
        }

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
        public ExternalEegInterface.DataModel.Montage GetDisplayedMontage(int viewCount)
        {
            return Converter.ToHolberg(helper.GetDisplayedMontage(viewCount));
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
            return Converter.ToHolberg(helper.GetMainWindow());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="p"></param>
        /// <param name="s"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public ExternalEegInterface.Location.Window GetMontagePortionWindow(int viewCount, ExternalEegInterface.DataModel.Patient p, ExternalEegInterface.DataModel.Study s, ExternalEegInterface.DataModel.Recording r)
        {
            return Converter.ToHolberg(helper.GetMontagePortionWindow(viewCount));
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
        public List<Tuple<ExternalEegInterface.DataModel.Sensor, ExternalEegInterface.DataModel.Sensor>> GetTraces(int viewCount)
        {
            var traces = helper.GetTraces(viewCount);
            var result = new List<Tuple<ExternalEegInterface.DataModel.Sensor, ExternalEegInterface.DataModel.Sensor>>();
            foreach (var trace in traces)
            {
                result.Add(new Tuple<ExternalEegInterface.DataModel.Sensor, ExternalEegInterface.DataModel.Sensor>(Converter.ToHolberg(trace.Item1), Converter.ToHolberg(trace.Item2)));
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
            get { throw new NotImplementedException(); }
        }

        public int NumberOfOpenEegs
        {
            get { throw new NotImplementedException(); }
        }        

        public int? GetIndexOfEeg(Recording recording)
        {
            throw new NotImplementedException();
        }      

        public List<Event> GetEvents(Recording recordingToActivate)
        {
            throw new NotImplementedException();
            /*
            var events = helper.GetEvents(viewCount);
            var result = new List<ExternalEegInterface.DataModel.Event>();
            foreach (var _event in events)
            {
                result.Add(Converter.ToHolberg(_event));
            }
            return result;
             */
        }

        public void GotoEvent(Recording recording, Event eventToGoTo)
        {
            throw new NotImplementedException();
            //helper.GotoDateTime(viewCount, eventToGoTo.Start);
        }

        public ExternalEegInterface.Location.Window GetEegChildWindow(Recording r)
        {
            throw new NotImplementedException();
        }

        public ExternalEegInterface.Location.Window GetMontagePortionWindow(Recording recording)
        {
            throw new NotImplementedException();
        }

        public void ActivateWindow(Recording recordingToActivate)
        {
            throw new NotImplementedException();
        }

        public Montage GetDisplayedMontage(Recording recordingToActivate)
        {
            throw new NotImplementedException();
        }

        public List<Tuple<Sensor, Sensor>> GetTraces(Recording recordingToActivate)
        {
            throw new NotImplementedException();
        }


        bool ExternalEegInterface.IExternalReader.FindRecordingWindow(Recording recording)
        {
            throw new NotImplementedException();
        }

        Recording ExternalEegInterface.IExternalReader.OpenRecording(Recording recording)
        {
            helper.OpenRecording(recording.ExternalId);
            return recording;
        }
    }
}

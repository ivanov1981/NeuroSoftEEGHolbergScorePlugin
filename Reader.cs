using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// <param name="viewCount"></param>
        /// <returns></returns>
        public List<ExternalEegInterface.DataModel.Event> GetEvents(int viewCount)
        {
            var events = helper.GetEvents(viewCount);
            var result = new List<ExternalEegInterface.DataModel.Event>();
            foreach (var _event in events)
            {
                result.Add(Converter.ToHolberg(_event));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="eventToChange"></param>
        /// <param name="text"></param>
        public void ChangeEventText(int viewCount, ExternalEegInterface.DataModel.Event eventToChange, string text)
        {
            helper.ChangeEventText(viewCount, Converter.ToNeuroSoft(eventToChange), text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="eventToDelete"></param>
        public void DeleteEvent(int viewCount, ExternalEegInterface.DataModel.Event eventToDelete)
        {
            helper.DeleteEvent(viewCount, Converter.ToNeuroSoft(eventToDelete));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewCount"></param>
        /// <param name="eventToInsert"></param>
        /// <param name="text"></param>
        public void InsertEvent(int viewCount, ExternalEegInterface.DataModel.Event eventToInsert, string text)
        {
            helper.InsertEvent(viewCount, Converter.ToNeuroSoft(eventToInsert), text);
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
        /// <param name="viewCount"></param>
        /// <param name="positionDateTime"></param>
        public void GotoDateTime(int viewCount, DateTime positionDateTime)
        {
            helper.GotoDateTime(viewCount, positionDateTime);
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recording"></param>
        public void OpenRecording(ExternalEegInterface.DataModel.Recording recording)
        {
            helper.OpenRecording(recording.ExternalId);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Shutdown()
        {
            helper.Shutdown();
        }        
    }
}

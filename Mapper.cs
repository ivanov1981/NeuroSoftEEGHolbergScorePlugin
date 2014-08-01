using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalEegInterface.DataModel;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public class Mapper : ExternalEegInterface.Mapping.IMapper
    {
        public List<EventType> GetEventTypes(Database database)
        {
            var eventTypes = NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper.GetEventTypes();
            var result = new List<EventType>(from e in eventTypes select Converter.ToHolberg(e));
            return result;
        }

        public List<Gender> GetGenders(Database database)
        {            
            var genders = NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper.GetGenders();
            var result = new List<Gender>(from g in genders select Converter.ToHolberg(g));
            return result;
        }

        public List<Montage> GetMontages(Database database)
        {
            var montages = NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper.GetMontages();
            var result = new List<Montage>(from m in montages select Converter.ToHolberg(m));
            return result;
        }

        public List<SensorChannel> GetSensorChannels(Database database)
        {
            var sensorChannels = NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper.GetSensorChannels();
            var result = new List<SensorChannel>(from s in sensorChannels select Converter.ToHolberg(s));
            return result;            
        }

        public List<Sensor> GetSensors(Database database)
        {
            var sensors = NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper.GetSensors();
            var result = new List<Sensor>(from s in sensors select Converter.ToHolberg(s));
            return result;
        }

        public List<StudyType> GetStudyTypes(Database database)
        {
            var studyTypes = NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper.GetStudyTypes();
            var result = new List<StudyType>(from s in studyTypes select Converter.ToHolberg(s));
            return result;
        }
    }
}

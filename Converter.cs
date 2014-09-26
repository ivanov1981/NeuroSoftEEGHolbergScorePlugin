using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalEegInterface.DataModel;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public static class Converter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static Gender ToHolberg(NeuroSoft.EEG.WPF.Holberg.Gender gender)
        {
            return new Gender() { ExternalId = gender.Id, Name = gender.Name };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static NeuroSoft.EEG.WPF.Holberg.Gender ToNeuroSoft(Gender gender)
        {
            int id = 0;
            Int32.TryParse(gender.ExternalId, out id);
            return new NeuroSoft.EEG.WPF.Holberg.Gender(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="montage"></param>
        /// <returns></returns>
        public static Montage ToHolberg(NeuroSoft.EEG.WPF.Holberg.Montage montage)
        {
            if (montage == null)
                return null;
            return new Montage() { ExternalId = montage.Id, Name = montage.Name };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="montage"></param>
        /// <returns></returns>
        public static NeuroSoft.EEG.WPF.Holberg.Montage ToNeuroSoft(Montage montage)
        {
            Guid id = new Guid();
            Guid.TryParse(montage.ExternalId, out id);
            return new NeuroSoft.EEG.WPF.Holberg.Montage(id, montage.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public static EventType ToHolberg(NeuroSoft.EEG.WPF.Holberg.EventType eventType)
        {
            return new EventType() { ExternalId = eventType.Id, Name = eventType.Name };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public static NeuroSoft.EEG.WPF.Holberg.EventType ToNeuroSoft(EventType eventType)
        {
            int id = 0;
            Int32.TryParse(eventType.ExternalId, out id);
            return new NeuroSoft.EEG.WPF.Holberg.EventType(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static Event ToHolberg(NeuroSoft.EEG.WPF.Holberg.Event _event)
        {
            return new Event() { ExternalId = _event.Id, EventType = ToHolberg(_event.EventType), Start = _event.Start, Stop = _event.Stop, Text = _event.Text };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static NeuroSoft.EEG.WPF.Holberg.Event ToNeuroSoft(Event _event)
        {
            return new NeuroSoft.EEG.WPF.Holberg.Event(_event.ExternalId, ToNeuroSoft(_event.EventType), _event.Start, _event.Stop) { Text = _event.Text };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public static Sensor ToHolberg(NeuroSoft.EEG.WPF.Holberg.Sensor sensor)
        {
            return new Sensor() { ExternalId = sensor.Id, Name = sensor.Name };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public static NeuroSoft.EEG.WPF.Holberg.Sensor ToNeuroSoft(Sensor sensor)
        {            
            return new NeuroSoft.EEG.WPF.Holberg.Sensor(sensor.ExternalId, sensor.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensorChannel"></param>
        /// <returns></returns>
        public static SensorChannel ToHolberg(NeuroSoft.EEG.WPF.Holberg.SensorChannel sensorChannel)
        {
            return new SensorChannel() 
            {
                ExternalId = sensorChannel.Id, 
                Name = sensorChannel.Name, 
                Active = ToHolberg(sensorChannel.ActiveSensor),
                Reference = ToHolberg(sensorChannel.ReferenceSensor),
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensorChannel"></param>
        /// <returns></returns>
        public static NeuroSoft.EEG.WPF.Holberg.SensorChannel ToNeuroSoft(SensorChannel sensorChannel)
        {
            int id = 0;
            Int32.TryParse(sensorChannel.ExternalId, out id);
            return new NeuroSoft.EEG.WPF.Holberg.SensorChannel(id, ToNeuroSoft(sensorChannel.Active), ToNeuroSoft(sensorChannel.Reference));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studyType"></param>
        /// <returns></returns>
        public static StudyType ToHolberg(NeuroSoft.EEG.WPF.Holberg.StudyType studyType)
        {
            return new StudyType()
            {
                ExternalId = studyType.Id,
                Name = studyType.Name,                
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studyType"></param>
        /// <returns></returns>
        public static NeuroSoft.EEG.WPF.Holberg.StudyType ToNeuroSoft(StudyType studyType)
        {
            NeuroSoft.EEG.WPF.Holberg.StudyTypeEnum id = NeuroSoft.EEG.WPF.Holberg.StudyTypeEnum.StandartEEG;
            Enum.TryParse(studyType.ExternalId, out id);            
            return new NeuroSoft.EEG.WPF.Holberg.StudyType(id);
        }

        /// <summary>
        /// 
        /// </summary>     
        /// <returns></returns>
        public static Study ToHolberg(NeuroSoft.EEG.WPF.Holberg.Study externalStudy, Patient parentPatient = null)
        {
            var study = new Study()
            {
                ExternalId = externalStudy.Id,
                StudyType = Converter.ToHolberg(externalStudy.StudyType),
                StudyStart = externalStudy.StudyStart,
                StudyLength = externalStudy.StudyLength,
                StudyStop = externalStudy.StudyStart.Add(externalStudy.StudyLength),
                Patient = parentPatient
            };

            var recording = new Recording() { ExternalId = externalStudy.Id, Start = study.StudyStart, Stop = study.StudyStop, Study = study, FileName = "DUMMY" };            
            study.Recordings.Add(recording);
            return study;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Patient ToHolberg(NeuroSoft.EEG.WPF.Holberg.Patient externalPatient)
        {
            var result = new Patient()
            {
                ExternalId = externalPatient.Id,
                DateOfBirth = externalPatient.DateOfBirth,
                FirstName = externalPatient.FirstName,
                LastName = externalPatient.LastName,
                Gender = ToHolberg(externalPatient.Gender),      
                MotherName = string.Empty,
                IdentityString = externalPatient.IdentityString
            };

            foreach (var study in externalPatient.Studies)
            {
                result.Studies.Add(ToHolberg(study, result));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ExternalEegInterface.Location.Window ToHolberg(NeuroSoft.EEG.WPF.Holberg.HolbergWindow window)
        {
            if (window == null)
                return null;
            ExternalEegInterface.Location.Window converted = new ExternalEegInterface.Location.Window()
            {
                Height = window.Height,
                IsForeground = window.IsForeground,
                Visible = window.Visible,
                X = window.X,
                Y = window.Y
            };

            return converted;
        }
    }
}

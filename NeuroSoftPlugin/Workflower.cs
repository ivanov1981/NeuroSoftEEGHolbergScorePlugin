using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public class Workflower : ExternalEegInterface.Workflow.IExternalReaderWorkflow
    {
        private ExternalEegInterface.DataModel.Database database;
        private NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper helper = new NeuroSoft.EEG.WPF.Holberg.HolbergScoreHelper();
        public Workflower(ExternalEegInterface.DataModel.Database database)
        {
            this.database = database;
        }

        public int GetImportCount(ExternalEegInterface.DataModel.Database database, DateTime fromDate, DateTime toDate)
        {
            return helper.GetImportCount(fromDate, toDate);
        }

        public ExternalEegInterface.DataModel.Study GetImportNext()
        {
            var externalStudy = helper.GetImportNext();            
            var patient = Converter.ToHolberg(externalStudy.Patient);
            var study = Converter.ToHolberg(externalStudy);
            study.Patient = patient;
            CleanupObjectGraph(study);
            return study;
        }

        private static void CleanupObjectGraph(ExternalEegInterface.DataModel.Study study)
        {            
            study.Patient.Studies = null;
            foreach (var recording in study.Recordings)
            {
                recording.Study = null;
                recording.ExternalId2 = "";
            }
        }

        public ExternalEegInterface.DataModel.Study RefreshStudy(ExternalEegInterface.DataModel.Study study)
        {
            var externalStudy = helper.GetStudyById(study.ExternalId);
            var patient = Converter.ToHolberg(externalStudy.Patient);
            var _study = Converter.ToHolberg(externalStudy);
            _study.Patient = patient;            
            return _study;
        }
    }
}

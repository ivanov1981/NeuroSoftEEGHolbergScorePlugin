using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalEegInterface;
using ExternalEegInterface.Admin;
using ExternalEegInterface.DataModel;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public class NeuroSoftEEG : IExternalEegSystem
    {

        public void OnPluginLoaded(ExternalEegInterface.DataModel.Reader reader)
        {            
            NeurosoftAssemblyLoader.LoadIfNotLoaded(reader);            
        }

        public void OnReaderChanged(ExternalEegInterface.DataModel.Reader reader)
        {
            NeurosoftAssemblyLoader.LoadIfNotLoaded(reader);
        }

        public ExternalEegInterface.Admin.IAdminDatabaseEditor GetAdminDatabaseEditor(ExternalEegInterface.DataModel.Database database)
        {
            return new NeuroSoftEEGHolbergScorePlugin.Presenters.AdminDatabaseEditPresenter(database);
        }

        public ExternalEegInterface.Admin.IAdminReaderEditor GetAdminReaderEditor(ExternalEegInterface.DataModel.Reader reader)
        {
            return new NeuroSoftEEGHolbergScorePlugin.Presenters.AdminReaderEditPresenter(reader); ;
        }

        public ExternalEegInterface.Mapping.IMapper GetMapper()
        {
            return new Mapper();
        }

        public IExternalReader GetReader(ExternalEegInterface.DataModel.Reader reader, ExternalEegInterface.DataModel.Database database)
        {
            return new Reader();
        }

        private Dictionary<Database, Workflower> workflowers = new Dictionary<Database, Workflower>();
        public Dictionary<Database, Workflower> Workflowers
        {
            get { return workflowers; }            
        }

        public ExternalEegInterface.Workflow.IExternalReaderWorkflow GetWorkflower(Database database)
        {
            Workflower result;
            var key = Workflowers.Keys.FirstOrDefault(k => k.DatabaseId == database.DatabaseId);
            if (key != null)
                result = Workflowers[key];
            else
            {
                result = new Workflower(database);
                Workflowers.Add(database, result);
            }
            return result;
        }

        public bool HasWorkflowIntegration
        {
            get { return true; }
        }

        public string Name
        {
            get { return "NeuroSoftEEG"; }
        }


        public bool UsesHolbergLocationOverlay
        {
            get { return false; }
        }

        public bool CanAcceptReports
        {
            get { return false; }
        }

        public List<ReportFormatEnum> AccetableReportFormats
        {
            get { return new List<ReportFormatEnum>(); }
        }

        public Exception SendFinalReport(Study study, byte[] dataBytes, ReportFormatEnum type)
        {
            throw new NotImplementedException();
        }
    }
}

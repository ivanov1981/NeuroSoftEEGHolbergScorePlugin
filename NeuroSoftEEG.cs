using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalEegInterface;
using ExternalEegInterface.DataModel;

namespace NeuroSoftEEGHolbergScorePlugin
{
    public class NeuroSoftEEG : IExternalEegSystem
    {
        public ExternalEegInterface.Admin.IAdminDatabaseEditor GetAdminDatabaseEditor(ExternalEegInterface.DataModel.Database database)
        {
            return null;
        }

        public ExternalEegInterface.Admin.IAdminReaderEditor GetAdminReaderEditor(ExternalEegInterface.DataModel.Reader reader)
        {
            return null;
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
    }
}

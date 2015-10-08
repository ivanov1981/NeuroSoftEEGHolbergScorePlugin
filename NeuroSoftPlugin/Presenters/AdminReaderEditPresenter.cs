using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ExternalEegInterface.Admin;
using ExternalEegInterface.DataModel;
using ExternalEegInterface;
using System.Windows.Controls;
using NeuroSoftEEGHolbergScorePlugin.Controls;
using NeuroSoftEEGHolbergScorePlugin.DataModels;

namespace NeuroSoftEEGHolbergScorePlugin.Presenters
{
       
    public class AdminReaderEditPresenter : ExternalEegInterface.Admin.IAdminReaderEditor
    {
        #region Fields
        private bool setOnceSet;
        public event CloseHandler Close;
        public event SaveReaderHandler Save;
        ExternalEegInterface.DataModel.Reader reader;        
        AdminReaderEditControl view;
        CommandBinding saveBinding;
        RoutedCommand saveCommand;
        CommandBinding cancelBinding;
        RoutedCommand cancelCommand;
        Boolean somethingChanged = false;        
        #endregion

        #region Constructors

        public AdminReaderEditPresenter(ExternalEegInterface.DataModel.Reader reader)
        {
            this.reader = reader;
            view = new AdminReaderEditControl();
            view.Cancel.Visibility = System.Windows.Visibility.Visible;
            SetupFields();
            BindCommands();
            WireEvents();
        }
                
        private void WireEvents()
        {
            view.ProgramFile.TextChanged += TextChanged;
            view.ProgramFileBrowse.Click += ProgramFileBrowse_Click;            
        }        

        #endregion

        public UserControl View 
        { 
            get 
            { 
                return (UserControl) view; 
            } 
        }

        public ExternalEegInterface.DataModel.Reader Reader
        {
            get
            {
                return reader;
            }            
        }        

        private void SetupFields()
        {
            view.ProgramFile.Text = NeurosoftReader.GetProgramFile(reader);            
        }

        private Boolean CanSave()
        {
            return somethingChanged && view.ProgramFile.Text.Length > 0;
        }
        
        private void CopyFromGuiToDatabase()
        {
            NeurosoftReader.SetProgramFile(reader, view.ProgramFile.Text);
        }
        
        
        void ProgramFileBrowse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = Constants.EEGSYSTEM_APPLICATION_FILENAME; // Default file name 
            dlg.DefaultExt = ".exe"; // Default file extension 
            dlg.Filter = "Program files (.exe)|*.exe"; // Filter files by extension 

            if (view.ProgramFile.Text.Length == 0)
            {
                dlg.InitialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), Constants.EEGSYSTEM_PROGRAMFILES_SUBFOLDER);
                dlg.FileName = Constants.EEGSYSTEM_APPLICATION_FILENAME;
            }

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                view.ProgramFile.Text = dlg.FileName;
                somethingChanged = true;
            }
        }

        void TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {            
            somethingChanged = true;
        }

        private void BindCommands()
        {
            saveCommand = new RoutedCommand();
            saveBinding = new CommandBinding(saveCommand, save_Execute, save_CanExecute);
            view.CommandBindings.Add(saveBinding);
            view.Save.Command = saveCommand;

            cancelCommand = new RoutedCommand();
            cancelBinding = new CommandBinding(cancelCommand, cancel_Execute, cancel_CanExecute);
            view.CommandBindings.Add(cancelBinding);
            view.Cancel.Command = cancelCommand;
        }

        void save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Boolean canSave = true;            
            canSave = CanSave();            

            if (canSave)
                view.Save.Visibility = System.Windows.Visibility.Visible;
            else
                view.Save.Visibility = System.Windows.Visibility.Collapsed;

            e.CanExecute = canSave;
        }
        
        void save_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            CopyFromGuiToDatabase();

            if (Save != null)
                Save(reader);

            if (Close != null)
                Close();
        }

        void cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {         
            e.CanExecute = true;
        }

        void cancel_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (Close != null)
                Close();
        }

    }
}

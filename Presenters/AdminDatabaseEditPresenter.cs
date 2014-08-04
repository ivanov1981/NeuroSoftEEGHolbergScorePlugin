using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ExternalEegInterface.Admin;
using ExternalEegInterface.DataModel;
using ExternalEegInterface;
using System.Windows.Controls;
using NeuroSoftEEGHolbergScorePlugin.DataModels;
using NeuroSoftEEGHolbergScorePlugin.Controls;

namespace NeuroSoftEEGHolbergScorePlugin.Presenters
{
       
    public class AdminDatabaseEditPresenter : ExternalEegInterface.Admin.IAdminDatabaseEditor
    {
        #region Fields        
        public event CloseHandler Close;
        public event SaveDatabaseHandler Save;
        Database database;        
        private AdminDatabaseEditControl view;
        CommandBinding saveBinding;
        RoutedCommand saveCommand;
        CommandBinding cancelBinding;
        RoutedCommand cancelCommand;
        Boolean somethingChanged = false;
        #endregion

        #region Constructors

        public AdminDatabaseEditPresenter(Database database)
        {
            this.database = database;
            view = new AdminDatabaseEditControl();
            view.Cancel.Visibility = System.Windows.Visibility.Visible;
            SetupFields();
            BindCommands();
            WireEvents();
        }

        private void WireEvents()
        {
            view.Name.TextChanged += TextChanged;
            view.DisplayName.TextChanged += TextChanged;

        }        

        
        #endregion

        public UserControl View 
        { 
            get 
            { 
                return (UserControl) view; 
            } 
        }


        #region Methods
        private void SetupFields()
        {
            view.Name.Text = database.Name;
            view.DisplayName.Text = database.DisplayName;            
        }
        
        private Boolean CanSave()
        {
            Boolean canSave = true;
            if (view.Name.Text.Length == 0)
            {
                view.NameRequired.Visibility = System.Windows.Visibility.Visible;
                canSave = false;
            }
            else
                view.NameRequired.Visibility = System.Windows.Visibility.Collapsed;

            if (view.DisplayName.Text.Length == 0)
            {
                view.DisplayNameRequired.Visibility = System.Windows.Visibility.Visible;
                canSave = false;
            }
            else
                view.DisplayNameRequired.Visibility = System.Windows.Visibility.Collapsed;
                       
            return canSave;
        }

        private void CopyFromGuiToDatabase()
        {
            database.Name = view.Name.Text;
            database.DisplayName = view.DisplayName.Text;                                  
        }
        #endregion

        #region Commands and event handlers
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
                Save(database);

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
        #endregion
    }
}

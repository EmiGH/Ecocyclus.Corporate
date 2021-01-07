using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI.Dashboard
{
    //public class TasksSummary : BaseDashboard
    public partial class BaseDashboard : BasePage
    {
        #region Tasks Summary

        #region Internal Properties

        private Int64 _CountPlannedTasksSummary = 0;
        private Int64 _CountWorkingTasksSummary = 0;
        private Int64 _CountFinishedTasksSummary = 0;
        private Int64 _CountOverDueTasksSummary = 0;

        #endregion

        #region Methods

        private TableRow BuildContentTotalTasksSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            Label lblTotal = new Label();
            lblTotal.Text = GetLocalResourceObject("TotalTasks").ToString();

            Label lblTotalValue = new Label();
            lblTotalValue.Text = (_CountPlannedTasksSummary + _CountWorkingTasksSummary + _CountFinishedTasksSummary + _CountOverDueTasksSummary).ToString();

            _tdColTitle.Controls.Add(lblTotal);
            _tdColContent.Controls.Add(lblTotalValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        private TableRow BuildContentPlannedTasksSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            LinkButton lblPlanned = new LinkButton();
            lblPlanned.Text = GetLocalResourceObject("Planned").ToString();
            lblPlanned.Click += new EventHandler(lblPlannedTasksSummary_Click);

            Label lblPlannedValue = new Label();
            lblPlannedValue.Text = _CountPlannedTasksSummary.ToString();

            _tdColTitle.Controls.Add(lblPlanned);
            _tdColContent.Controls.Add(lblPlannedValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        private TableRow BuildContentWorkingTasksSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            LinkButton lblWorking = new LinkButton();
            lblWorking.Text = GetLocalResourceObject("Working").ToString();
            lblWorking.Click += new EventHandler(lblWorkingTasksSummary_Click);

            Label lblWorkingValue = new Label();
            lblWorkingValue.Text = _CountWorkingTasksSummary.ToString();

            _tdColTitle.Controls.Add(lblWorking);
            _tdColContent.Controls.Add(lblWorkingValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        private TableRow BuildContentOverDueTasksSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            LinkButton lblOverDue = new LinkButton();
            lblOverDue.Text = GetLocalResourceObject("OverDue").ToString();
            lblOverDue.Click += new EventHandler(lblOverDueTasksSummary_Click);

            Label lblOverDueValue = new Label();
            lblOverDueValue.Text = _CountOverDueTasksSummary.ToString();

            _tdColTitle.Controls.Add(lblOverDue);
            _tdColContent.Controls.Add(lblOverDueValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        private TableRow BuildContentFinishedTasksSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            LinkButton lblFinished = new LinkButton();
            lblFinished.Text = GetLocalResourceObject("Finished").ToString();
            lblFinished.Click += new EventHandler(lblFinishedTasksSummary_Click);

            Label lblFinishedValue = new Label();
            lblFinishedValue.Text = _CountFinishedTasksSummary.ToString();

            _tdColTitle.Controls.Add(lblFinished);
            _tdColContent.Controls.Add(lblFinishedValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        protected Table BuildTasksSummary()
        {
            //Estructura HTML
            GetStatesTasksSummary();

            Table _container = BuildTable("tblContentForm", "ContentListDashboard");

            TableRow _trTotal = BuildContentTotalTasksSummary();
            TableRow _trPlanned = BuildContentPlannedTasksSummary();
            TableRow _trWorking = BuildContentWorkingTasksSummary();
            TableRow _trOverDue = BuildContentOverDueTasksSummary();
            TableRow _trFinished = BuildContentFinishedTasksSummary();

            _container.Controls.Add(_trTotal);
            _container.Controls.Add(_trPlanned);
            _container.Controls.Add(_trWorking);
            _container.Controls.Add(_trOverDue);
            _container.Controls.Add(_trFinished);

            return _container;
        }
        private void GetStatesTasksSummary()
        {
            _CountOverDueTasksSummary = EMSLibrary.User.Dashboard.TaskOverdue.Count;
            _CountFinishedTasksSummary = EMSLibrary.User.Dashboard.TasksFinished.Count;
            _CountWorkingTasksSummary = EMSLibrary.User.Dashboard.TasksWorking.Count;
            _CountPlannedTasksSummary = EMSLibrary.User.Dashboard.TasksPlanned().Count;
        }

        #endregion

        #region Page Event

        protected void lblPlannedTasksSummary_Click(object sender, EventArgs e)
        {
            String _entityName = Common.ConstantsEntitiesName.DB.PlannedTasks;
            String _entityNameGrid = Common.ConstantsEntitiesName.DB.PlannedTasks;

            base.NavigatorAddTransferVar("EntityName", _entityName);
            base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            base.NavigatorAddTransferVar("IsFilterHierarchy", false);

            Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);

            //this.Navigate("~/Dashboard/TasksPlanned.aspx", GetLocalResourceObject("TasksPlanned").ToString());
        }
        protected void lblWorkingTasksSummary_Click(object sender, EventArgs e)
        {
            String _entityName = Common.ConstantsEntitiesName.DB.ActiveTasks;
            String _entityNameGrid = Common.ConstantsEntitiesName.DB.ActiveTasks;

            base.NavigatorAddTransferVar("EntityName", _entityName);
            base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            base.NavigatorAddTransferVar("IsFilterHierarchy", false);

            Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);

            //this.Navigate("~/Dashboard/TasksActive.aspx", GetLocalResourceObject("TasksActive").ToString());
        }
        protected void lblFinishedTasksSummary_Click(object sender, EventArgs e)
        {
            String _entityName = Common.ConstantsEntitiesName.DB.FinishedTasks;
            String _entityNameGrid = Common.ConstantsEntitiesName.DB.FinishedTasks;

            base.NavigatorAddTransferVar("EntityName", _entityName);
            base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            base.NavigatorAddTransferVar("IsFilterHierarchy", false);

            Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);

            //this.Navigate("~/Dashboard/TasksFinished.aspx", GetLocalResourceObject("TasksFinished").ToString());
        }
        protected void lblOverDueTasksSummary_Click(object sender, EventArgs e)
        {
            String _entityName = Common.ConstantsEntitiesName.DB.OverDueTasks;
            String _entityNameGrid = Common.ConstantsEntitiesName.DB.OverDueTasks;

            base.NavigatorAddTransferVar("EntityName", _entityName);
            base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            base.NavigatorAddTransferVar("IsFilterHierarchy", false);

            Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);
            //this.Navigate("~/Dashboard/TasksActive.aspx", GetLocalResourceObject("TasksOverDue").ToString());
        }

        #endregion

        #endregion
    }
}

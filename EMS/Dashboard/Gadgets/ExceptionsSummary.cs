using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI.Dashboard
{
    //public class ExceptionsSummary
    public partial class BaseDashboard : BasePage
    {
        #region Exceptions Summary

        #region Internal Properties

        private Int64 _CountOpenedExceptionsSummary = 0;
        private Int64 _CountWorkingExceptionsSummary = 0;
        private Int64 _CountClosedExceptionsSummary = 0;

        #endregion

        #region Methods

        private TableRow BuildContentTotalExceptionsSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            Label lblTotal = new Label();
            lblTotal.Text = GetLocalResourceObject("TotalExceptions").ToString();

            Label lblTotalValue = new Label();
            lblTotalValue.Text = (_CountOpenedExceptionsSummary + _CountWorkingExceptionsSummary + _CountClosedExceptionsSummary).ToString();
            _tdColTitle.Controls.Add(lblTotal);
            _tdColContent.Controls.Add(lblTotalValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        private TableRow BuildContentOpenedExceptionsSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            LinkButton lbOpened = new LinkButton();
            lbOpened.Text = GetLocalResourceObject("Opened").ToString();
            lbOpened.Click += new EventHandler(lbOpenedExceptionsSummary_Click);

            Label lblOpenedValue = new Label();
            lblOpenedValue.Text = _CountOpenedExceptionsSummary.ToString();

            _tdColTitle.Controls.Add(lbOpened);
            _tdColContent.Controls.Add(lblOpenedValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        private TableRow BuildContentWorkingExceptionsSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            LinkButton lbWorking = new LinkButton();
            lbWorking.Text = GetLocalResourceObject("Working").ToString();
            lbWorking.Click += new EventHandler(lbWorkingExceptionsSummary_Click);

            Label lblWorkingValue = new Label();
            lblWorkingValue.Text = _CountWorkingExceptionsSummary.ToString();

            _tdColTitle.Controls.Add(lbWorking);
            _tdColContent.Controls.Add(lblWorkingValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        private TableRow BuildContentClosedExceptionsSummary()
        {
            TableRow _tr = new TableRow();
            TableCell _tdColTitle = BuildTableCell("ColTitle");
            TableCell _tdColContent = BuildTableCell("ColContent");

            LinkButton lbClosed = new LinkButton();
            lbClosed.Text = GetLocalResourceObject("Closed").ToString();
            lbClosed.Click += new EventHandler(lbClosedExceptionsSummary_Click);

            Label lblClosedValue = new Label();
            lblClosedValue.Text = _CountClosedExceptionsSummary.ToString();

            _tdColTitle.Controls.Add(lbClosed);
            _tdColContent.Controls.Add(lblClosedValue);
            _tr.Controls.Add(_tdColTitle);
            _tr.Controls.Add(_tdColContent);
            return _tr;
        }
        protected Table BuildExceptionsSummary()
        {
            //Estructura HTML
            GetStatesExceptions();

            Table _container = BuildTable("tblContentForm", "ContentListDashboard");

            TableRow _trTotal = BuildContentTotalExceptionsSummary();
            TableRow _trOpened = BuildContentOpenedExceptionsSummary();
            TableRow _trWorking = BuildContentWorkingExceptionsSummary();
            TableRow _trClosed = BuildContentClosedExceptionsSummary();

            _container.Controls.Add(_trTotal);
            _container.Controls.Add(_trOpened);
            _container.Controls.Add(_trWorking);
            _container.Controls.Add(_trClosed);

            return _container;
        }
        private void GetStatesExceptions()
        {
            _CountOpenedExceptionsSummary = EMSLibrary.User.Dashboard.ProcessExceptionOpen.Count;
            _CountWorkingExceptionsSummary = EMSLibrary.User.Dashboard.ProcessExceptionTreat.Count;
            _CountClosedExceptionsSummary = EMSLibrary.User.Dashboard.ProcessExceptionClose.Count;
        }

        #endregion

        #region Page Event

        protected void lbOpenedExceptionsSummary_Click(object sender, EventArgs e)
        {
            String _entityName = Common.ConstantsEntitiesName.DB.OpenedExceptions;
            String _entityNameGrid = Common.ConstantsEntitiesName.DB.OpenedExceptions;

            base.NavigatorAddTransferVar("EntityName", _entityName);
            base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            base.NavigatorAddTransferVar("IsFilterHierarchy", false);

            Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);

            //this.Navigate("~/Dashboard/ExceptionsOpened.aspx",GetLocalResourceObject("ExceptionsOpened").ToString());
        }
        protected void lbWorkingExceptionsSummary_Click(object sender, EventArgs e)
        {
            String _entityName = Common.ConstantsEntitiesName.DB.WorkingExceptions;
            String _entityNameGrid = Common.ConstantsEntitiesName.DB.WorkingExceptions;

            base.NavigatorAddTransferVar("EntityName", _entityName);
            base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            base.NavigatorAddTransferVar("IsFilterHierarchy", false);

            Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);

            //this.Navigate("~/Dashboard/ExceptionsWorking.aspx",GetLocalResourceObject("ExceptionsWorking").ToString());
        }
        protected void lbClosedExceptionsSummary_Click(object sender, EventArgs e)
        {
            String _entityName = Common.ConstantsEntitiesName.DB.ClosedExceptions;
            String _entityNameGrid = Common.ConstantsEntitiesName.DB.ClosedExceptions;

            base.NavigatorAddTransferVar("EntityName", _entityName);
            base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            base.NavigatorAddTransferVar("IsFilterHierarchy", false);

            Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);

            //this.Navigate("~/Dashboard/ExceptionsClosed.aspx",GetLocalResourceObject("ExceptionsClosed").ToString());
        }

        #endregion

        #endregion
    }
}

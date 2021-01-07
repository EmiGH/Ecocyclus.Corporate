using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public class JobtitleWithChart : JobTitle
    {
        #region Internal Properties
        private JobtitleWithChart _Parent;
        private List<JobtitleWithChart> _Childrens;
        private OrganizationalChart _OrganizationalChart;
        private Int64 _IdParentGeographicArea;
        private Int64 _IdParentFunctionalArea;
        private Int64 _IdParentOrganization;
        private Int64 _IdParentPosition;
        #endregion


        #region External Properties
        public Int64 IdParentGeographicArea
        {
            get
            {
                if (Parent == null) { _IdParentGeographicArea = 0; } else { _IdParentGeographicArea = _Parent.IdGeographicArea; }
                return _IdParentGeographicArea;
            }
        }
        public Int64 IdParentFunctionalArea
        {
            get
            {
                if (Parent == null) { _IdParentFunctionalArea = 0; } else { _IdParentFunctionalArea= _Parent.IdFunctionalArea; }
                return _IdParentFunctionalArea;
            }
        }
        public Int64 IdParentPosition
        {
            get
            {
                if (Parent == null) { _IdParentPosition = 0; } else { _IdParentPosition = _Parent.IdParentPosition; }
                return _IdParentPosition;
            }
        }

        /// <summary>
        /// Devuelve el padre en un organizational Cart
        /// </summary>
        public JobtitleWithChart Parent
        {
            get
            {
                if (_Parent == null)
                { _Parent = (JobtitleWithChart)new Collections.JobTitles(_OrganizationalChart).Item(this); }
                return _Parent;
            }
        }
        /// <summary>
        /// Devuelve sus hijos en un organizational chart
        /// </summary>
        public List<JobtitleWithChart> Childrens
        {
            get
            {
                if (_Childrens == null)
                { _Childrens = new Collections.JobTitles(_OrganizationalChart).Items(this); }
                return _Childrens;
            }
        }
        /// <summary>
        /// Devuelve el organizational chart asociado
        /// </summary>
        public OrganizationalChart OrganizationalChart
        {
            get { return _OrganizationalChart; }
        }
        #endregion


        internal JobtitleWithChart(Int64 idOrganization, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Credential credential, OrganizationalChart organizationalChart) 
            : base(idOrganization, idGeographicArea, idFunctionalArea, idPosition, credential)
        {
            _OrganizationalChart = organizationalChart;
        }

        /// <summary>
        /// modifica las relaciones en el organizational chart
        /// </summary>
        /// <param name="idGeographicArea"></param>
        /// <param name="idPosition"></param>
        /// <param name="idFunctionalArea"></param>
        /// <param name="idGeographicAreaParent"></param>
        /// <param name="idPositionParent"></param>
        /// <param name="idFunctionalAreaParent"></param>
        /// <returns></returns>
        public JobTitle Modify(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.JobTitles(OrganizationalChart).Modify(idGeographicArea, idPosition, idFunctionalArea, idGeographicAreaParent, idPositionParent, idFunctionalAreaParent);
                _transactionScope.Complete();
                return this;
            }
        }

    }
}

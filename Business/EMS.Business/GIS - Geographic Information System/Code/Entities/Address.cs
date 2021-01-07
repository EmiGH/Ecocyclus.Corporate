using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Entities
{
    public abstract class Address : IGoegraphicData
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdAddress; 
        private Int64 _IdGeographicArea;
        private String _Coordinate;
        //private String _Name;
        //private String _Description;
        private String _Street;
        private String _Number;
        private String _Floor;
        private String _Department;
        private String _PostCode;        
        private Entities.GeographicArea _GeographicArea;
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdAddress
        {
            get { return _IdAddress; }
        }
        public Int64 IdGeographicArea
        {
            get { return _IdGeographicArea; }
        }
        public String Coordinate
        {
            get { return _Coordinate; }
        }
        //public String Name
        //{
        //    get {return _Name;}
        //}
        //public String Description
        //{
        //    get { return _Description; }
        //}
        public String Street
        {
            get { return _Street; }
        }
        public String Number
        {
            get { return _Number; }
        }
        public String Floor
        {
            get { return _Floor; }
        }
        public String Department
        {
            get { return _Department; }
        }
        public String PostCode
        {
            get { return _PostCode; }
        }
        public Entities.GeographicArea GeographicArea
        {
            get
            {
                if (_GeographicArea == null)
                { _GeographicArea = new Collections.GeographicAreas(_Credential).Item(_IdGeographicArea); }
                return _GeographicArea;
            }
        }
      
        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal void Remove()
        {
        }
        #endregion

        internal Address(Int64 IdAddress, Int64 idGeographicArea, String coordinate, 
            String street, String number, String floor, String department, String postCode, Credential credential)
        {
            _Credential = credential;
            _IdAddress = IdAddress;
            _IdGeographicArea = idGeographicArea;
            _Coordinate = coordinate;
            //_Name = name;
            //_Description = description;
            _Street = street;
            _Number = number;
            _Floor = floor;
            _Department = department;
            _PostCode = postCode;
        }


    }
}

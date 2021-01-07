using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public class JobTitle
    {
        #region Internal Properties
            
            private Credential _Credential;
            private Organization _Organization;
            private Int64 _IdPosition;//Identificador de la posicion      
            private Int64 _IdGeographicArea;//Identificador del area geografica      
            private Int64 _IdFunctionalArea;//Identificador del area funcional      
            private Int64 _IdOrganization;
            private GIS.Entities.GeographicArea _GeographicArea;
            private DS.Entities.FunctionalArea _FunctionalArea;
            private DS.Entities.Position _Position;
            private Entities.GeographicFunctionalArea _GeographicFunctionalAreas;
            private Entities.FunctionalPosition _FunctionalPositions;
            private List<Entities.Post> _Posts;   //puestos de la persona
            private List<Entities.Responsibility> _Responsibilities;   //puestos de la persona

        #endregion


        #region External Properties
            public Int64 IdGeographicArea
            {
                get
                {
                     return _IdGeographicArea; 
                    
                }
            }
            public Int64 IdFunctionalArea
            {
                get
                {
                    return _IdFunctionalArea;
                }
            }
            public Int64 IdPosition
            {
                get
                {
                    return _IdPosition;
                }
            }
            public Int64 IdOrganization
            {
                get
                {
                    return _IdOrganization;
                }
            }
            #region Posts
            public List<Entities.Post> Posts()
            {
                if (_Posts == null)
                {
                    _Posts = new Condesus.EMS.Business.DS.Collections.Posts(this).Items();
                }
                return _Posts;
            }
            public Entities.Post Post(Person person)
            {
                return new Condesus.EMS.Business.DS.Collections.Posts(this).Item(person.IdPerson, IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition);
            }
            #endregion            

            public Entities.GeographicFunctionalArea GeographicFunctionalAreas
            {
                get
                {
                    if (_GeographicFunctionalAreas == null)
                    { _GeographicFunctionalAreas = new DS.Collections.GeographicFunctionalAreas(Organization).Item(FunctionalArea, GeographicArea); }
                    return _GeographicFunctionalAreas;
                }
            }
            public Entities.FunctionalPosition FunctionalPositions
            {
                get
                {
                    if (_FunctionalPositions == null)
                    { _FunctionalPositions = new Collections.FunctionalPositions(Organization).Item(Position, FunctionalArea); }
                    return _FunctionalPositions;
                }
            }
            public String Name()
            {
                StringBuilder _compositeName = new StringBuilder();

                //Arma el name con Position.Name + FunctionalArea.Name + GeographicArea.Name
                _compositeName.Append(FunctionalPositions.Position.LanguageOption.Name.ToString());
                _compositeName.Append(" - ");
                _compositeName.Append(FunctionalPositions.FunctionalArea.LanguageOption.Name.ToString());
                _compositeName.Append(" - ");
                _compositeName.Append(GeographicFunctionalAreas.GeographicArea.LanguageOption.Name.ToString());
                return _compositeName.ToString();
            }
            public Organization Organization
            {
                get
                {
                    _Organization = new Collections.Organizations(_Credential).Item(_IdOrganization);
                    return _Organization;
                }
            }
            public GIS.Entities.GeographicArea GeographicArea
            {
                get
                {
                    if(_GeographicArea==null)
                    {_GeographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(_IdGeographicArea);}
                    return _GeographicArea;
                }
            }
            public Entities.FunctionalArea FunctionalArea
            {
                get
                {
                    if (_FunctionalArea == null)
                    { _FunctionalArea = new Collections.FunctionalAreas(Organization).Item(_IdFunctionalArea); }
                    return _FunctionalArea;
                }
            }
            public Entities.Position Position
            {
                get
                {
                    if (_Position == null)
                    { _Position = new Collections.Positions(Organization).Item(_IdPosition); }
                    return _Position;
                }
            }
            
            #region Responsibilities
                public List<Entities.Responsibility> Responsibilities()
                {
                    if (_Responsibilities == null)
                    {
                        _Responsibilities = new Collections.Responsibilities(this, _Credential).Items();
                    }
                    return _Responsibilities;
                }
                public Entities.Responsibility Responsibility(Entities.FunctionalArea functionalAreaResponsibility, GIS.Entities.GeographicArea geographicAreaResponsibility)
                {
                    return new Condesus.EMS.Business.DS.Collections.Responsibilities(this, _Credential).Item(functionalAreaResponsibility.IdFunctionalArea, geographicAreaResponsibility.IdGeographicArea);
                }
                public void ResponsibilitiesAdd(Entities.FunctionalArea functionalAreaResponsibility, GIS.Entities.GeographicArea geographicAreaResponsibility)
                {
                    Collections.Responsibilities _responsibilities = new Condesus.EMS.Business.DS.Collections.Responsibilities(this, _Credential);
                    _responsibilities.Add(functionalAreaResponsibility, geographicAreaResponsibility);
                }
                public void Remove(Responsibility responsibility)
                {
                    Collections.Responsibilities _responsibilities = new Condesus.EMS.Business.DS.Collections.Responsibilities(this, _Credential);
                    _responsibilities.Remove(responsibility);
                }
            #endregion

        

        #endregion

        internal JobTitle(Int64 idOrganization, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Credential credential) 
        {

            _IdOrganization = idOrganization;
            _IdGeographicArea = idGeographicArea;
            _IdFunctionalArea = idFunctionalArea;
            _IdPosition = idPosition;
            _Credential = credential;
        }


        internal Dictionary<Int64, OrganizationalChart> associatedChart
        {
            get
            {
                return new Collections.OrganizationalCharts(this.Organization).Items();
            }
        }
        //Borra sus dependencias
        internal void Remove()
        {
            //Borra Responsibility
            foreach (Responsibility _responsibility in Responsibilities())
            {
                new Collections.Responsibilities(this, _Credential).Remove();
            }

            //hacer una rutina de reenganche para los chart que contengan este JT y boorarlo de ellos
           
            //Borra seguridad
            new Security.Collections.Rights(_Credential).Remove(this);

        }


        #region Security
        public void SecurityClassAdd(ISecurity Object, Security.Entities.Permission permission)
        {
            new Security.Collections.Rights(_Credential).Add(Object, this, permission);
        }
        public void SecurityClassRemove(ISecurity Object, Security.Entities.Permission permission)
        {
            new Security.Collections.Rights(_Credential).Remove(this, Object, permission);
        }
       

        public List<Security.Entities.Right> Rights()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByJobTitle(this);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _Rights.Add(_right);
            }
            return _Rights;
        }
        public List<Security.Entities.RightMapKC> RightMapKC()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapKC> _RightMapKC = new List<Security.Entities.RightMapKC>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.MapKC);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapKC.Add((Security.Entities.RightMapKC)_right);                
            }
            return _RightMapKC;
        }
        public List<Security.Entities.RightMapDS> RightMapDS()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapDS> _RightMapDS = new List<Security.Entities.RightMapDS>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.MapDS);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapDS.Add((Security.Entities.RightMapDS)_right);
            }
            return _RightMapDS;
        }
        public List<Security.Entities.RightMapIA> RightMapIA()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapIA> _RightMapIA = new List<Security.Entities.RightMapIA>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.MapIA);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapIA.Add((Security.Entities.RightMapIA)_right);
            }
            return _RightMapIA;
        }
        public List<Security.Entities.RightMapRM> RightMapRM()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapRM> _RightMapRM = new List<Security.Entities.RightMapRM>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.MapRM);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapRM.Add((Security.Entities.RightMapRM)_right);
            }
            return _RightMapRM;
        }
        public List<Security.Entities.RightMapPA> RightMapPA()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapPA> _RightMapPA = new List<Security.Entities.RightMapPA>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.MapPA);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapPA.Add((Security.Entities.RightMapPA)_right);
            }
            return _RightMapPA;
        }
        public List<Security.Entities.RightMapPF> RightMapPF()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapPF> _RightMapPF = new List<Security.Entities.RightMapPF>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.MapPF);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapPF.Add((Security.Entities.RightMapPF)_right);
            }
            return _RightMapPF;
        }
        public List<Security.Entities.RightOrganization> RightOrganizations()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightOrganization> _RightOrganization = new List<Security.Entities.RightOrganization>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.Organization);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightOrganization.Add((Security.Entities.RightOrganization)_right);
            }
            return _RightOrganization;
        }
        public List<Security.Entities.RightProcess> RightProcesses()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightProcess> _RightProcess = new List<Security.Entities.RightProcess>();

            _Rights = new Security.Collections.Rights(_Credential).ReadClassByJobTitleAndClassName(this, Common.Security.Process);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightProcess.Add((Security.Entities.RightProcess)_right);
            }
            return _RightProcess;
        }

        #endregion
    }
}

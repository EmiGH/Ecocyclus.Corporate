using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class FunctionalPosition
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdPosition;
            private Int64 _IdFunctionalArea;
            private Int64 _IdOrganization;
            private Int64 _IdParentPosition;
            private Int64 _IdParentFunctionalArea; //Id del area funcional padre        
            private List<Entities.FunctionalPosition> _Children;   //FunctionalPositions hijas
            private Entities.Position _Positions;
            private Entities.FunctionalArea _FunctionalArea;
            private Entities.Organization _Organization;
            private Entities.FunctionalPosition _ParentFunctionalPosition;

           
        #endregion

        #region External Properties            
            public Int64 IdPosition
            {
                get
                {
                    if (_Positions == null) { _IdPosition = 0; } else { _IdPosition = Position.IdPosition; }
                    return _IdPosition;
                }
            }
            public Int64 IdFunctionalArea
            {
                get
                {
                    if (_FunctionalArea == null) { _IdFunctionalArea = 0; } else { _IdFunctionalArea = FunctionalArea.IdFunctionalArea; }
                    return _IdFunctionalArea;
                }
            }
            public Int64 IdParentPosition
            {
                get 
                {
                    if (_ParentFunctionalPosition == null) { _IdParentPosition = 0; } else { _IdParentPosition = _ParentFunctionalPosition.Position.IdPosition; }
                    return _IdParentPosition; 
                }
            }
            public Int64 IdParentFunctionalArea
            {
                get 
                {
                    if (_ParentFunctionalPosition == null) { _IdParentFunctionalArea = 0; } else { _IdParentFunctionalArea = _ParentFunctionalPosition.FunctionalArea.IdFunctionalArea; }
                    return _IdParentFunctionalArea; 
                }
            }
            public Int64 IdOrganization
            {
                get
                {
                    if (_IdOrganization == null) { _IdOrganization = 0; } else { _IdOrganization = Organization.IdOrganization; }
                    return _IdOrganization;
                }
            }
            public Entities.Organization Organization
            {
                get
                {
                    return _Organization;
                }
            }
            public Entities.Position Position
            {
                get
                {                    
                    return _Positions;
                }
            }
            public Entities.FunctionalArea FunctionalArea
            {
                get
                {                    
                    return _FunctionalArea;
                }
            }
            public Entities.FunctionalPosition ParentFunctionalPosition
            {
                get { return _ParentFunctionalPosition; }
            }
            public String Name()
            {
                StringBuilder _compositeName = new StringBuilder();

                _compositeName.Append(Position.LanguageOption.Name.ToString());
                _compositeName.Append(" - ");
                _compositeName.Append(FunctionalArea.LanguageOption.Name.ToString());
                return _compositeName.ToString();

                //.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage).Name
                
                //_compositeName.Append(Position.LanguageOption.Name.ToString());
                //_compositeName.Append(" - ");
                //_compositeName.Append(FunctionalArea.LanguageOption.Name.ToString());
                //try
                //{
                //    StringBuilder _compositeName = new StringBuilder();
                //    _compositeName.Append(Position.LanguagesOptions.Item(_Credential.CurrentLanguage.IdLanguage).Name.ToString());
                //    _compositeName.Append(" - ");
                //    _compositeName.Append(FunctionalArea.LanguagesOptions.Item(_Credential.CurrentLanguage.IdLanguage).Name.ToString());
                //    return _compositeName.ToString();
                //}
                //catch
                //{
                //    StringBuilder _compositeNameErr = new StringBuilder();
                //    _compositeNameErr.Append(Position.LanguagesOptions.Item(_Credential.DefaultLanguage.IdLanguage).Name.ToString());
                //    _compositeNameErr.Append(" - ");
                //    _compositeNameErr.Append(FunctionalArea.LanguagesOptions.Item(_Credential.DefaultLanguage.IdLanguage).Name.ToString());
                //    return _compositeNameErr.ToString();
                //}
               
            }

            public List<Entities.FunctionalPosition> Children
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.FunctionalPositions(this, Organization).Items();
                    }
                    return _Children;
                }
            }
        #endregion


            internal FunctionalPosition(Position position, FunctionalArea functionalArea, Organization organization, FunctionalPosition parentFunctionalPosition, Credential credential) 
        {
            _FunctionalArea = functionalArea;
            _Positions = position;
            _ParentFunctionalPosition = parentFunctionalPosition;
            _Organization = organization;
            _Credential = credential;
           
        }

    }
}

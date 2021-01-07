using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class OrganizationRelationship
    {
        #region Internal Properties
            private Credential _Credential;
            private Organization _Organization;//Identificador de la organizacion1
            private Organization _OrganizationRelated;//Identificador de la organizacion2
            private OrganizationRelationshipType _OrganizationRelationshipType;//Identificador del tipo derelacion de la organizacion
        #endregion

        #region External Properties
            public Organization Organization
            {
                get { return _Organization; }
            }
            public Entities.OrganizationRelationshipType OrganizationRelationshipType
            {
                get
                {
                    return _OrganizationRelationshipType;
                }
            }
            public Entities.Organization OrganizationRelated
            {
                get
                {
                    return _OrganizationRelated;
                }
            }
        #endregion

            internal OrganizationRelationship(Organization organization, Organization organizationRelated, OrganizationRelationshipType organizationRelationshipType, Credential credential)
        {
            _Credential = credential;
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _Organization = organization;
            _OrganizationRelated = organizationRelated;
            _OrganizationRelationshipType = organizationRelationshipType;
         }
    }
}

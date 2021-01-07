using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    public interface ISecurity : IObjectIdentifier
    {        
        #region Read
        //Permisos de la entidad
        Dictionary<Int64, Security.Entities.Permission> Permissions { get; }

        //ALL
        List<Security.Entities.RightPerson> SecurityPeople();
        List<Security.Entities.RightJobTitle> SecurityJobTitles();

        //por ID
        Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission);
        Security.Entities.RightPerson ReadPersonByID(DS.Entities.Person person, Security.Entities.Permission permission);
        
        #endregion

        #region Write region
        Security.Entities.RightPerson SecurityPersonAdd(DS.Entities.Person person, Security.Entities.Permission permission);
        Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission);

        void SecurityPersonRemove(DS.Entities.Person person, Security.Entities.Permission permission);
        void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission);

        Security.Entities.RightPerson SecurityPersonModify(Security.Entities.RightPerson oldRightPerson, DS.Entities.Person person, Security.Entities.Permission permission);
        Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission);

        #endregion


        #region Version Security 

        #region Security 15-02-2010

        //#region Properties
        //public Int64 IdObject
        //{
        //    get { return 0; }
        //}
        //public String ClassName
        //{
        //    get
        //    {
        //        return Common.Security.MapPF;
        //    }
        //}
        //#endregion

        //#region Read
        //#region Permissions
        //internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
        //public Dictionary<Int64, Security.Entities.Permission> Permissions
        //{
        //    get
        //    {
        //        if (_Permissions == null)
        //        { _Permissions = new Security.Collections.Permissions(_Credential).Items(this); }
        //        return _Permissions;
        //    }
        //}
        //#endregion

        ////ALL
        //public List<Security.Entities.RightPerson> SecurityPeople()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadPersonByObject(this);
        //}

        //public List<Security.Entities.RightJobTitle> SecurityJobTitles()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadJobTitleByObject(this);
        //}
        ////por ID
        //public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    return new Security.Collections.Rights(_Credential).ReadJobTitleByID(jobTitle, permission);
        //}
        //public Security.Entities.RightPerson ReadPersonByID(DS.Entities.Person person, Security.Entities.Permission permission)
        //{
        //    return new Security.Collections.Rights(_Credential).ReadPersonByID(person, permission);
        //}

        //#endregion

        //#region Write
        ////Security Add
        //public Security.Entities.RightPerson SecurityPersonAdd(DS.Entities.Person person, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Alta el permiso
        //    Security.Entities.RightPerson _rightPerson = new Security.Collections.Rights(_Credential).Add(this, person, permission);

        //    return _rightPerson;
        //}
        //public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Alta el permiso
        //    Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission);

        //    return _rightJobTitle;
        //}
        ////Security Remove
        //public void SecurityPersonRemove(DS.Entities.Person person, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Borra el permiso
        //    new Security.Collections.Rights(_Credential).Remove(person, this, permission);
        //}
        //public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Borra el permiso
        //    new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        //}
        ////Security Modify
        //public Security.Entities.RightPerson SecurityPersonModify(Security.Entities.RightPerson oldRightPerson, DS.Entities.Person person, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Se borra con sus herederos
        //    this.SecurityPersonRemove(person, oldRightPerson.Permission);
        //    //se da de alta el y sus herederos
        //    this.SecurityPersonAdd(person, permission);

        //    return new Condesus.EMS.Business.Security.Entities.RightPerson(permission, person);
        //}
        //public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Se borra con sus herederos
        //    this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
        //    //se da de alta el y sus herederos
        //    this.SecurityJobTitleAdd(jobTitle, permission);

        //    return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle);
        //}
        //#endregion

        #endregion

        #region Security 10-02-2010

        //#region Properties
        //public Int64 IdObject
        //{
        //    get { return 0; }
        //}
        //public String ClassName
        //{
        //    get
        //    {
        //        return Common.Security.MapPF;
        //    }
        //}
        //#endregion

        //#region Write
        ////Security Add
        //public Security.Entities.RightPost SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Alta el permiso
        //    Security.Entities.RightPost _rightPost = new Security.Collections.Rights(_Credential).Add(this, post, permission, this, roleType);

        //    return _rightPost;
        //}
        //public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Alta el permiso
        //    Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission, this, roleType);

        //    return _rightJobTitle;
        //}
        ////Security Remove
        //public void SecurityPostRemove(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Borra el permiso
        //    new Security.Collections.Rights(_Credential).Remove(post, this, permission);
        //}
        //public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Borra el permiso
        //    new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        //}
        ////Security Modify
        //public Security.Entities.RightPost SecurityPostModify(Security.Entities.RightPost oldRightPost, DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Se borra con sus herederos
        //    this.SecurityPostRemove(post, oldRightPost.Permission);
        //    //se da de alta el y sus herederos
        //    this.SecurityPostAdd(post, permission, roleType);

        //    return new Condesus.EMS.Business.Security.Entities.RightPost(permission, post, roleType);
        //}
        //public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Se borra con sus herederos
        //    this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
        //    //se da de alta el y sus herederos
        //    this.SecurityJobTitleAdd(jobTitle, permission, roleType);

        //    return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle, roleType);
        //}
        //#endregion

        //#region Read
        //#region Permissions
        //internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
        //public Dictionary<Int64, Security.Entities.Permission> Permissions
        //{
        //    get
        //    {
        //        if (_Permissions == null)
        //        { _Permissions = new Security.Collections.Permissions(_Credential).Items(this); }
        //        return _Permissions;
        //    }
        //}
        //#endregion

        ////ALL
        //public List<Security.Entities.RightPost> SecurityPosts()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadPostByClassName(this);
        //}

        //public List<Security.Entities.RightJobTitle> SecurityJobTitles()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadJobTitleByClassName(this);
        //}
        ////por ID
        //public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    return new Security.Collections.Rights(_Credential).ReadByJobTitleObjectPermissionParent(jobTitle, this, permission);
        //}
        //public Security.Entities.RightPost ReadPostByID(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    return new Security.Collections.Rights(_Credential).ReadByPostObjectPermissionParent(post, this, permission);
        //}

        //#endregion

        #endregion

        #region Security 10-02-2009

        //#region Properties
        //public Int64 IdObject
        //{
        //    get { return 0; }
        //}
        //public String Title
        //{
        //    get { return Common.Security.MapPA; }
        //}
        //public String ClassName
        //{
        //    get
        //    {
        //        return Common.Security.MapPA;
        //    }
        //}
        //internal Dictionary<Int64, ISecurityElement> SecurityClassifications(Dictionary<Int64, Entities.IndicatorClassification> Classifications)
        //{
        //    Dictionary<Int64, ISecurityElement> _ClassificationSecurityElement = new Dictionary<long, ISecurityElement>();
        //    foreach (Entities.IndicatorClassification _classification in Classifications.Values)
        //    {
        //        _ClassificationSecurityElement.Add(_classification.IdObject, _classification);
        //    }
        //    return _ClassificationSecurityElement;
        //}
        //#endregion

        //#region Write
        ////Security Add
        //public Security.Entities.RightPost SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Alta el permiso
        //    Security.Entities.RightPost _rightPost = new Security.Collections.Rights(_Credential).Add(this, post, permission, this, roleType);
        //    //Alta en cascada para las clasificaciones
        //    foreach (ISecurityElement _classification in this.IndicatorClassifications().Values)
        //    {
        //        _classification.SecurityPostAdd(post, permission, this, roleType);
        //    }
        //    //Alta en cascada para los elementos
        //    foreach (ISecurityElement _element in this.IndicatorWithoutClassifications().Values)
        //    {
        //        _element.SecurityPostAdd(post, permission, this, roleType);
        //    }

        //    return _rightPost;
        //}
        //public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Alta el permiso
        //    Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission, this, roleType);
        //    //Alta en cascada para las clasificaciones
        //    foreach (ISecurityElement _classification in this.IndicatorClassifications().Values)
        //    {
        //        _classification.SecurityJobTitleAdd(jobTitle, permission, this, roleType);
        //    }
        //    //Alta en cascada para los elementos
        //    foreach (ISecurityElement _element in this.IndicatorWithoutClassifications().Values)
        //    {
        //        _element.SecurityJobTitleAdd(jobTitle, permission, this, roleType);
        //    }
        //    return _rightJobTitle;
        //}
        ////Security Remove
        //public void SecurityPostRemove(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Borra el permiso
        //    new Security.Collections.Rights(_Credential).Remove(post, this, permission);
        //    //Borra en cascada para las clasificaciones
        //    foreach (ISecurityElement _classification in this.IndicatorClassifications().Values)
        //    {
        //        _classification.SecurityPostRemove(post, permission);
        //    }
        //    //Borra en cascada para los elementos
        //    foreach (ISecurityElement _element in this.IndicatorWithoutClassifications().Values)
        //    {
        //        _element.SecurityPostRemove(post, permission);
        //    }
        //}
        //public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Borra el permiso
        //    new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        //    //Borra en cascada para las clasificaciones
        //    foreach (ISecurityElement _classification in this.IndicatorClassifications().Values)
        //    {
        //        _classification.SecurityJobTitleRemove(jobTitle, permission);
        //    }
        //    //Borra en cascada para los elementos
        //    foreach (ISecurityElement _element in this.IndicatorWithoutClassifications().Values)
        //    {
        //        _element.SecurityJobTitleRemove(jobTitle, permission);
        //    }
        //}
        ////Security Modify
        //public Security.Entities.RightPost SecurityPostModify(Security.Entities.RightPost oldRightPost, DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Se borra con sus herederos
        //    this.SecurityPostRemove(post, oldRightPost.Permission);
        //    //se da de alta el y sus herederos
        //    this.SecurityPostAdd(post, permission, roleType);

        //    return new Condesus.EMS.Business.Security.Entities.RightPost(permission, post, roleType);
        //}
        //public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
        //{
        //    //Valida Seguridad
        //    if (!new Security.Authority(_Credential).Authorize(this.ClassName, _Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        //    //Se borra con sus herederos
        //    this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
        //    //se da de alta el y sus herederos
        //    this.SecurityJobTitleAdd(jobTitle, permission, roleType);

        //    return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle, roleType);
        //}
        //#endregion

        //#region Read
        //#region Permissions
        //internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
        //public Dictionary<Int64, Security.Entities.Permission> Permissions
        //{
        //    get
        //    {
        //        if (_Permissions == null)
        //        { _Permissions = new Security.Collections.Permissions(_Credential).Items(this); }
        //        return _Permissions;
        //    }
        //}
        //#endregion

        ////ALL
        //public List<Security.Entities.RightPost> SecurityPosts()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadPostByClassName(this);
        //}

        //public List<Security.Entities.RightJobTitle> SecurityJobTitles()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadJobTitleByClassName(this);
        //}
        ////por ID
        //public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    return new Security.Collections.Rights(_Credential).ReadByJobTitleObjectPermissionParent(jobTitle, this, permission);
        //}
        //public Security.Entities.RightPost ReadPostByID(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    return new Security.Collections.Rights(_Credential).ReadByPostObjectPermissionParent(post, this, permission);
        //}

        //#endregion

        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    //public interface ISecurityElement : ISecurity
    //{
    //    #region Write region
    //    Security.Entities.RightPost SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType);
    //    //void SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission, ISecurity parent, Security.Entities.RoleType roleType);

    //    Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType);
    //    //void SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, ISecurity parent, Security.Entities.RoleType roleType);

    //    //void SecurityPostRemove(DS.Entities.Post post, Security.Entities.Permission permission);
    //    //void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission);
    //    void SecurityPostRemove();
    //    void SecurityJobTitleRemove();

    //    Security.Entities.RightPost SecurityPostModify(Security.Entities.RightPost oldRightPost, DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType);
    //    Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType);
    //    #endregion

    //    #region Versions Security Element

    //    #region Security 10-02-2010

    //    //#region Properties
    //    //public Int64 IdObject
    //    //{
    //    //    get { return IdProcess; }
    //    //}
    //    //public String ClassName
    //    //{
    //    //    get
    //    //    {
    //    //        return Common.Security.Process;
    //    //    }
    //    //}
    //    //#endregion

    //    //#region Read
    //    //#region Permissions
    //    //internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
    //    //public Dictionary<Int64, Security.Entities.Permission> Permissions
    //    //{
    //    //    get
    //    //    {
    //    //        if (_Permissions == null)
    //    //        { _Permissions = new Security.Collections.Permissions(Credential).Items(this); }
    //    //        return _Permissions;
    //    //    }
    //    //}
    //    //#endregion
    //    ////ALL
    //    //public List<Security.Entities.RightPost> SecurityPosts()
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadPostByClassName(this);
    //    //}
    //    //public List<Security.Entities.RightJobTitle> SecurityJobTitles()
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadJobTitleByClassName(this);
    //    //}
    //    ////por ID
    //    //public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadByJobTitleObjectPermissionParent(jobTitle, this, permission);
    //    //}
    //    //public Security.Entities.RightPost ReadPostByID(DS.Entities.Post post, Security.Entities.Permission permission)
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadByPostObjectPermissionParent(post, this, permission);
    //    //}

    //    //#endregion

    //    //#region Write
    //    ////Security Add
    //    //public Security.Entities.RightPost SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa para cuando se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    return new Security.Collections.Rights(Credential).Add(this, post, permission, this, roleType);
    //    //}
    //    //public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa cuando el alta se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    return new Security.Collections.Rights(Credential).Add(this, jobTitle, permission, this, roleType);
    //    //}

    //    ////Security Remove
    //    //public void SecurityPostRemove()
    //    //{//se usa cuando la baja se hace de este elemento
    //    //    new Security.Collections.Rights(Credential).Remove(this);
    //    //}
    //    //public void SecurityJobTitleRemove()
    //    //{//se usa cuando la baja se hace de este elemento
    //    //    new Security.Collections.Rights(Credential).Remove(this);
    //    //}

    //    ////Security Modify
    //    //public Security.Entities.RightPost SecurityPostModify(Security.Entities.RightPost oldRightPost, DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa cuando la modificacion se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    //se borra con sus herederos
    //    //    this.SecurityPostRemove();
    //    //    //se da de alta con sus herederos
    //    //    this.SecurityPostAdd(post, permission, roleType);
    //    //    //retorma el right
    //    //    return new Condesus.EMS.Business.Security.Entities.RightPost(permission, post, roleType);
    //    //}
    //    //public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa cuando la modificacion se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    //se borra con sus herederos
    //    //    this.SecurityJobTitleRemove();
    //    //    //se da de alta con sus herederos
    //    //    this.SecurityJobTitleAdd(jobTitle, permission, roleType);
    //    //    //retorma el right
    //    //    return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle, roleType);
    //    //}


    //    //#endregion

    //    #endregion

    //    #region Security 10-02-2009
       
    //    //#region Properties
    //    //public Int64 IdObject
    //    //{
    //    //    get { return IdProcess; }
    //    //}
    //    //public String Title
    //    //{
    //    //    get { return LanguageOption.Title; }
    //    //}
    //    //public String ClassName
    //    //{
    //    //    get
    //    //    {
    //    //        return Common.Security.Process;
    //    //    }
    //    //}
    //    //#endregion

    //    //#region Read
    //    //#region Permissions
    //    //internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
    //    //public Dictionary<Int64, Security.Entities.Permission> Permissions
    //    //{
    //    //    get
    //    //    {
    //    //        if (_Permissions == null)
    //    //        { _Permissions = new Security.Collections.Permissions(Credential).Items(this); }
    //    //        return _Permissions;
    //    //    }
    //    //}
    //    //#endregion
    //    ////ALL
    //    //public List<Security.Entities.RightPost> SecurityPosts()
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadPostByClassName(this);
    //    //}
    //    //public List<Security.Entities.RightJobTitle> SecurityJobTitles()
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadJobTitleByClassName(this);
    //    //}
    //    ////por ID
    //    //public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadByJobTitleObjectPermissionParent(jobTitle, this, permission);
    //    //}
    //    //public Security.Entities.RightPost ReadPostByID(DS.Entities.Post post, Security.Entities.Permission permission)
    //    //{
    //    //    return new Security.Collections.Rights(Credential).ReadByPostObjectPermissionParent(post, this, permission);
    //    //}

    //    //#endregion

    //    //#region Write
    //    ////Security Add
    //    //public Security.Entities.RightPost SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa para cuando se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    return new Security.Collections.Rights(Credential).Add(this, post, permission, this, roleType);
    //    //}
    //    //public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa cuando el alta se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    return new Security.Collections.Rights(Credential).Add(this, jobTitle, permission, this, roleType);
    //    //}
    //    ////Uso interno
    //    //public void SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission, ISecurity parent, Security.Entities.RoleType roleType)
    //    //{//se usa cuando el alta se hereda el alta 
    //    //    new Security.Collections.Rights(Credential).Add(this, post, permission, parent, roleType);
    //    //}
    //    ////Uso Interno
    //    //public void SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, ISecurity parent, Security.Entities.RoleType roleType)
    //    //{//se usa para cuando se hereda el alta
    //    //    new Security.Collections.Rights(Credential).Add(this, jobTitle, permission, parent, roleType);
    //    //}

    //    ////Security Remove
    //    //public void SecurityPostRemove(DS.Entities.Post post, Security.Entities.Permission permission)
    //    //{//se usa para cuando se hereda el borrado
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    new Security.Collections.Rights(Credential).Remove(post, this, permission);
    //    //}
    //    //public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
    //    //{//se usa para cuando se hereda el borrado
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    new Security.Collections.Rights(Credential).Remove(jobTitle, this, permission);
    //    //}
    //    ////Uso Interno
    //    //public void SecurityPostRemove()
    //    //{//se usa cuando la baja se hace de este elemento
    //    //    new Security.Collections.Rights(Credential).Remove(this);
    //    //}
    //    ////Uso Interno
    //    //public void SecurityJobTitleRemove()
    //    //{//se usa cuando la baja se hace de este elemento
    //    //    new Security.Collections.Rights(Credential).Remove(this);
    //    //}

    //    ////Security Modify
    //    //public Security.Entities.RightPost SecurityPostModify(Security.Entities.RightPost oldRightPost, DS.Entities.Post post, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa cuando la modificacion se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    //se borra con sus herederos
    //    //    this.SecurityPostRemove(post, oldRightPost.Permission);
    //    //    //se da de alta con sus herederos
    //    //    this.SecurityPostAdd(post, permission, roleType);
    //    //    //retorma el right
    //    //    return new Condesus.EMS.Business.Security.Entities.RightPost(permission, post, roleType);
    //    //}
    //    //public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission, Security.Entities.RoleType roleType)
    //    //{//se usa cuando la modificacion se hace de este elemento
    //    //    if (!new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage)) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
    //    //    //se borra con sus herederos
    //    //    this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
    //    //    //se da de alta con sus herederos
    //    //    this.SecurityJobTitleAdd(jobTitle, permission, roleType);
    //    //    //retorma el right
    //    //    return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle, roleType);
    //    //}

    //    //public void InheritPermissions(ISecurity owner)
    //    //{
    //    //    //copia todos los permisos de post del owner que se le asigno
    //    //    foreach (Security.Entities.RightPost _rightPost in owner.SecurityPosts())
    //    //    {
    //    //        SecurityPostAdd(_rightPost.Post, _rightPost.Permission, _rightPost.Permission.OwnerPermission, _rightPost.RoleType);
    //    //    }
    //    //    //copia todos los permisos de jobtites del owner que se le asigno
    //    //    foreach (Security.Entities.RightJobTitle _rightJobTitle in owner.SecurityJobTitles())
    //    //    {
    //    //        SecurityJobTitleAdd(_rightJobTitle.JobTitle, _rightJobTitle.Permission, _rightJobTitle.Permission.OwnerPermission, _rightJobTitle.RoleType);
    //    //    }
    //    //}
    //    //#endregion

    //    #endregion

    //    #endregion
    //}
}

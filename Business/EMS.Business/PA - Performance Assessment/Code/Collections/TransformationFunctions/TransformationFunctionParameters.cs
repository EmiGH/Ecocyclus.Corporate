using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    /// <summary>
    /// Esta clase no va a la base, construye los parametros de las TransformationFunction 
    /// </summary>
    //internal class TransformationFunctionParameters
    //{
    //    private Entities.TransformationFunction _TransformationFunction;

    //    internal TransformationFunctionParameters(Entities.TransformationFunction transformationFunction)
    //    {
    //        _TransformationFunction = transformationFunction;
    //    }

    //    #region Read Functions
    //    internal Entities.TransformationFunctionParameter Item(String name)
    //    {
    //        Entities.TransformationFunctionParameter _item = null;

    //        _item = Items()[name];

    //        return _item;
    //    }
    //    internal Dictionary<String, Entities.TransformationFunctionParameter> Items()
    //    {
    //        Dictionary<String, Entities.TransformationFunctionParameter> _items = new Dictionary<String, Entities.TransformationFunctionParameter>();

    //        _items = CreateParameters(_TransformationFunction.IdFunction);

    //        return _items;
    //    }

    //    #endregion



    //    /// <summary>
    //    /// En este metodo se crean las funciones
    //    /// </summary>
    //    /// <returns></returns>
    //    private Dictionary<String, Entities.TransformationFunctionParameter> CreateParameters(Int64 idFunction)
    //    {
    //        Dictionary<String, Entities.TransformationFunctionParameter> _items = new Dictionary<String, Entities.TransformationFunctionParameter>();

    //        switch (idFunction)
    //        {
    //            case 1:
    //                //Crea parametros de la Function 1, Transformacion lineal
    //                Entities.TransformationFunctionParameter _paremeter1 = new Entities.TransformationFunctionParameter("FactorEmision", "Medicion por Factor de Emision");
    //                _items.Add(_paremeter1.Name, _paremeter1);
    //                break;
    //            case 2:
    //                break;
    //            default:
    //                break;
    //        }
    //        return _items;
    //    }

        


    //}
}

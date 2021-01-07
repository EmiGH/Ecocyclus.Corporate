using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    /// <summary>
    /// Esta clase no va a la base, construye tantas TransformationFunction como ecuaciones haya en TransformationEquation y toma los datos de ahi
    /// </summary>
    //internal class TransformationFunctions
    //{
    //    private Entities.CalculationOfTransformation _CalculationOfTransformation;

    //    internal TransformationFunctions(Entities.CalculationOfTransformation calculationOfTransformation)
    //    {
    //        _CalculationOfTransformation = calculationOfTransformation;
    //    }

    //    internal TransformationFunctions(Entities.ConfigurationPA configurationPA)
    //    {
            
    //    }

    //    #region Read Functions
    //    internal Entities.TransformationFunction Item(Int64 idFunction)
    //    {
    //        Entities.TransformationFunction _item = null;

    //        _item = Items()[idFunction];

    //        return _item;
    //    }
    //    internal Dictionary<Int64, Entities.TransformationFunction> Items()
    //    {
    //        Dictionary<Int64, Entities.TransformationFunction> _items = new Dictionary<Int64, Entities.TransformationFunction>();

    //        _items = CreateFunctions();

    //        return _items;
    //    }

    //    #endregion



    //    /// <summary>
    //    /// En este metodo se crean las funciones
    //    /// </summary>
    //    /// <returns></returns>
    //    private Dictionary<Int64, Entities.TransformationFunction> CreateFunctions()
    //    {
    //        Dictionary<Int64, Entities.TransformationFunction> _items = new Dictionary<Int64, Entities.TransformationFunction>();

    //        //Crea Function 1, Transformacion lineal
    //        Int64 _idFuncion1 = 1;
    //        String _title1 = "Medicion por Factor de Emision";
    //        String _literalFormula1 = "R = M * Fe";
    //        String _description1 = "'M' es la base, medicion a transformar, 'Fe' es el Factor de Emision";
    //        String _equation1 = "TransformacionLineal";
    //        Entities.TransformationFunction _function1 = new Entities.TransformationFunction(_idFuncion1, _title1, _literalFormula1, _description1, _equation1);
    //        _items.Add(_function1.IdFunction, _function1);

    //        ////Crea Function 2, 
    //        //Int64 _idFuncion2 = 2;
    //        //String _title2 = "";
    //        //String _literalFormula2 = "";
    //        //String _description2 = "";
    //        //String _equation2 = "";
    //        //Entities.TransformationFunction _function2 = new Entities.TransformationFunction(_idFuncion2, _title2, _literalFormula2, _description2, _equation2);
    //        //_items.Add(_function2.IdFunction, _function2);

    //        return _items;
    //    }

        


    //}
}

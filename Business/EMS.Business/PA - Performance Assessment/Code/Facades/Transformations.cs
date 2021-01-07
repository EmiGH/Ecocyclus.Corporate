using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA
{
    public class Transformations
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal Credential Credential
        {
            get { return _Credential; }
        }

        #region Public Properties (Facades of System)

        public Entities.CalculateOfTransformation CalculateOfTransformation(Int64 id)
        {
            
                return new Collections.CalculateOfTransformations(this, _Credential).Item(id);
            
        }
        public Dictionary<Int64, Entities.CalculateOfTransformation> CalculateOfTransformations
        {
            get
            {
                return new Collections.CalculateOfTransformations(this, _Credential).Items(); 
            }
        }
        //public Dictionary<Int64, Entities.CalculateOfTransformation> CalculateOfTransformations
        //{
        //    get 
        //    {

        //        Dictionary<Int64, Entities.CalculateOfTransformation> _oItems = new Dictionary<long, Condesus.EMS.Business.PA.Entities.CalculateOfTransformation>();

        //        //return new Collections.CalculateOfTransformations(this, _Credential).Items(); 

        //        foreach (Entities.CalculateOfTransformation _Calculate in new Collections.CalculateOfTransformations(this, _Credential).Items().Values)
        //        {
        //            foreach (Entities.CalculateOfTransformation _CalculateChild in _Calculate.TransformationsParameters.Values)
        //            {                        
        //                if (!_oItems.ContainsKey(_CalculateChild.IdTransformation))
        //                {
        //                    _oItems.Add(_CalculateChild.IdTransformation, _CalculateChild);
        //                }
        //                CalculateOfTransformationsParameters(ref _oItems, _CalculateChild);
        //            }
        //            if (!_oItems.ContainsKey(_Calculate.IdTransformation))
        //            {
        //                _oItems.Add(_Calculate.IdTransformation, _Calculate);
        //            }
        //            foreach (Entities.CalculateOfTransformation _CalculateChild in _Calculate.Transformations.Values)
        //            {
        //                if (!_oItems.ContainsKey(_CalculateChild.IdTransformation))
        //                {
        //                    _oItems.Add(_CalculateChild.IdTransformation, _CalculateChild);
        //                }
        //                CalculateOfTransformationsChild(ref _oItems, _CalculateChild);
        //            }
                    
        //        }

        //        return _oItems;
        //    }
        //}
        //private Dictionary<Int64, Entities.CalculateOfTransformation> CalculateOfTransformationsParameters(ref Dictionary<Int64, Entities.CalculateOfTransformation> oItems, Entities.CalculateOfTransformation calculate)
        //{
        //    foreach (Entities.CalculateOfTransformation _CalculateChild in calculate.TransformationsParameters.Values)
        //    {                
        //        if (!oItems.ContainsKey(_CalculateChild.IdTransformation))
        //        {
        //            oItems.Add(_CalculateChild.IdTransformation, _CalculateChild);
        //        }
        //        CalculateOfTransformationsParameters(ref oItems, _CalculateChild);
        //    }
            
        //    return oItems;
        //}
        //private Dictionary<Int64, Entities.CalculateOfTransformation> CalculateOfTransformationsChild(ref Dictionary<Int64, Entities.CalculateOfTransformation> oItems, Entities.CalculateOfTransformation calculate)
        //{
        //    foreach (Entities.CalculateOfTransformation _CalculateChild in calculate.Transformations.Values)
        //    {
        //        if (!oItems.ContainsKey(_CalculateChild.IdTransformation))
        //        {
        //            oItems.Add(_CalculateChild.IdTransformation, _CalculateChild);
        //        }
        //        CalculateOfTransformationsChild(ref oItems, _CalculateChild);
        //    }

        //    return oItems;
        //}


        public Dictionary<Int64, Entities.CalculateOfTransformation> CalculateOfTransformationErrors
        {
            get { return new Collections.CalculateOfTransformations(this).Items(); }
        }

        #endregion

        internal Transformations(Credential credential)
        {
            _Credential = credential;
        }
    }
}

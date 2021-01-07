using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculateOfTransformationParameter
    {
        private Credential _Credential;
        private Int64 _IdTransformation;
        private String _IdParameter;
        private Int64 _IdObject;
        private String _ClassName;
        private IOperand _Operand;

        public Int64 IdTransformation
        {
            get { return _IdTransformation; }
        }
        public String IdParameter
        {
            get { return _IdParameter; }
        }
        public Int64 IdObject
        {
            get { return _IdObject; }
        }
        public String ClassName
        {
            get { return _ClassName; }
        }
        public IOperand Operand
        {
            get
            {
                if(_Operand==null)
                { _Operand = new OperandFactory().CreateOperand(_IdObject, _ClassName, _Credential);}
                return _Operand;
            }
        }

        internal CalculateOfTransformationParameter(Int64 idTransformation, String idParameter, Int64 idObject, String className, Credential credential)
        {
            _Credential = credential;
            _IdTransformation = idTransformation;
            _IdParameter = idParameter;
            _IdObject = idObject;
            _ClassName = className;
        }
    }
}

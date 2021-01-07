using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class CalculateOfTransformations
    {
        #region Internal Properties
        private ICollectionItems _Datasource;
        private Credential _Credential;
        #endregion

        internal CalculateOfTransformations(Credential credential)
        {
            _Credential = credential;
        }

        internal CalculateOfTransformations(Entities.Measurement measurement)
        {
            _Credential = measurement.Credential;
            _Datasource = new CalculateOfTransformationRead.CalculateOfTransformationByMeasurements(measurement);

        }

        internal CalculateOfTransformations(Entities.Indicator indicator)
        {
            _Credential = indicator.Credential;
            _Datasource = new CalculateOfTransformationRead.CalculateOfTransformationByIndicator(indicator);

        }
        internal CalculateOfTransformations(Entities.Constant constant)
        {
            _Credential = constant.Credential;
            _Datasource = new CalculateOfTransformationRead.CalculateOfTransformationByConstant(constant);

        }

        internal CalculateOfTransformations(PF.Entities.ProcessGroupProcess processGroupProcess)
        {
            _Credential = processGroupProcess.Credential;
            _Datasource = new CalculateOfTransformationRead.CalculateOfTransformationByProcess(processGroupProcess);

        }

        internal CalculateOfTransformations(Entities.CalculateOfTransformation calculateOfTransformation)
        {
            _Credential = calculateOfTransformation.Credential;
            _Datasource = new CalculateOfTransformationRead.CalculateOfTransformationByTransformation(calculateOfTransformation);
        }

        internal CalculateOfTransformations(Transformations transformations)
        {
            _Credential = transformations.Credential;
            _Datasource = new CalculateOfTransformationRead.CalculateOfTransformationReadWhitErrors(transformations);
        }

        internal CalculateOfTransformations(Transformations transformations, Credential credential)
        {
            _Credential = transformations.Credential;
            _Datasource = new CalculateOfTransformationRead.CalculateOfTransformationReadAll(transformations);
        }

        #region Read Functions
        internal Entities.CalculateOfTransformation Item(Int64 idTransformation)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Entities.CalculateOfTransformation _item = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformations_ReadById(idTransformation, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdTransformation", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                ITransformer _baseTransformer = new TransformerFactory().CreateTransformer(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementTransformer"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTransformationTransformer"], 0)), _Credential);

                _item = new Entities.CalculateOfTransformation(Convert.ToInt64(_dbRecord["IdTransformation"]), _baseTransformer, Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdMeasurementOrigin"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Formula"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
            }
            return _item;
        }
        internal Dictionary<Int64, Entities.CalculateOfTransformation> Items()
        {
            Dictionary<Int64, Entities.CalculateOfTransformation> _items = new Dictionary<Int64, Entities.CalculateOfTransformation>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdTransformation", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                ITransformer _baseTransformer = new TransformerFactory().CreateTransformer(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementTransformer"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTransformationTransformer"], 0)), _Credential);

                Entities.CalculateOfTransformation _item = new Entities.CalculateOfTransformation(Convert.ToInt64(_dbRecord["IdTransformation"]), _baseTransformer, Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdMeasurementOrigin"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"],0)), Convert.ToString(_dbRecord["Formula"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                _items.Add(_item.IdTransformation, _item);
            }
            return _items;
        }
        //solo lo usa cuando una transformacion se resetea 
        internal Dictionary<Int64, Entities.CalculateOfTransformation> ItemsAsParameter(Entities.CalculateOfTransformation transformation)
        {
            Dictionary<Int64, Entities.CalculateOfTransformation> _items = new Dictionary<Int64, Entities.CalculateOfTransformation>();
            
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformations_ReadByTransformationAsParameter(transformation.IdTransformation, transformation.Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdTransformation", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                ITransformer _baseTransformer = new TransformerFactory().CreateTransformer(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementTransformer"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTransformationTransformer"], 0)), _Credential);

                Entities.CalculateOfTransformation _item = new Entities.CalculateOfTransformation(Convert.ToInt64(_dbRecord["IdTransformation"]), _baseTransformer, Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdMeasurementOrigin"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Formula"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                _items.Add(_item.IdTransformation, _item);
            }
            return _items;
        }
        internal Dictionary<Int64, Entities.CalculateOfTransformation> ItemsTransformationParameter(Entities.CalculateOfTransformation transformation)
        {
            Dictionary<Int64, Entities.CalculateOfTransformation> _items = new Dictionary<Int64, Entities.CalculateOfTransformation>();

            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformations_ReadTransformationParameter(transformation.IdTransformation, transformation.Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdTransformation", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                ITransformer _baseTransformer = new TransformerFactory().CreateTransformer(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementTransformer"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTransformationTransformer"], 0)), _Credential);

                Entities.CalculateOfTransformation _item = new Entities.CalculateOfTransformation(Convert.ToInt64(_dbRecord["IdTransformation"]), _baseTransformer, Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdMeasurementOrigin"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Formula"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                _items.Add(_item.IdTransformation, _item);
            }
            return _items;
        }
        //solo lo usa cuando una transformacion se resetea 
        internal Dictionary<Int64, Entities.CalculateOfTransformation> ItemsAsParameter(Entities.Measurement measurement)
        {
            Dictionary<Int64, Entities.CalculateOfTransformation> _items = new Dictionary<Int64, Entities.CalculateOfTransformation>();

            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformations_ReadByMeasurementAsParameter(measurement.IdMeasurement, measurement.Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdTransformation", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                ITransformer _baseTransformer = new TransformerFactory().CreateTransformer(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementTransformer"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTransformationTransformer"], 0)), _Credential);

                Entities.CalculateOfTransformation _item = new Entities.CalculateOfTransformation(Convert.ToInt64(_dbRecord["IdTransformation"]), _baseTransformer, Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdMeasurementOrigin"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Formula"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                _items.Add(_item.IdTransformation, _item);
            }
            return _items;
        }
        internal Double ReadOperateValue(Entities.CalculateOfTransformation transformation, DateTime startDate, DateTime endDate)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Double _operateValue = Double.MinValue;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformations_ReadOperateValue(transformation.IdTransformation, transformation.Indicator.IsCumulative, startDate, endDate);
            //por cada valor...
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_dbRecord["Value"] != System.DBNull.Value)
                {
                    _operateValue = Convert.ToDouble(_dbRecord["Value"]);
                }
            }
            return _operateValue;
        }
   
        #endregion

        #region Write Functions      
        internal Entities.CalculateOfTransformation Add(Entities.Indicator indicator, Entities.MeasurementUnit measurementUnit, 
            ITransformer baseTransformer, String formula, String name, String description, PF.Entities.ProcessGroupProcess processGroupProcess, 
            Entities.Measurement measurementOrigin, Entities.AccountingActivity activity, Dictionary<String, IOperand> operands, List<NT.Entities.NotificationRecipient> notificationRecipients)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                Int64 _idMeasurementTransformer = 0;
                Int64 _idTransformationTransformer = 0;
                Int64 _IdActivity = activity == null ? 0 : activity.IdActivity;

                switch(baseTransformer.ClassName)
                {
                    case Common.ClassName.Measurement:
                        _idMeasurementTransformer = baseTransformer.IdObject;
                        break;
                    case Common.ClassName.CalculateOfTransformation :
                        _idTransformationTransformer = baseTransformer.IdObject;
                        break;
                    default:
                        break;
                }
                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idTransformation = _dbPerformanceAssessments.CalculateOfTransformations_Create(_idMeasurementTransformer, _idTransformationTransformer, indicator.IdIndicator, formula, measurementUnit.IdMeasurementUnit, processGroupProcess.IdProcess, measurementOrigin.IdMeasurement, _IdActivity);
                //alta del lg
                _dbPerformanceAssessments.CalculateOfTransformations_LG_Create(_idTransformation, _Credential.DefaultLanguage.IdLanguage, name, description);
                //crea el objeto 
                Entities.CalculateOfTransformation _item = new Entities.CalculateOfTransformation(_idTransformation, baseTransformer, indicator.IdIndicator, measurementUnit.IdMeasurementUnit, processGroupProcess.IdProcess, measurementOrigin.IdMeasurement, _IdActivity, formula, name, description, _Credential.DefaultLanguage.IdLanguage, _Credential);
                //Alta de los parametros
                foreach (KeyValuePair<String, IOperand> _operand in operands)
                {
                    _item.ParameterAdd(_operand.Key, _operand.Value);
                }
                //Alta de los emails
                foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                {
                    switch (_notificationRecipient.GetType().Name)
                    {
                        case "NotificationRecipientEmail":
                            _item.NotificationRecipientAdd(_notificationRecipient.Email);
                            break;
                        case "NotificationRecipientPerson":
                            NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                            _item.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                            break;
                        default:
                            break;
                    }
                }
                //Log
                _dbLog.Create("PA_CalculateOfTransformations", "CalculateOfTransformations", "Add", "IdTransformation=" + _idTransformation, _Credential.User.IdPerson);
                //Devuelvo el objeto creado
                return _item;
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        internal void Modify(Entities.CalculateOfTransformation calculateOfTransformation, Entities.Indicator indicator, 
            Entities.MeasurementUnit measurementUnit, ITransformer baseTransformer, String formula, String name, String description,
            PF.Entities.ProcessGroupProcess processGroupProcess, Entities.Measurement measurementOrigin, Entities.AccountingActivity activity, Dictionary<String, IOperand> operands, 
            List<NT.Entities.NotificationRecipient> notificationRecipients)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                Int64 _idMeasurementTransformer = 0;
                Int64 _idTransformationTransformer = 0;
                Int64 _IdActivity = activity == null ? 0 : activity.IdActivity;

                switch (baseTransformer.ClassName)
                {
                    case Common.ClassName.Measurement:
                        _idMeasurementTransformer = baseTransformer.IdObject;
                        break;
                    case Common.ClassName.CalculateOfTransformation:
                        _idTransformationTransformer = baseTransformer.IdObject;
                        break;
                    default:
                        break;
                }

                //Modifico los datos de la base
                _dbPerformanceAssessments.CalculateOfTransformations_Update(calculateOfTransformation.IdTransformation, _idMeasurementTransformer, _idTransformationTransformer, indicator.IdIndicator, formula, measurementUnit.IdMeasurementUnit, processGroupProcess.IdProcess, measurementOrigin.IdMeasurement, _IdActivity);
                //alta del lg
                _dbPerformanceAssessments.CalculateOfTransformations_LG_Update(calculateOfTransformation.IdTransformation, _Credential.DefaultLanguage.IdLanguage, name, description);
                //Borra todos los parametros
                foreach (KeyValuePair<String, IOperand> _operand in operands)
                {
                    new CalculateOfTransformationParameters(calculateOfTransformation).Remove();
                }
                //Alta de los parametros
                foreach (KeyValuePair<String, IOperand> _operand in operands)
                {
                    calculateOfTransformation.ParameterAdd(_operand.Key, _operand.Value);
                }
                //Borra los email de notificacion
                calculateOfTransformation.NotificationRecipientRemove();
                // alta de los email de notificacion
                //Alta de los emails
                foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                {
                    switch (_notificationRecipient.GetType().Name)
                    {
                        case "NotificationRecipientEmail":
                            calculateOfTransformation.NotificationRecipientAdd(_notificationRecipient.Email);
                            break;
                        case "NotificationRecipientPerson":
                            NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                            calculateOfTransformation.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                            break;
                        default:
                            break;
                    }
                }
                //Log
                _dbLog.Create("PA_CalculateOfTransformations", "CalculateOfTransformations", "Modify", "IdTransformation=" + calculateOfTransformation.IdTransformation, _Credential.User.IdPerson);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        internal void Remove(Entities.CalculateOfTransformation calculateOfTransformation)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //borra dependencias
                calculateOfTransformation.Remove();

                //se borra si es parametro de otro calculo
                _dbPerformanceAssessments.CalculateOfTransformationParameterTransformations_DeleteAsParameter(calculateOfTransformation.IdTransformation);
                //Borra LG
                _dbPerformanceAssessments.CalculateOfTransformations_LG_Delete(calculateOfTransformation.IdTransformation);
                //Borra registro
                _dbPerformanceAssessments.CalculateOfTransformations_Delete(calculateOfTransformation.IdTransformation);
                //Log
                _dbLog.Create("PA_CalculateOfTransformations", "CalculateOfTransformations", "Remove", "IdTransformation=" + calculateOfTransformation.IdTransformation, _Credential.User.IdPerson);

            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                }
                throw ex;
            }
        }
        #endregion



    }
}

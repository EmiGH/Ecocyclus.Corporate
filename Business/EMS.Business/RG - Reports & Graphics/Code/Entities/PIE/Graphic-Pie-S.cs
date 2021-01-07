using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.RG.Entities
{
    public class Graphic_Pie_S : IGraphicPie
    {

        #region Internal Properties
        private Int64 _Id;
        private String _Name;
        private Decimal _Value;
        private Decimal _Percentage;
        #endregion

        #region Icolums
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        //nombrr de la entidad
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public Decimal Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        public Decimal Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }
        #endregion

        internal Graphic_Pie_S(Int64 id, String name, Decimal value, Decimal percentage)
        {
            _Id = id;
            _Name = name;
            _Value = value;
            _Percentage = percentage;
        }
    }
}

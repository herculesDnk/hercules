//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hercules.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class alarms
    {
        public int ID { get; set; }
        public int MessageID { get; set; }
        public string LoggerSMSNumber { get; set; }
        public string AlarmText { get; set; }
        public Nullable<bool> Acknowledged { get; set; }
        public Nullable<System.DateTime> AlarmDate { get; set; }
    }
}

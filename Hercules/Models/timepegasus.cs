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
    
    public partial class timepegasus
    {
        public int ID { get; set; }
        public Nullable<int> loggerid { get; set; }
        public Nullable<int> timetype { get; set; }
        public Nullable<System.TimeSpan> time { get; set; }
        public Nullable<double> pressure { get; set; }
        public Nullable<System.DateTime> lastdatereceived { get; set; }
    }
}

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
    
    public partial class statuspegasus
    {
        public int ID { get; set; }
        public Nullable<bool> control { get; set; }
        public Nullable<int> control_type { get; set; }
        public Nullable<double> target_pressure { get; set; }
        public Nullable<int> sol_up_ms { get; set; }
        public Nullable<int> sol_down_ms { get; set; }
        public Nullable<int> deadband_dm { get; set; }
        public Nullable<int> sec_calc_flow { get; set; }
        public Nullable<int> loggerID { get; set; }
        public Nullable<System.DateTime> lastdatereceived { get; set; }
        public Nullable<int> gain { get; set; }
    }
}

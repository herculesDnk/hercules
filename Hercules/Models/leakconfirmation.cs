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
    
    public partial class leakconfirmation
    {
        public int ID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<System.DateTime> LeakConfirmed { get; set; }
        public Nullable<System.DateTime> LeakDetected { get; set; }
        public string TypeOfLeak { get; set; }
        public Nullable<System.DateTime> LeakRepair { get; set; }
        public Nullable<int> LeakComfirmationAssocaitions { get; set; }
        public string LeakSize { get; set; }
        public string EstimatedGPM { get; set; }
        public string CostPer1000 { get; set; }
        public Nullable<int> EstimatedUnits { get; set; }
    }
}

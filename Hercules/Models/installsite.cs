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
    
    public partial class installsite
    {
        public int Id { get; set; }
        public string SiteId { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string SMPArea { get; set; }
        public string NodeRef { get; set; }
        public string FLOC { get; set; }
        public Nullable<int> AccountID { get; set; }
    }
}

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
    
    public partial class steptesthistory
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public Nullable<System.DateTime> TestDate { get; set; }
        public Nullable<int> TestDuration { get; set; }
        public string SiteIdOld { get; set; }
        public Nullable<int> SiteId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Status { get; set; }
    }
}

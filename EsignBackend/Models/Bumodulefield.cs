using System;
using System.Collections.Generic;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Bumodulefield
    {
        public int Id { get; set; }
        public string Tname { get; set; }
        public string Tdisplay { get; set; }
        public string Ttype { get; set; }
        public double? Tlength { get; set; }
        public string Tdefault { get; set; }
        public string Tdir { get; set; }
        public string Tcontrol { get; set; }
        public int? Tpriority { get; set; }
        public int? Tid { get; set; }
        public string Tfilter { get; set; }
        public string Tmultiupdate { get; set; }
        public string Tdata { get; set; }
        public string Tmust { get; set; }
        public string Thelp { get; set; }
        public double? Tmin { get; set; }
        public double? Tmax { get; set; }
        public string Tlistupdate { get; set; }
        public string Tnotr { get; set; }
        public string Tlist { get; set; }
    }
}

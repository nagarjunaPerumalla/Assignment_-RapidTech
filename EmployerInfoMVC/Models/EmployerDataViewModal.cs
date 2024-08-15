using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployerInfoMVC.Models
{
    public class EmployerDataViewModal
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string EntryNotes { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
    public class DataPoint
    {
        public string label { get; set; }

        public int y { get; set; }
    }
    public class Employerdata
    {
        public string EmployeeName { get; set; }

        public int TotalTime { get; set; }
    }
}
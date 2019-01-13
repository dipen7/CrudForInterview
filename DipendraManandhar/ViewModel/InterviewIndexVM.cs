using DipendraManandhar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DipendraManandhar.ViewModel
{
    public class InterviewIndexVM
    {
        public InterviewIndexVM()
        {
            indexlist = new List<Interview>();
            droplist = new List<InterviewDrop>();
        }
        public IEnumerable<Interview> indexlist { get; set; }
        public IEnumerable<InterviewDrop> droplist { get; set; }
    }
}
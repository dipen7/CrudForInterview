using DipendraManandhar.Models;
using System.Collections.Generic;

namespace DipendraManandhar.ViewModel
{
    public class InterviewAddEdit
    {
        public InterviewAddEdit()
        {
            InterviewObj = new Interview();
            Droplist = new List<InterviewDrop>(); 
        }
        public Interview InterviewObj { get; set; }
        public IEnumerable<InterviewDrop> Droplist { get; set; }
    }
}
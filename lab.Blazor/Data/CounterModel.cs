using System;
using System.ComponentModel.DataAnnotations;

namespace lab.Blazor.Data
{
    public class CounterModel
    {
        [DataType("int")]
        [Range(0, int.MaxValue)]
        public int CounterValue { get; set; }
    }
}

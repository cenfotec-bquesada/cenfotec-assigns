using System;
using System.Collections.Generic;

namespace rmapp.Data
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? Due { get; set; }
    }
}

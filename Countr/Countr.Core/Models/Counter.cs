using System;
using System.Collections.Generic;
using System.Text;
using SQLite; //give acress the the SQLite classes

/// <summary>
/// This data model  stores and updates counters
/// </summary>

namespace Countr.Core.Models
{
    public class Counter
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; } //Id property is an audto incrementing primary key

        public string Name { get; set; }
        public int Count { get; set; }
    }
}

using System;
using SQLite;
namespace Timera
{
    public class Song
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BeatsPerMinute { get; set; }
    }
}

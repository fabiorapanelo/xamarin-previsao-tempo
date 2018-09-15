using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace AppPrevisaoDoTempo
{
    public class Local
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nome { get; set; }
    }
}

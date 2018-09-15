using System;
using System.Collections.Generic;
using System.Text;

namespace AppPrevisaoDoTempo
{
    public class Tempo
    {
        // Because labels bind to these values, set them to an empty string to
        // ensure that the label appears on all platforms by default.
        public string Titulo { get; set; } = " ";
        public string Temperatura { get; set; } = " ";
        public string Vento { get; set; } = " ";
        public string Humidade { get; set; } = " ";
        public string Visibilidade { get; set; } = " ";
        public string NascerDoSol { get; set; } = " ";
        public string PorDoSol { get; set; } = " ";
        public string ImageUrl { get; set; } = " ";
        public Local Local { get; set; }
    }
}

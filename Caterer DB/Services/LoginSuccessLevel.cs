﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caterer_DB.Services
{
    public enum LoginSuccessLevel
    {
        
            Erfolgreich = 1,
            BenutzerNichtGefunden = 2,
            PasswortFalsch = 3,
            DatenbankFehler = 4,
            Unbekannt = 5
        
    }
}
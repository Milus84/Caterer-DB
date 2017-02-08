﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caterer_DB.Models
{

    public class CreateMailViewModel
    {
        public string Absender { get; set; }
        public string Empfaenger { get; set; }
        public string Betreff { get; set; }
        public string Inhalt { get; set; }
        public string SMTPSeverName { get; set; }
        public string UserName { get; set; }
        public string Passwort { get; set; }
        public int Port { get; set; }
        public List<string> FehlerListe { get; set; }
    }


}
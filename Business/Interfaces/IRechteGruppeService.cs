﻿using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRechteGruppeService
    {
        RechteGruppe SearchRightGroupById(int id);
        List<RechteGruppe> FindAllRightGroups();
        void AddRechteGruppe(RechteGruppe rechteGruppe);
        void EditRechteGruppe(RechteGruppe rechteGruppe);
        void RemoveRechteGruppe(int id);
    }
}

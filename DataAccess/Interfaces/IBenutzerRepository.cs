﻿using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBenutzerRepository
    {
        void AddUser(Benutzer benutzer);
        void EditUser(Benutzer benutzer);
        void RemoveUser(Benutzer benutzer);

        Benutzer SearchUserById(int id);
        Benutzer SearchUserByIdNoTracking(int id);
        Benutzer SearchUserByEMail(string eMail);
        Benutzer SearchUserByEmailVerify(string verify);

        List<Benutzer> SearchUser();
        List<Benutzer> SearchUserByCity(string city);
        List<Benutzer> SearchUserByPostcode(string postcode);
        List<Benutzer> SearchUserBySurname(string surname);
        List<Benutzer> SearchAllMitarbeiterWithPaging(int aktuelleSeite, int seitenGroesse);
        List<Benutzer> SearchAllCatererWithPaging(int aktuelleSeite, int seitenGroesse);

        int GetMitarbeiterCount();
        int GetCatererCount();


    }
}

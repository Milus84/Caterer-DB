﻿using Caterer_DB.Models;
using DataAccess.Model;
using System.Collections.Generic;

namespace Caterer_DB.Interfaces
{
    public interface IBenutzerViewModelService
    {
        AnmeldenBenutzerViewModel GeneriereAnmeldenBenutzerViewModel(Benutzer benutzer);
        AnmeldenBenutzerViewModel GeneriereAnmeldenBenutzerViewModel(int benutzerId = -1, string infobox = "");


       
        Benutzer Map_CreateMitarbeiterViewModel_Benutzer(CreateMitarbeiterViewModel createBenutzerViewModel);
        Benutzer Map_CreateCatererViewModel_Benutzer(CreateCatererViewModel createCatererViewModel);
        Benutzer Map_EditBenutzerViewModel_Benutzer(EditBenutzerViewModel editBenutzerViewModel);
        Benutzer Map_MyDataBenutzerViewModel_Benutzer(MyDataBenutzerViewModel myDataBenutzerViewModel);
        Benutzer Map_DeleteBenutzerViewModel_Benutzer(DeleteBenutzerViewModel deleteBenutzerViewModel);
        Benutzer Map_RegisterBenutzerViewModel_Benutzer(RegisterBenutzerViewModel registerBenutzerViewModel);
        Benutzer Map_ForgottenPasswordRequestViewModel_Benutzer(ForgottenPasswordRequestViewModel forgottenPasswordRequestViewModel);
        Benutzer Map_ForgottenPasswordCreateNewPasswordViewModel_Benutzer(ForgottenPasswordCreateNewPasswordViewModel forgottenPasswordCreateNewPasswordViewModel);

        IndexBenutzerViewModel GeneriereIndexBenutzerViewModel(Benutzer benutzer);
        EditBenutzerViewModel Map_Benutzer_EditBenutzerViewModel(Benutzer benutzer);
        MyDataBenutzerViewModel Map_Benutzer_MyDataBenutzerViewModel(Benutzer benutzer);
        DetailsBenutzerViewModel Map_Benutzer_DetailsBenutzerViewModel(Benutzer benutzer);
        DeleteBenutzerViewModel Map_Benutzer_DeleteBenutzerViewModel(Benutzer benutzer);
        RegisterBenutzerViewModel CreateNewRegisterBenutzerViewModel();
        DetailsCatererViewModel Map_Benutzer_DetailsCatererViewModel(Benutzer benutzer);

        CreateMitarbeiterViewModel CreateNewCreateMitarbeiterViewModel();
        CreateMitarbeiterViewModel UpdateCreateMitarbeiterViewModel(CreateMitarbeiterViewModel createMitarbeiterViewModel);

        ForgottenPasswordRequestViewModel Map_Benutzer_ForgottenPasswordRequestViewModel(Benutzer benutzer);
        ForgottenPasswordCreateNewPasswordViewModel Map_Benutzer_ForgottenPasswordCreateNewPasswordViewModel(Benutzer benutzer);
        ForgottenPasswordCreateNewPasswordViewModel Get_ForgottenPasswordCreateNewPasswordViewModel_ByBenutzerId(int id);

        RegisterBenutzerViewModel AddListsToRegisterViewModel(RegisterBenutzerViewModel registerBenutzerViewModel);
        MyDataBenutzerViewModel AddListsToMyDataViewModel(MyDataBenutzerViewModel myDataBenutzerViewModel);
        CreateMitarbeiterViewModel AddListsToCreateViewModel(CreateMitarbeiterViewModel createBenutzerViewModel);
        CreateCatererViewModel AddListsToCreateCatererViewModel(CreateCatererViewModel createCatererViewModel);

        ListViewModel<IndexBenutzerViewModel> GeneriereListViewModel(List<Benutzer> benutzerListe, int gesamtAnzahlDatensätze, int aktuelleSeite = 1, int seitenGröße = 10);
        ListViewModel<IndexCatererViewModel> GeneriereListViewModelCaterer(List<Benutzer> benutzerListe, int gesamtAnzahlDatensätze, int aktuelleSeite = 1, int seitenGröße = 10);
        IndexCatererViewModel GeneriereIndexCatererViewModel(Benutzer benutzer);
        CreateCatererViewModel CreateNewCreateCatererViewModel();
    }
}

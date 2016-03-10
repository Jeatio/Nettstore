using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
namespace Nettbutikk.DAL
{
    public interface IOrdreRepository
    {
        bool endreAntallVarer(int ant, int ordrenummer, string kundeEpost);
        bool KjøpVarer(string kundeEpost);
        bool leggIHandlevogn(int vareid, string kundeEmail);
        int RegnPris(string kundeEpost);
        bool slettOrdre(int ordrenr, string kundeEpost);
        List<visOrdreAdmin> visAlleKvitteringer();
        List<visallekvitteringer> visAlleKvitteringer(string kundeEpost);
        List<visordre> visHandlevogn(string kundeEmail);
        List<viskvittering> visKvittering(string kundeEmail, int kvitteringsID);
    }
}

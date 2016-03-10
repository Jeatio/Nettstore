using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.BLL
{
    public interface IAnsattLogikk
    {
        bool settInnAnsatt(ansatt innAnsatt);
        List<ansatt> listAnsatte();
        bool slettAnsatt(int id);
    }
}

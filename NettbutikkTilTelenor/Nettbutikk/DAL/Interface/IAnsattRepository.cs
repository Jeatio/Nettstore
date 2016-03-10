using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.DAL
{
    public interface IAnsattRepository
    {
        bool settInnAnsatt(ansatt innAnsatt);
        List<ansatt> listAnsatte();
        bool slettAnsatt(int id);
    }
}

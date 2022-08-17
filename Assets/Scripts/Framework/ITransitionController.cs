using System.Threading.Tasks;

namespace MeltingChamber.Framework
{
    public interface ITransitionController
    {
        Task Open();
        Task Close();
    }
}

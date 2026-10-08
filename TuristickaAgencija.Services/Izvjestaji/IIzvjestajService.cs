using TuristickaAgencija.Model.Izvjestaji;

namespace TuristickaAgencija.Services.Izvjestaji
{
    public interface IIzvjestajService
    {
        Task<UplateIzvjestaj> UplateZaMjesecAsync(int godina, int mjesec);
        Task<DashboardStatistika> DashboardAsync();
    }
}

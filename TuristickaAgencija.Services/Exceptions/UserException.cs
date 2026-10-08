namespace TuristickaAgencija.Services.Exceptions
{
    /// <summary>
    /// Greska uzrokovana neispravnim zahtjevom korisnika (poslovno pravilo, validacija...).
    /// ErrorFilter je pretvara u HTTP 400 sa porukom koja se prikazuje na UI-u.
    /// </summary>
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }

    /// <summary>Trazeni zapis ne postoji (HTTP 404).</summary>
    public class NotFoundException : UserException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>Korisnik nema pravo na trazenu akciju (HTTP 403).</summary>
    public class ForbiddenException : UserException
    {
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}

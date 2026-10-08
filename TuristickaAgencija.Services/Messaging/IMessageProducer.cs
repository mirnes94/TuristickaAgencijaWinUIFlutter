using TuristickaAgencija.Model.Messages;

namespace TuristickaAgencija.Services.Messaging
{
    public interface IMessageProducer
    {
        /// <summary>
        /// Salje notifikaciju na RabbitMQ red. Neuspjeh slanja se loguje, ali ne rusi
        /// glavnu operaciju (npr. registracija uspije i kad je RabbitMQ privremeno nedostupan).
        /// </summary>
        void Posalji(NotifikacijaPoruka poruka);
    }
}

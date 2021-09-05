namespace Infrastruttura
{
    public class Config
    {
        /// <summary>
        /// Tempo di inattività prima che la sessione venga abbandonata. Il tempo è espresseo in minuti
        /// </summary>
        public int SessionIdleTimeout { get; set; }

        /// <summary>
        /// Postfix per creare un indirizzo email a partire dallo username. L'indirizzo email sarà quindi: usewrname + EmailPostfix
        /// </summary>
        public string EmailPostfix { get; set; }

        /// <summary>
        /// Dopo quanti giorni scade la password
        /// </summary>
        public int PasswordExpirationDays { get; set; }

        /// <summary>
        /// Quanti giorni prima della scadenza avvisare l'utente che la password è in scadenza.
        /// </summary>
        public int PasswordExpirationReminderDays { get; set; }
    }
}

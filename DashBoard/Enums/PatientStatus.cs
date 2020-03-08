namespace DashBoard
{
    /// <summary>
    /// A patients status
    /// </summary>
    public enum PatientStatus
    {
        /// <summary>
        /// Registered (Name,Isman,Birthdate, CountryId , CityId, PassorId, Email,Tellno, Address, UserPassID, TimeRegister ,paternal name 
        /// Job ,Insurance, HelpName ,HelpJob, HelpTellNo)
        /// </summary>
        Registered = 0,
        /// <summary>
        /// the Patient is in Iran, Either With Company Interaction or not, 
        /// (no new fields)
        /// </summary>
        Entered = 1,
        /// <summary>
        /// Receptionary deeds are done.(In Hospital) (HealthCode,) 
        /// </summary>
        Reception = 2,
        /// <summary>
        /// (Pid , Coshar, DateReleased)
        /// </summary>
        Served = 3,
    }
}

namespace SophiChain.BlazorDatePicker.Localizations;

/// <summary>
/// French localization for DatePicker components
/// </summary>
public class FrenchDatePickerLocalizer : DefaultDatePickerLocalizer
{
    // Common
    public override string Close => "Fermer";
    public override string Clear => "Effacer";
    public override string Cancel => "Annuler";
    public override string Apply => "Appliquer";
    public override string Selected => "Sélectionné :";
    public override string PreviousMonth => "Mois précédent";
    public override string NextMonth => "Mois suivant";
    
    // Date Picker
    public override string SelectDate => "Sélectionner une Date";
    public override string Years => "Années";
    public override string Months => "Mois";
    public override string QuickSelect => "Sélection Rapide";
    public override string Today => "Aujourd'hui";
    public override string Yesterday => "Hier";
    public override string Tomorrow => "Demain";
    public override string SelectedDate => "Date Sélectionnée";
    public override string SelectDateFromCalendar => "Sélectionnez une date dans le calendrier ou les raccourcis";
    
    // Range Picker
    public override string SelectDateRange => "Sélectionner une Plage de Dates";
    public override string SelectDateRangePlaceholder => "Sélectionner une plage de dates";
    public override string Recent => "Récent";
    public override string StartDate => "Date de Début";
    public override string EndDate => "Date de Fin";
    public override string Days => "jours";
    public override string SelectEndDateToComplete => "Sélectionnez la date de fin pour compléter la plage";
    public override string SelectStartAndEndDates => "Sélectionnez les dates de début et de fin";
    
    // Range Shortcuts
    public override string Last7Days => "7 derniers jours";
    public override string Last14Days => "14 derniers jours";
    public override string Last30Days => "30 derniers jours";
    public override string Last60Days => "60 derniers jours";
    public override string Last90Days => "90 derniers jours";
    public override string LastWeek => "Semaine dernière";
    public override string LastMonth => "Mois dernier";
    public override string LastQuarter => "Trimestre dernier";
    public override string LastYear => "Année dernière";
    public override string WeekToDate => "Semaine à ce jour";
    public override string MonthToDate => "Mois à ce jour";
    public override string QuarterToDate => "Trimestre à ce jour";
    public override string YearToDate => "Année à ce jour";
    public override string Rolling7Days => "7 jours glissants";
    public override string Rolling30Days => "30 jours glissants";
    public override string Rolling90Days => "90 jours glissants";
    public override string ThisWeek => "Cette semaine";
    public override string ThisMonth => "Ce mois";
    public override string ThisQuarter => "Ce trimestre";
    public override string ThisYear => "Cette année";
    public override string Next1Day => "1 prochain jour";
    public override string Next2Days => "2 prochains jours";
    public override string Next3Days => "3 prochains jours";
    public override string Next7Days => "7 prochains jours";
    public override string Next14Days => "14 prochains jours";
    public override string Next30Days => "30 prochains jours";
    public override string Next90Days => "90 prochains jours";
    public override string NextWeek => "Semaine prochaine";
    public override string NextMonthRange => "Mois prochain";
    public override string NextQuarter => "Trimestre prochain";
    public override string NextYear => "Année prochaine";
    public override string Custom => "Personnalisé";
    
    // Validation Messages
    public override string StartDateCannotBeEarlierThan => "La date de début ne peut pas être antérieure au {0}";
    public override string EndDateCannotBeLaterThan => "La date de fin ne peut pas être postérieure au {0}";
    public override string SelectedDatesMustBeInThePast => "Les dates sélectionnées doivent être dans le passé";
    public override string SelectedDatesMustBeInTheFuture => "Les dates sélectionnées doivent être dans le futur";
    public override string RangeMustBeAtLeastDays => "La plage doit être d'au moins {0} jours";
    public override string RangeCannotExceedDays => "La plage ne peut pas dépasser {0} jours";
}

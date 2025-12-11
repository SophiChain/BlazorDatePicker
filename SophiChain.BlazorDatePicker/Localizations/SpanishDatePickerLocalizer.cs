namespace SophiChain.BlazorDatePicker.Localizations;

/// <summary>
/// Spanish localization for DatePicker components
/// </summary>
public class SpanishDatePickerLocalizer : DefaultDatePickerLocalizer
{
    // Common
    public override string Close => "Cerrar";
    public override string Clear => "Limpiar";
    public override string Cancel => "Cancelar";
    public override string Apply => "Aplicar";
    public override string Selected => "Seleccionado:";
    public override string PreviousMonth => "Mes anterior";
    public override string NextMonth => "Mes siguiente";
    
    // Date Picker
    public override string SelectDate => "Seleccionar Fecha";
    public override string Years => "Años";
    public override string Months => "Meses";
    public override string QuickSelect => "Selección Rápida";
    public override string Today => "Hoy";
    public override string Yesterday => "Ayer";
    public override string Tomorrow => "Mañana";
    public override string SelectedDate => "Fecha Seleccionada";
    public override string SelectDateFromCalendar => "Selecciona una fecha del calendario o los accesos directos";
    
    // Range Picker
    public override string SelectDateRange => "Seleccionar Rango de Fechas";
    public override string SelectDateRangePlaceholder => "Seleccionar rango de fechas";
    public override string Recent => "Reciente";
    public override string StartDate => "Fecha de Inicio";
    public override string EndDate => "Fecha de Fin";
    public override string Days => "días";
    public override string SelectEndDateToComplete => "Selecciona la fecha de fin para completar el rango";
    public override string SelectStartAndEndDates => "Selecciona las fechas de inicio y fin";
    
    // Range Shortcuts
    public override string Last7Days => "Últimos 7 días";
    public override string Last14Days => "Últimos 14 días";
    public override string Last30Days => "Últimos 30 días";
    public override string Last60Days => "Últimos 60 días";
    public override string Last90Days => "Últimos 90 días";
    public override string LastWeek => "Semana pasada";
    public override string LastMonth => "Mes pasado";
    public override string LastQuarter => "Trimestre pasado";
    public override string LastYear => "Año pasado";
    public override string WeekToDate => "Semana hasta hoy";
    public override string MonthToDate => "Mes hasta hoy";
    public override string QuarterToDate => "Trimestre hasta hoy";
    public override string YearToDate => "Año hasta hoy";
    public override string Rolling7Days => "7 días móviles";
    public override string Rolling30Days => "30 días móviles";
    public override string Rolling90Days => "90 días móviles";
    public override string ThisWeek => "Esta semana";
    public override string ThisMonth => "Este mes";
    public override string ThisQuarter => "Este trimestre";
    public override string ThisYear => "Este año";
    public override string Next1Day => "Próximo 1 día";
    public override string Next2Days => "Próximos 2 días";
    public override string Next3Days => "Próximos 3 días";
    public override string Next7Days => "Próximos 7 días";
    public override string Next14Days => "Próximos 14 días";
    public override string Next30Days => "Próximos 30 días";
    public override string Next90Days => "Próximos 90 días";
    public override string NextWeek => "Próxima semana";
    public override string NextMonthRange => "Próximo mes";
    public override string NextQuarter => "Próximo trimestre";
    public override string NextYear => "Próximo año";
    public override string Custom => "Personalizado";
    
    // Validation Messages
    public override string StartDateCannotBeEarlierThan => "La fecha de inicio no puede ser anterior a {0}";
    public override string EndDateCannotBeLaterThan => "La fecha de fin no puede ser posterior a {0}";
    public override string SelectedDatesMustBeInThePast => "Las fechas seleccionadas deben estar en el pasado";
    public override string SelectedDatesMustBeInTheFuture => "Las fechas seleccionadas deben estar en el futuro";
    public override string RangeMustBeAtLeastDays => "El rango debe ser de al menos {0} días";
    public override string RangeCannotExceedDays => "El rango no puede exceder {0} días";
}

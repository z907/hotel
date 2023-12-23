using CsvHelper.Configuration.Attributes;

namespace Hotel.ViewModel.VmEntities;

public class ReportInc
{
    [Name("День")]
    [Index(0)]
    public int Day { get; set; }
    [Name("Прибыль")]
    [Index(1)]
    public int Income { get; set; }
}
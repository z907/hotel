using CsvHelper.Configuration.Attributes;
using Hotel.View;

namespace Hotel.ViewModel.VmEntities;

public class ReportOcc
{
  [Name("Занятость")]
  [Index(0)]
  public string Occupancy { get; set; }
  [Name("Доля")]
  [Index(1)]
  public string Percentage { get; set; }
}
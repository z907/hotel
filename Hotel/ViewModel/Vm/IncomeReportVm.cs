using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using CsvHelper;
using Hotel.ViewModel.VmEntities;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using ViewModel.Services;
using FileDialog = System.Windows.Forms.FileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace Hotel.ViewModel;

public class IncomeReportVm:BaseVm
{
    private SeriesCollection _reportData;
    private ReportService repService;
    public ICommand GetReport { get; }
    public ICommand SaveReport { get; }
    
    private List<int> _yearList ;
    public List<int> YearList
    {
        get => _yearList;
        set
        {
            _yearList = value;
            OnPropertyChanged(nameof(YearList));
        }
    }

    private Dictionary<int, string> _months;

    public Dictionary<int, string> Months
    {
        get => _months;
        set
        {
            _months = value;
            OnPropertyChanged(nameof(Months));
        }
    }
    
    private int statMonth;

    public int StatMonth
    {
        get => statMonth;
        set
        {
            statMonth = value;
            OnPropertyChanged(nameof(StatMonth));
        }
    }
    private int statYear;

    public int StatYear
    {
        get => statYear;
        set
        {
            statYear = value;
            OnPropertyChanged(nameof(StatMonth));
        }
    }
    public IncomeReportVm()
    {
        GetReport = new VmCommand(ExecuteGetReport,CanExecuteGetReport);
        SaveReport = new VmCommand(ExecuteSaveReport,CanExecuteSaveReport);
        LoadData();
        repService = new ReportService();
    }

    private void LoadData()
    {
        Months=new Dictionary<int, string>()
        {
            {1,"Январь"},
            {2,"Февраль"},
            {3,"Март"},
            {4,"Апрель"},
            {5,"Май"},
            {6,"Июнь"},
            {7,"Июль"},
            {8,"Август"},
            {9,"Сентябрь"},
            {10,"Октябрь"},
            {11,"Ноябрь"},
            {12,"Декабрь"}
        };
        YearList = Enumerable.Range((DateTime.Now.Year - 10), 11).ToList();
    }
    public SeriesCollection ReportData
    {
        get => _reportData;
        set
        {
            _reportData = value;
            OnPropertyChanged(nameof(ReportData));
        }
    }

    private string[] _barLabels;

    public string[] BarLabels
    {
        get => _barLabels;
        set
        {
            _barLabels = value;
            OnPropertyChanged(nameof(BarLabels));
        }
    }

    private List<ReportInc> data;
    private List<int> _values;
    public List<int> Values
    {
        get => _values;
        set
        {
            _values = value;
            OnPropertyChanged(nameof(Values));
        }
    }
    public void ExecuteGetReport(object obj)
    {
        if (statMonth == 0 || statYear == 0) return;
        data = repService.GetIncomePerDay(statMonth,statYear);
        Formatter = value => value.ToString("N");
       Values = data.Select(inc => inc.Income).ToList();
        List<string> days = data.Select(inc => inc.Day.ToString()).ToList();
        
        ReportData = new SeriesCollection
        {
           new ColumnSeries
           {
               Title = "Прибыль",
               Values = new ChartValues<ObservableValue>()
           }
        };
        foreach(var x in Values)
        ReportData.First().Values.Add(new ObservableValue(x));
        BarLabels = days.ToArray();
    }

    private Func<double, string> _formatter;

    public Func<double, string> Formatter
    {
        get => _formatter;
        set
        {
            _formatter = value;
            OnPropertyChanged(nameof(Formatter));
        }
    }
    public bool CanExecuteGetReport(object obj)
    {
        return true;
    }
    public void ExecuteSaveReport(object obj)
    {
        SaveFileDialog fileDialog = new SaveFileDialog();
        fileDialog.Filter = "CSV отчет|*.csv";
        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            using (var writer=new StreamWriter(fileDialog.FileName,false,Encoding.GetEncoding(65001)))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecord(new ReportOcc{Occupancy = Months[StatMonth],Percentage = StatYear.ToString()});
                csv.NextRecord();
                csv.WriteRecords(data);
                csv.WriteRecord(new ReportOcc { Occupancy = "Итого", Percentage = data.Sum(r => r.Income).ToString() });
            }
        }
    }
    public bool CanExecuteSaveReport(object obj)
    {
        return true;
    }
}
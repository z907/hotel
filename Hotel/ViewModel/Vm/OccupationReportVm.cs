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

public class OccupationReportVm:BaseVm
{
    private SeriesCollection _reportData;
    private ReportService repService;
    public ICommand GetReport { get; }
    public ICommand SaveReport { get; }
    
    private List<int> _yearList = Enumerable.Range((DateTime.Now.Year - 10), 11).ToList();
    public List<int> YearList
    {
        get => _yearList;
        set
        {
            _yearList = value;
            OnPropertyChanged(nameof(YearList));
        }
    }
    private Dictionary<int, string> _months =new Dictionary<int, string>()
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
    public OccupationReportVm()
    {
        GetReport = new VmCommand(ExecuteGetReport,CanExecuteGetReport);
        SaveReport = new VmCommand(ExecuteSaveReport,CanExecuteSaveReport);
        
        repService = new ReportService();
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

    private int percentage;
    public void ExecuteGetReport(object obj)
    {
        if (statMonth == 0 || statYear == 0) return;
        percentage = repService.GetAverageOccupancyPercentage(statMonth,statYear);
        ReportData = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Занято ("+ percentage + " %)",
                Values = new ChartValues<ObservableValue>{new ObservableValue(percentage)},
                DataLabels = false
            },
            new PieSeries
            {
            Title = "Свободно ("+ (100-percentage) + " %)",
            Values = new ChartValues<ObservableValue>{new ObservableValue(100-percentage)},
            DataLabels = false
            }
        };
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
                csv.WriteRecord(new ReportOcc {Occupancy = "Занято",Percentage = percentage+"%"});
                csv.NextRecord();
                csv.WriteRecord(new ReportOcc {Occupancy = "Свободно",Percentage = (100-percentage)+"%" });
            }
        }
    }
    public bool CanExecuteSaveReport(object obj)
    {
        return true;
    }
}
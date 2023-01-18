using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using GoodPass.Contracts.ViewModels;
using GoodPass.Models;
using GoodPass.Services;

namespace GoodPass.ViewModels;

public class ListDetailsViewModel : ObservableRecipient, INavigationAware
{
    public void OnNavigatedFrom()
    {
    }

    private readonly GoodPassDataService _dataService;
    private GPData? _selectedData;
    public ObservableCollection<GPData> DataItems { get; private set; } = new ObservableCollection<GPData>();

    public ListDetailsViewModel(GoodPassDataService goodPassDataService)
    {
        _dataService = goodPassDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        DataItems.Clear();

        var datas = await _dataService.GetListDetailsDataAsync();

        foreach (var data in datas)
        {
            DataItems.Add(data);
        }
        EnsureItemSelected();
    }

    public GPData? SlectedData
    {
        get => _selectedData;
        set => SetProperty(ref _selectedData, value);
    }

    public void EnsureItemSelected()
    {
        if (SlectedData == null)
        {
            SlectedData = DataItems.First();
        }
    }

    public bool DeleteDataItem(GPData targetItem)
    {
        return DataItems.Remove(targetItem);
    }

    public void AddDataItem(GPData newData)
    {
        DataItems.Add(newData);
    }
}

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

    private GPData? _selectedData;
    public ObservableCollection<GPData> DataItems { get; private set; } = new ObservableCollection<GPData>();

    public ListDetailsViewModel()
    {
    }

    public async void OnNavigatedTo(object parameter)
    {
        DataItems.Clear();

        var datas = await GoodPassDataService.GetListDetailsDataAsync();

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
        SlectedData ??= DataItems.First(); //复合分配简化防null代码
    }

    /// <summary>
    /// 实现删除数据的实时响应
    /// </summary>
    /// <param name="targetItem">指定删除的数据</param>
    public bool DeleteDataItem(GPData targetItem)
    {
        return DataItems.Remove(targetItem);
    }

    /// <summary>
    /// 实现添加数据的实时响应
    /// </summary>
    public void AddDataItem(GPData newData)
    {
        DataItems.Add(newData);
    }

    public void GoToData(int index)
    {
        if (DataItems == null)
            throw new ArgumentNullException("GoToData: DataItems is null");
        if (DataItems.Count > index)
        {
            SlectedData = DataItems[index];
        }
    }
}

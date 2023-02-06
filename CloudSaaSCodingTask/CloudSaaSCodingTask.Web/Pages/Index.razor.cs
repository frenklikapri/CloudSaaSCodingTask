using AutoMapper;
using CloudSaaSCodingTask.Core.Common;
using CloudSaaSCodingTask.Core.Dtos;
using CloudSaaSCodingTask.Core.Entities;
using CloudSaaSCodingTask.Core.Helpers;
using CloudSaaSCodingTask.Core.Repositories;
using CloudSaaSCodingTask.Web.Components;
using CloudSaaSCodingTask.Web.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace CloudSaaSCodingTask.Web.Pages
{
    public partial class Index
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        private PaginatedTable<ViewEmployeeDto> _paginatedTable;
        private ViewEmployeeDto _itemTemplate = new(Guid.Empty, "", "", DateTime.Now, "");
        private string _nameFilter = string.Empty;
        private string _emailFilter = string.Empty;
        private string _departmentFilter = string.Empty;
        private bool _loading;
        private SaveEmployeeDto _employeeToSave = new SaveEmployeeDto();
        private ConfirmDialog _confirmDialog;
        private ViewEmployeeDto _employeeToDelete;
        private string _saveEmployeeModalId = "modalSaveEmployee";

        public async Task<PaginatedResult<ViewEmployeeDto>> GetItems(PaginationParameters paginationParameters)
        {
            var filter = EmployeeSearchHelper.BuildFilter(_nameFilter, _emailFilter, _departmentFilter);

            var result = await HttpClient.GetFromJsonAsync<PaginatedResult<ViewEmployeeDto>>($"employee?page={paginationParameters.Page}" +
                $"&pageSize={paginationParameters.PageSize}"
                + $"&name={_nameFilter}"
                + $"&email={_emailFilter}"
                + $"&department={_departmentFilter}"
                );

            return new PaginatedResult<ViewEmployeeDto>
            {
                Items = result.Items.Select(m => Mapper.Map<ViewEmployeeDto>(m)).ToList(),
                TotalCount = result.TotalCount
            };
        }

        private async Task ChangeName(ChangeEventArgs e)
        {
            _nameFilter = e.Value.ToString();
            await _paginatedTable.LoadItemsFromParentAsync(true);
        }

        private async Task ChangeEmail(ChangeEventArgs e)
        {
            _emailFilter = e.Value.ToString();
            await _paginatedTable.LoadItemsFromParentAsync(true);
        }

        private async Task ChangeDepartment(ChangeEventArgs e)
        {
            _departmentFilter = e.Value.ToString();
            await _paginatedTable.LoadItemsFromParentAsync(true);
        }

        private async Task SaveEmployee()
        {
            HttpResponseMessage result;
            bool add = false;
            if (_employeeToSave.Id == Guid.Empty)
            {
                add = true;
                result = await HttpClient.PostAsJsonAsync($"employee", _employeeToSave);
            }
            else
            {
                result = await HttpClient.PutAsJsonAsync($"employee", _employeeToSave);
            }

            if (result.IsSuccessStatusCode)
            {
                await JS.ShowSuccessAsync("Employee saved successfully!");
            }
            else
            {
                await JS.ShowSuccessAsync("Couldn't save employee!");
            }

            await JS.HideModalWithIdAsync(_saveEmployeeModalId);

            if (add)
            {
                await _paginatedTable.GoToLastPageAsync();
                await _paginatedTable.LoadItemsFromParentAsync(false);
            }
            else
            {
                await _paginatedTable.LoadItemsFromParentAsync(false);
            }

            _employeeToSave = new();
        }

        private async Task DeleteClicked(ViewEmployeeDto employeeToDelete)
        {
            _employeeToDelete = employeeToDelete;
            await _confirmDialog.Show();
        }

        private async Task ConfirmDeleteChanged(bool confirmed)
        {
            if (confirmed)
            {
                var result = await HttpClient.DeleteAsync($"employee/{_employeeToDelete.Id}");

                if (result.IsSuccessStatusCode)
                {
                    await JS.ShowSuccessAsync("Employee deleted successfully!");
                    await _paginatedTable.LoadItemsFromParentAsync(false);
                }
                else
                {
                    await JS.ShowSuccessAsync("Couldn't delete employee!");
                }

                _employeeToDelete = new(Guid.Empty, "", "", DateTime.Now, "");
            }
        }

        private async Task EditClicked(ViewEmployeeDto employee)
        {
            _employeeToSave = new SaveEmployeeDto
            {
                Id = employee.Id,
                DateOfBirth = employee.DateOfBirth,
                Department = employee.Department,
                Email = employee.Email,
                Name = employee.Name
            };
            await JS.ShowModalWithIdAsync(_saveEmployeeModalId);
        }

        private async Task AddClicked()
        {
            _employeeToSave = new SaveEmployeeDto();
            await JS.ShowModalWithIdAsync(_saveEmployeeModalId);
        }
    }
}

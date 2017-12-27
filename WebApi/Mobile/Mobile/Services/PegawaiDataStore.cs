using Mobile.Helpers;
using Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Mobile.Services.PegawaiDataStore))]

namespace Mobile.Services
{
    public class PegawaiDataStore : IDataStore<pegawai>
    {
        bool isInitialized;
        List<pegawai> items;

        public Task<bool> AddItemAsync(pegawai item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<pegawai> GetItemAsync(string id)
        {
            var pegId = Convert.ToInt32(id);
            await InitializeAsync();
            return await Task.FromResult(items.Where(O => O.Id== pegId).FirstOrDefault());
        }

        private async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<pegawai>();
            using (var service = new RestService())
            {
                try
                {
                    await service.CekTokenAsync();
                    var response = await service.GetAsync("api/pegawai");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<pegawai>>(content);
                        foreach (var item in Items)
                        {
                            items.Add(item);
                        }

                    }
                    else
                    {
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = ex.Message,
                        Cancel = "OK"
                    }, "message");
                }
                finally
                {
                    isInitialized = true;
                }
            }

        }

        public async Task<IEnumerable<pegawai>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
                isInitialized = false;
            await InitializeAsync();
            return await Task.FromResult(items);
        }

        public Task<bool> UpdateItemAsync(pegawai item)
        {
            throw new NotImplementedException();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Data;
using Ocph.DAL;

namespace Desktop.Collections
{
    public delegate void ChangeSource<T>(T obj);

    public class Repository<T> :BaseNotify ,ICollection<T>
    {
        private string uri;
        private List<T> list = new List<T>();
        public CollectionView SourceView { get; set; }
      //  private bool iSInstance;
        public event ChangeSource<T> OnChangeSource;
        public int Count => list.Count;
        public bool IsReadOnly => IsReadOnly;
        private T selected;

        public virtual T SelectedItem
        {
            get { return selected; }
            set { SetProperty(ref selected, value); }
        }


        public Repository(string uri)
        {
           SourceView = (CollectionView)CollectionViewSource.GetDefaultView(list);
           this.uri = uri;
             GetListAsync();

        }

       

        public async void GetListAsync()
        {
            list.Clear();
            Client client = new Client();
            var response = await client.ClientContext.GetAsync(uri);
            if (response.IsSuccessStatusCode == true)
            {
                var result = JsonConvert.DeserializeObject<List<T>>(response.Content.ReadAsStringAsync().Result);
                foreach (var item in result)
                {
                    list.Add(item);
                }
            }
            SourceView.Refresh();
        }

        public void Add(T item)
        {
            Client client = new Client();
            var obj = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            try
            {
                var response = client.ClientContext.PostAsync(uri, content).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    if (response != null)
                    {
                        list.Add(result);
                        OnChangeSource?.Invoke(result);
                        SourceView.Refresh();
                    }
                    ResourcesBase.ShowMessage("Data Berhasil Ditambah");
                }
                else
                {
                    throw new SystemException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
           
        }
       

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }



        public bool  Remove(T item)
        {
            dynamic a = item;
            var ApiUrl = uri + "?id=" + a.Id;
            try
            {
                if (ResourcesBase.MessageAsk("Yakin Hapus Data ?"))
                {
                    Client client = new Client();
                    var response = client.ClientContext.DeleteAsync(ApiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        list.Remove(item);
                        OnChangeSource(item);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
                return false;
            }
        }

        public T Updated(T item)
        {
            dynamic a = item;
            var ApiUrl = uri + "?id=" + a.Id;
            Client client = new Client();
            var obj = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            var response = client.ClientContext.PutAsync(ApiUrl,content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                if(OnChangeSource!=null)
                     OnChangeSource(result);
                return result;
            }
            else
                return default(T);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}

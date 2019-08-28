using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;
using corelsp.Shared.Models;
using corelsp.Shared.Helpers;

namespace corelsp.Shared.Models
{
    public class AppBase
    {

        public static async Task<bool> log(object msg){
            return await JSRuntime.Current.InvokeAsync<bool>("log",msg);
        }
        public static async Task<bool> loading_gif(){
            return await JSRuntime.Current.InvokeAsync<bool>("load_gif");
        }
        public static async Task<T> Fetch<T>(string url){
            return await HttpClientJson2.GetJsonAsync<T>(url);
        }
        public override string ToString()
        {
            return this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.Public)
            .Aggregate(Environment.NewLine, (acc, v) => acc + $"{v.Name} = {v.GetValue(this)}{Environment.NewLine}");
        }
    }
}

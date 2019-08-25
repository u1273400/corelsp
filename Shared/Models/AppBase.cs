using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;
using corelsp.Shared.Models;
using corelsp.Shared.Helpers;
//using Http=Microsoft.AspNetCore.Components.HttpClientJsonExtensions;

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

    }
}

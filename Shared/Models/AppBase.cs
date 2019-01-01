using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;
using corelsp.Shared.Models;

namespace corelsp.Shared.Models
{
    public class AppBase
    {
        public static async Task<bool> log(object msg){
            return await JSRuntime.Current.InvokeAsync<bool>("log",msg);
        }

    }
}

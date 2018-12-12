using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;
using corelsp.Shared.Models;

namespace corelsp.Shared.Controllers
{
    public class ControllerBase : BlazorComponent
    {
        protected async Task<bool> log(object msg){
            return await JSRuntime.Current.InvokeAsync<bool>("log",msg);
        }
        protected async Task<bool> init(object[] data,object[] cols){
            return await JSRuntime.Current.InvokeAsync<bool>("init",data,cols);
        }
        protected object[] testdata(){
            return TestData.populate();   
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;
using corelsp.Shared.Models;
using corelsp.Shared.Helpers;
using System.Collections.Generic;

namespace corelsp.Shared.Models
{
    public class Params
    {
        public static bool Editing { get; set; }=false;
        public static bool Saving { get; set; }=false;
        public static Params InitData=new Params();

        public IData iParams {get; set; } 
        public DeptMenuData[] DeptsMenu {get;set;}=new DeptMenuData[]{};

        public struct IData{ 
            public DateTime MaxDate;
            public string SessTok;
        }
        public struct DeptMenuData{ public int Key;public string Value;}
        
        // [JSInvokable]
        // public static async Task<bool> IsEditing(bool data){
        //     Editing=data;
        //     return true;
        // }
        
        // [JSInvokable]
        // public static async Task<bool> IsSaving(bool data){
        //     Saving=data;
        //     return true;
        // }
        [JSInvokable]
        public static async Task<DeptMenuData[]> Departments(){
            return InitData.DeptsMenu;
        }
    }
}

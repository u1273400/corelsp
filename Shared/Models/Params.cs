using System;
using System.Linq;
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
        public MenuData[] DeptsMenu {get;set;}=new MenuData[]{};
        public MenuData[] UsagesMenu {get;set;}=new MenuData[]{};
        public MenuData[] AsasList {get;set;}=new MenuData[]{};

        public struct IData{ 
            public DateTime MaxDate;
            public string SessTok;
        }
        public struct MenuData{ public int Key;public string Value;}
        
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
        public static async Task<string[]> Departments(){
            return InitData.DeptsMenu.Select(c=>c.Value).ToArray();
        }
        [JSInvokable]
        public static async Task<string[]> Usages(){
            return InitData.UsagesMenu.Select(c=>c.Value).ToArray();
        }
    }
    public class AppJsonResponse: AppBase
    {
        public string status { get; set; }
        public string errmsg { get; set; }
        public string error{ get; set; }
        public SpaceSaveObj value{ get; set; }
        
    }

}

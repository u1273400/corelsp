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
    public class Params: AppBase
    {
        public static bool Editing { get; set; }=false;
        public static bool Saving { get; set; }=false;
        public static Params InitData=new Params();

        public IData iParams {get; set; } 
        public MenuData[] DeptsMenu {get;set;}=new MenuData[]{};
        public MenuData[] UsagesMenu {get;set;}=new MenuData[]{};
        public MenuData[] LabelsMenu {get;set;}=new MenuData[]{};

        public Params(){}
        public class IData{ 
            public DateTime MaxDate;
            public string SessTok;

            public IData(){}
        }
        
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
        [JSInvokable]
        public static async Task<string[]> Labels(){
            return InitData.LabelsMenu.Select(c=>c.Value).ToArray();
        }
    }
    public class MenuData: AppBase{ 
        public int Key { get; set; }
        public string Value { get; set; }

        public MenuData(){}
    }
    public class AppJsonResponse: AppBase
    {
        public string status { get; set; }
        public string errmsg { get; set; }
        public string error{ get; set; }
        public SpaceSaveObj value{ get; set; }

        public AppJsonResponse(){}
    }
    public class FloorJsonResponse: AppBase
    {
        public string status { get; set; }
        public string errmsg { get; set; }
        public string error{ get; set; }
        public FloorSaveObj value{ get; set; }
        public FloorJsonResponse(){}
    }

}

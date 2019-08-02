using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Http=Microsoft.AspNetCore.Components.HttpClientJsonExtensions;
using System.Net.Http;

namespace corelsp.Shared.Models
{
    public class Space: AppBase
    {

        public static Space[] CSpaces;
        public static long CFId { get; set; } 

        public static object[] spcols = new object[]{
            // new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, selectable=true },
            new{id= "label", name= "Space", field= "label", width= 80, minWidth= 80, defaultSortAsc= true, selectable= false},
            new{id= "area", name= "Area", field= "area", minWidth= 40, selectable= false},
            new{id= "usageName", name= "Usage", field= "usageName", minWidth= 200, selectable= false},
            new{id= "capacity", name= "Capacity", field= "capacity", minWidth= 200, selectable= false},
            new{id= "dept", name= "Department", field= "dept", minWidth= 250, selectable= false},
            new{id= "asasName", name= "Asas", field= "asasName", minWidth= 250, selectable= false},
            new{id= "tableDate", name= "tableDate", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public string Label { get; set; }
        public string Dept { get; set; }
        public long DeptId { get; set; }
        public double Area { get; set; }
        public string UsageName { get; set; }
        public string AsasName { get; set; }
        public long AsasId { get; set; }
        public long Floor { get; set; }
        public long Capacity { get; set; }
        public DateTime tableDate { get; set; }

        // public static string[] Months(){
        //     return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        // }

        [JSInvokable]
        public static async Task<bool> SetSpaces(string data){
            //await log($"Floors::SetFloors: data = {data}");
            CSpaces=Json.Deserialize<Space[]>(data);
            Init();
            return true;
        }

        [JSInvokable]
        public static async Task<bool> SetFloor(long fid){
            CFId=fid;
            //await log("set floor called "+CFId);
            return await JSRuntime.Current.InvokeAsync<bool>("initFloors",$"../api/spr/{Space.CFId}/{Building.CMonth}");
        }
        public static async Task<bool> Init(){
            //await log($"Space::Init: Cspaces={CSpaces.Length}");
            return await JSRuntime.Current.InvokeAsync<bool>("initspaces",CSpaces,spcols);
        }
    }
    
}
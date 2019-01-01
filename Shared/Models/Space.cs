using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Models
{
    public class Space: AppBase
    {
        public static Space[] Spaces;
        public static long CBId { get; set; } = 2;

        public static object[] spcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, defaultSortAsc= true, selectable=true },
            new{id= "floorName", name= "Floor ref", field= "floorName", width= 80, minWidth= 80, selectable= false},
            new{id= "gIA", name= "GIA", field= "gIA", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public long Label { get; set; }
        public string Dept { get; set; }
        public long DeptId { get; set; }
        public string Area { get; set; }
        public long Floor { get; set; }
        public DateTime tableDate { get; set; }

        // public static string[] Months(){
        //     return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        // }

        public static Floor[] FromBuilding(){
            return Floors.Where(c=>c.tableDate==DateTime.Parse(Building.CMonth)&&c.BuildingId==CBId).ToArray();
        }

        [JSInvokable]
        public static async Task<String> SetBuilding(long bid){
            CBId=bid;
            await log("set building called "+CBId);
            Init();
            return String.Empty;
        }
        public static async Task<bool> Init(){
            return await JSRuntime.Current.InvokeAsync<bool>("initflrs",FromBuilding(),flrcols);
        }
    }
    
}
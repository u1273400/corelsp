using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Models
{
    public class Floor
    {
        public static Floor[] Floors;
        public static long CBId { get; set; } = 2;

        public static object[] flrcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, defaultSortAsc= true, selectable=true },
            new{id= "buildingRef", name= "Bldg Ref", field= "buildingRef", selectable= false, minWidth= 60},
            new{id= "buildingName", name= "Building Name", field= "buildingName", width= 150, minWidth= 180, selectable= false},
            new{id= "tableDate", name= "Table Date", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string FloorName { get; set; }
        public double GIA { get; set; }
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
            await AppBase.log("set building called "+CBId);
            Init();
            return String.Empty;
        }
        public static async Task<bool> Init(){
            return await JSRuntime.Current.InvokeAsync<bool>("initflrs",FromBuilding(),flrcols);
        }
    }
    
}
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
        public static string CBId { get; set; } = "2";

        public static object[] bldcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, defaultSortAsc= true, selectable=true },
            new{id= "buildingRef", name= "Bldg Ref", field= "buildingRef", selectable= false, minWidth= 60},
            new{id= "buildingName", name= "Building Name", field= "buildingName", width= 150, minWidth= 180, selectable= false},
            new{id= "tableDate", name= "Table Date", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public string FloorName { get; set; }
        public double GIA { get; set; }
        public DateTime tableDate { get; set; }

        // public static string[] Months(){
        //     return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        // }

        public static Building[] FromBuilding(string bid){
            return Buildings.Where(c=>c.tableDate==DateTime.Parse(monthend)).ToArray();
        }

        [JSInvokable]
        public static Task<String> SetMonth(string month){
            CMonth=month;
            log("set month called "+Building.CMonth);
            Init();
            return Task.FromResult(String.Empty);
        }
        public static async Task<bool> log(object msg){
            return await JSRuntime.Current.InvokeAsync<bool>("log",msg);
        }
        public static async Task<bool> Init(object[] data,object[] cols, string[] months){
            return await JSRuntime.Current.InvokeAsync<bool>("init",data,cols,months);
        }
        public static async Task<bool> Init(){
            return await JSRuntime.Current.InvokeAsync<bool>("init",Monthly(CMonth),bldcols,Months());
        }
    }
    
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Models
{
    public class Building: AppBase
    {
        public static Building[] Buildings;
        public static string CMonth { get; set; } = "2018-05-31";

        public static object[] bldcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, sortable= true, resizable= false, selectable=true },
            new{id= "buildingRef", name= "Bldg Ref", field= "buildingRef", minWidth= 60, defaultSortAsc= true, selectable= false, sortable= true},
            new{id= "buildingName", name= "Building Name", field= "buildingName", width= 150, minWidth= 180, selectable= false, sortable= true},
            new{id= "tableDate", name= "Table Date", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public string BuildingRef { get; set; }
        public string BuildingName { get; set; }
        public DateTime tableDate { get; set; }

        public static string[] Months(){
            return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        }

        public static Building[] Monthly(string monthend){
            return Buildings.Where(c=>c.tableDate==DateTime.Parse(monthend)).ToArray();
        }

        public static string InitialiseDate(DateTime theDate){
            return theDate.Year+"-"+(theDate.Month>9?"":"0")+theDate.Month+"-"+DateTime.DaysInMonth(theDate.Year,theDate.Month);
        }

        [JSInvokable]
        public static Task<String> GetCMonth(){
            return Task.FromResult(CMonth);
        }

        [JSInvokable]
        public static Task<String> SetMonth(string month){
            CMonth=month;
            //log("set month called "+Building.CMonth);
            Init();
            return Task.FromResult(String.Empty);
        }

        public static async Task<bool> Init(){
            var bldgs = Monthly(CMonth);
            //log($"Within Building.Init bldgs count={bldgs.Length}, cmonth={CMonth}");
            Floor.CBId=bldgs[0].Id;
            await JSRuntime.Current.InvokeAsync<bool>("init",bldgs,bldcols,Months());
            log($"Building::Init: Initialising floors../api/flr/{Floor.CBId}/{Building.CMonth}");
            return await JSRuntime.Current.InvokeAsync<bool>("initFloors",$"../api/flr/{Floor.CBId}/{Building.CMonth}");
        }
    }

}

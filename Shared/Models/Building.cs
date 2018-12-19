using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Models
{
    class Building
    {
        public static Building[] Buildings;

        public long Id { get; set; }
        public string BuildingRef { get; set; }
        public string BuildingName { get; set; }
        public string tableDate { get; set; }

        public static string[] Months(){
            return Buildings.Select(c=>c.tableDate).Distinct().ToArray();
        }

        public static Building[] Monthly(string monthend){
            return Buildings.Where(c=>c.tableDate.Equals(monthend)).ToArray();
        }
    }
    
}
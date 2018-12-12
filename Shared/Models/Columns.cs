using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Models
{
    public class Columns{
        public string id;
        public string name;
        public string field;
        public string behavior;
        public string cssClass;
        public string editor;
        public string formatter;
        public string validator;
        public int width;
        public int minWidth;
        public int maxWidth;
        public bool cannotTriggerInsert;
        public bool resizable;
        public bool sortable;
        public bool selectable;
        public bool defaultSortAsc;
    }
}
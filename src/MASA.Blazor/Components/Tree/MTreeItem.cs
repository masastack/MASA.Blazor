using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public class MTreeItem<T> : BTreeItem<T>
    {
        public override void SetComponentClass()
        {
            CssBuilder.Add("m-treeview-node");

            CssBuilderRoot.Add("m-treeview-node__root");

            CssBuilderLevel.Add("m-treeview-node__level");

            
        }
    }
}

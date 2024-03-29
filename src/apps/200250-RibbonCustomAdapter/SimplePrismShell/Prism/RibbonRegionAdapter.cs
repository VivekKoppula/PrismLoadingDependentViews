﻿using Infragistics.Windows.Ribbon;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrismShell.Prism
{
    public class RibbonRegionAdapter : RegionAdapterBase<XamRibbon>
    {
        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamRibbon regionTarget)
        {
            if (region == null) throw new ArgumentNullException("region");
            if (regionTarget == null) throw new ArgumentNullException("regionTarget");

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems!)
                    {
                        AddViewToRegion(view, regionTarget);
                    }

                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems!)
                    {
                        RemoveViewFromRegion(view, regionTarget);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }

        static void AddViewToRegion(object view, XamRibbon xamRibbon)
        {
            var ribbonTabItem = view as RibbonTabItem;
            if (ribbonTabItem != null)
                xamRibbon.Tabs.Add(ribbonTabItem);
        }

        static void RemoveViewFromRegion(object view, XamRibbon xamRibbon)
        {
            var ribbonTabItem = view as RibbonTabItem;
            if (ribbonTabItem != null)
                xamRibbon.Tabs.Remove(ribbonTabItem);
        }
    }
}
